﻿// <auto-generated />
using System;
using LeadManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LeadManager.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250426133654_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("LeadManager.Models.Lead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<string>("Suburb")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Leads");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Painters",
                            DateCreated = new DateTime(2024, 1, 4, 14, 37, 0, 0, DateTimeKind.Utc),
                            Description = "Need to paint 2 aluminum windows and a sliding glass door",
                            Email = "bill@example.com",
                            FirstName = "Bill",
                            LastName = "Smith",
                            Phone = "0412345678",
                            Price = 62.00m,
                            Status = 0,
                            Suburb = "Yanderra 2574"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Interior Painters",
                            DateCreated = new DateTime(2024, 1, 4, 13, 15, 0, 0, DateTimeKind.Utc),
                            Description = "Internal walls 3 colours",
                            Email = "craig@sample.net",
                            FirstName = "Craig",
                            LastName = "Johnson",
                            Phone = "0498765432",
                            Price = 49.00m,
                            Status = 0,
                            Suburb = "Woolooware 2230"
                        },
                        new
                        {
                            Id = 3,
                            Category = "General Building Work",
                            DateCreated = new DateTime(2024, 9, 5, 10, 36, 0, 0, DateTimeKind.Utc),
                            Description = "Plaster exposed brick walls",
                            Email = "pete@mailinator.com",
                            FirstName = "Pete",
                            LastName = "Sample",
                            Phone = "0412345678",
                            Price = 526.00m,
                            Status = 0,
                            Suburb = "Carramar 6031"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Electrical",
                            DateCreated = new DateTime(2024, 2, 15, 9, 20, 0, 0, DateTimeKind.Utc),
                            Description = "Install new lighting fixtures",
                            Email = "maria@example.com",
                            FirstName = "Maria",
                            LastName = "Garcia",
                            Phone = "0422334455",
                            Price = 850.00m,
                            Status = 0,
                            Suburb = "Sydney 2000"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
