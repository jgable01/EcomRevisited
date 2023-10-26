﻿// <auto-generated />
using System;
using EcomRevisited.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcomRevisited.Migrations
{
    [DbContext(typeof(EcomDbContext))]
    partial class EcomDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EcomRevisited.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Carts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("45fd1914-69f0-4398-9b5e-aea66929b005")
                        },
                        new
                        {
                            Id = new Guid("d4e8779a-906e-45bb-9cee-20ce678b7c7c")
                        });
                });

            modelBuilder.Entity("EcomRevisited.Models.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("EcomRevisited.Models.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("ConversionRate")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TaxRate")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e698307a-3b26-457d-a9c6-79847d643bd4"),
                            ConversionRate = 1.0,
                            Name = "Canada",
                            TaxRate = 0.070000000000000007
                        },
                        new
                        {
                            Id = new Guid("a121e5f1-b02c-4acf-bb24-f648b56230a7"),
                            ConversionRate = 1.1000000000000001,
                            Name = "United States",
                            TaxRate = 0.050000000000000003
                        },
                        new
                        {
                            Id = new Guid("3433e79c-e0a6-4ef7-b7c1-58d16614c388"),
                            ConversionRate = 1.3,
                            Name = "United Kingdom",
                            TaxRate = 0.10000000000000001
                        });
                });

            modelBuilder.Entity("EcomRevisited.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MailingCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EcomRevisited.Models.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("EcomRevisited.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("27c978ee-afb8-4630-bff6-1f77dadcdbe4"),
                            AvailableQuantity = 10,
                            Description = "High performance laptop",
                            ImageUrl = "https://www.lifeofpix.com/wp-content/uploads/2018/05/p-244-ae-mint-005-1600x1169.jpg",
                            Name = "Laptop",
                            Price = 1000.0
                        },
                        new
                        {
                            Id = new Guid("ff5c99b2-42fa-432c-9a61-b6209d6fa91d"),
                            AvailableQuantity = 20,
                            Description = "Latest model",
                            ImageUrl = "https://media.istockphoto.com/id/1377877660/vector/realistic-mobile-phone-mockup-cellphone-app-template-isolated-stock-vector.jpg?s=612x612&w=0&k=20&c=Xw2padf6w33h9eQFFz83PL0reGEMdu1FtFsuI5G5Nf0=",
                            Name = "Smartphone",
                            Price = 800.0
                        },
                        new
                        {
                            Id = new Guid("063efa76-3b7d-4887-ba7d-992e4269c4fd"),
                            AvailableQuantity = 30,
                            Description = "Wireless",
                            ImageUrl = "https://images.pexels.com/photos/3945667/pexels-photo-3945667.jpeg?cs=srgb&dl=pexels-cottonbro-studio-3945667.jpg&fm=jpg",
                            Name = "Headphones",
                            Price = 150.0
                        },
                        new
                        {
                            Id = new Guid("e653c522-c6c9-4ded-8a39-d938ae8acce4"),
                            AvailableQuantity = 5,
                            Description = "Digital SLR",
                            ImageUrl = "https://images.pexels.com/photos/274973/pexels-photo-274973.jpeg?cs=srgb&dl=pexels-pixabay-274973.jpg&fm=jpg",
                            Name = "Camera",
                            Price = 1200.0
                        },
                        new
                        {
                            Id = new Guid("8ced7c77-de52-45c7-abdd-0675822d6e2f"),
                            AvailableQuantity = 15,
                            Description = "With fitness tracking",
                            ImageUrl = "https://images.pexels.com/photos/393047/pexels-photo-393047.jpeg?cs=srgb&dl=pexels-alexandr-borecky-393047.jpg&fm=jpg",
                            Name = "Smartwatch",
                            Price = 250.0
                        });
                });

            modelBuilder.Entity("EcomRevisited.Models.CartItem", b =>
                {
                    b.HasOne("EcomRevisited.Models.Cart", null)
                        .WithMany("CartItems")
                        .HasForeignKey("CartId");
                });

            modelBuilder.Entity("EcomRevisited.Models.OrderItem", b =>
                {
                    b.HasOne("EcomRevisited.Models.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("EcomRevisited.Models.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("EcomRevisited.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
