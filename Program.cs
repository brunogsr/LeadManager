using LeadManager.Data;
using LeadManager.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000", // Permite origem do React (porta padr�o)
                                             "http://localhost:3001", // Outra porta comum para React
                                             "http://localhost:5173") // Porta padr�o do Vite/React
                                .AllowAnyHeader() // Permite qualquer cabe�alho na requisi��o (Authorization, Content-Type, etc.)
                                .AllowAnyMethod(); // Permite qualquer m�todo HTTP (GET, POST, PUT, DELETE, etc.)
                          // Em produ��o, seja mais restritivo com Headers e Methods se poss�vel.
                          // Para permitir qualquer origem (N�O RECOMENDADO PARA PRODU��O): .AllowAnyOrigin()
                      });
});

// --- Registro de Servi�os ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro do Servi�o de Email
builder.Services.AddScoped<IEmailService, FakeEmailService>();
builder.Services.AddScoped<ILeadService, LeadService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- Configura��o do Pipeline HTTP ---

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
}
else
{
    // app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// --- Aplicar a Pol�tica de CORS ---
// IMPORTANTE: Chamar UseCors DEPOIS de UseRouting e ANTES de UseAuthorization (se houver) e MapControllers.
app.UseCors(MyAllowSpecificOrigins);

// app.UseAuthorization(); // Descomente se adicionar autentica��o/autoriza��o

app.MapControllers();

app.Run();
