﻿// <auto-generated />
using System;
using EcomRevisited.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcomRevisited.Migrations
{
    [DbContext(typeof(EcomDbContext))]
    [Migration("20231028085807_new-price-fields")]
    partial class newpricefields
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            Id = new Guid("c8840611-9ff4-4c16-9daf-631e4958680f")
                        },
                        new
                        {
                            Id = new Guid("94306a99-bcd0-4c34-8185-fae513e0457f")
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

                    b.HasIndex("ProductId");

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
                            Id = new Guid("7a3afe90-dc82-4e2d-bc23-12f9302b0d92"),
                            ConversionRate = 1.0,
                            Name = "Canada",
                            TaxRate = 0.070000000000000007
                        },
                        new
                        {
                            Id = new Guid("9bfb4a05-3738-421c-a278-3ff94f3fb461"),
                            ConversionRate = 1.1000000000000001,
                            Name = "United States",
                            TaxRate = 0.050000000000000003
                        },
                        new
                        {
                            Id = new Guid("12c7f898-f70b-46c8-8e88-d69ba6f7568a"),
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

                    b.Property<double>("ConvertedPrice")
                        .HasColumnType("float");

                    b.Property<string>("DestinationCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("FinalPrice")
                        .HasColumnType("float");

                    b.Property<string>("MailingCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfItems")
                        .HasColumnType("int");

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
                            Id = new Guid("5c8f99f3-bc5b-4459-b897-0bbf93269ae0"),
                            AvailableQuantity = 10,
                            Description = "High performance laptop",
                            ImageUrl = "https://www.lifeofpix.com/wp-content/uploads/2018/05/p-244-ae-mint-005-1600x1169.jpg",
                            Name = "Laptop",
                            Price = 1000.0
                        },
                        new
                        {
                            Id = new Guid("a7b733e5-c8f4-4326-8fce-a692bd72414a"),
                            AvailableQuantity = 20,
                            Description = "Latest model",
                            ImageUrl = "https://media.istockphoto.com/id/1377877660/vector/realistic-mobile-phone-mockup-cellphone-app-template-isolated-stock-vector.jpg?s=612x612&w=0&k=20&c=Xw2padf6w33h9eQFFz83PL0reGEMdu1FtFsuI5G5Nf0=",
                            Name = "Smartphone",
                            Price = 800.0
                        },
                        new
                        {
                            Id = new Guid("d531e956-1460-4601-94ae-dd2d6a2f858a"),
                            AvailableQuantity = 30,
                            Description = "Wireless",
                            ImageUrl = "https://images.pexels.com/photos/3945667/pexels-photo-3945667.jpeg?cs=srgb&dl=pexels-cottonbro-studio-3945667.jpg&fm=jpg",
                            Name = "Headphones",
                            Price = 150.0
                        },
                        new
                        {
                            Id = new Guid("e538ce0e-273b-49b0-826d-431004edc63d"),
                            AvailableQuantity = 5,
                            Description = "Digital SLR",
                            ImageUrl = "https://images.pexels.com/photos/274973/pexels-photo-274973.jpeg?cs=srgb&dl=pexels-pixabay-274973.jpg&fm=jpg",
                            Name = "Camera",
                            Price = 1200.0
                        },
                        new
                        {
                            Id = new Guid("12a1ee3a-0e08-4c42-8a8f-d24e7d78f743"),
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

                    b.HasOne("EcomRevisited.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
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