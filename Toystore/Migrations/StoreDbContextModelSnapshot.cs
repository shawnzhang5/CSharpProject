﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Toystore.Models;

namespace Toystore.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    partial class StoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Toystore.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            id = 1,
                            Category = "Animals",
                            Description = "",
                            Name = "Polar Bear",
                            Photo = ""
                        },
                        new
                        {
                            id = 2,
                            Category = "MISC",
                            Description = "",
                            Name = "Piano lesson",
                            Photo = ""
                        },
                        new
                        {
                            id = 3,
                            Category = "MISC",
                            Description = "",
                            Name = "Gardening",
                            Photo = ""
                        },
                        new
                        {
                            id = 4,
                            Category = "Furnitures",
                            Description = "",
                            Name = "Workbench",
                            Photo = ""
                        });
                });

            modelBuilder.Entity("Toystore.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RePassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Vendor")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Age = 30,
                            Email = "aaa@example.com",
                            Gender = false,
                            Password = "111",
                            RePassword = "111",
                            UserName = "aaa",
                            Vendor = true
                        },
                        new
                        {
                            UserId = 2,
                            Age = 30,
                            Email = "bbb@example.com",
                            Gender = false,
                            Password = "111",
                            RePassword = "111",
                            UserName = "bbb",
                            Vendor = true
                        },
                        new
                        {
                            UserId = 3,
                            Age = 30,
                            Email = "ccc@example.com",
                            Gender = false,
                            Password = "111",
                            RePassword = "111",
                            UserName = "ccc",
                            Vendor = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
