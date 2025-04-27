// LeadManager/Data/AppDbContext.cs
using LeadManager.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LeadManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lead> Leads { get; set; }
        public DbSet<Job> Jobs { get; set; } // Adicionado DbSet para Job

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>(entity =>
            {

            });

            modelBuilder.Entity<Lead>(entity =>
            {
                entity.Property(e => e.DateCreated)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Status)
                      .HasDefaultValue(LeadStatus.Invited);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.OriginalPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(l => l.Job)
                      .WithMany(j => j.Leads)
                      .HasForeignKey(l => l.JobId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Job>().HasData(
                new Job { Id = 1, ClientName = "Bill Smith", Address = "Yanderra 2574", DateRequested = new DateTime(2024, 1, 4, 0, 0, 0, DateTimeKind.Utc) },
                new Job { Id = 2, ClientName = "Craig Johnson", Address = "Woolooware 2230", DateRequested = new DateTime(2024, 1, 4, 0, 0, 0, DateTimeKind.Utc) },
                new Job { Id = 3, ClientName = "Pete Sample", Address = "Carramar 6031", DateRequested = new DateTime(2024, 9, 5, 0, 0, 0, DateTimeKind.Utc) },
                new Job { Id = 4, ClientName = "Maria Garcia", Address = "Sydney 2000", DateRequested = new DateTime(2024, 2, 15, 0, 0, 0, DateTimeKind.Utc) },
                new Job { Id = 5, ClientName = "Chris Sanderson", Address = "Quinns Rocks 6030", DateRequested = new DateTime(2023, 8, 30, 0, 0, 0, DateTimeKind.Utc) }
            );

            modelBuilder.Entity<Lead>().HasData(
                new Lead
                {
                    Id = 1,
                    FirstName = "Bill",
                    LastName = "Smith",
                    DateCreated = new DateTime(2024, 1, 4, 14, 37, 0, DateTimeKind.Utc),
                    Suburb = "Yanderra 2574",
                    Category = "Painters",
                    JobId = 1, // <-- ATUALIZADO
                    Description = "Need to paint 2 aluminum windows and a sliding glass door",
                    Price = 62.00m,
                    OriginalPrice = 62.00m,
                    Status = LeadStatus.Invited,
                    Phone = "0412345678",
                    Email = "bill@example.com"
                },
                new Lead
                {
                    Id = 2,
                    FirstName = "Craig",
                    LastName = "Johnson",
                    DateCreated = new DateTime(2024, 1, 4, 13, 15, 0, DateTimeKind.Utc),
                    Suburb = "Woolooware 2230",
                    Category = "Interior Painters",
                    JobId = 2, // <-- ATUALIZADO
                    Description = "Internal walls 3 colours",
                    Price = 49.00m,
                    OriginalPrice = 49.00m,
                    Status = LeadStatus.Invited,
                    Phone = "0498765432",
                    Email = "craig@sample.net"
                },
                new Lead
                {
                    Id = 3,
                    FirstName = "Pete",
                    LastName = "Sample",
                    DateCreated = new DateTime(2024, 9, 5, 10, 36, 0, DateTimeKind.Utc),
                    Suburb = "Carramar 6031",
                    Category = "General Building Work",
                    JobId = 3, // <-- ATUALIZADO
                    Description = "Plaster exposed brick walls",
                    Price = 526.00m,
                    OriginalPrice = 526.00m,
                    Status = LeadStatus.Invited,
                    Phone = "0412345678",
                    Email = "pete@mailinator.com"
                },
                new Lead
                {
                    Id = 4,
                    FirstName = "Maria",
                    LastName = "Garcia",
                    DateCreated = new DateTime(2024, 2, 15, 9, 20, 0, DateTimeKind.Utc),
                    Suburb = "Sydney 2000",
                    Category = "Electrical",
                    JobId = 4, // <-- ATUALIZADO
                    Description = "Install new lighting fixtures",
                    Price = 850.00m,
                    OriginalPrice = 850.00m,
                    Status = LeadStatus.Invited,
                    Phone = "0422334455",
                    Email = "maria@example.com"
                },
                 new Lead
                 {
                     Id = 5,
                     FirstName = "Chris",
                     LastName = "Sanderson",
                     DateCreated = new DateTime(2023, 8, 30, 11, 14, 0, DateTimeKind.Utc),
                     Suburb = "Quinns Rocks 6030",
                     Category = "Home Renovations",
                     JobId = 5, // <-- ATUALIZADO
                     Description = "Two story building conversion",
                     Price = 1532.00m,
                     OriginalPrice = 1532.00m,
                     Status = LeadStatus.Accepted,
                     Phone = "04987654321",
                     Email = "another.fake@hipmail.com"
                 }
            );
        }
    }
}