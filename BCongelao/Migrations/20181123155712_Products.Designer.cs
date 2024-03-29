﻿// <auto-generated />
using System;
using BCongelao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BCongelao.Migrations
{
    [DbContext(typeof(BCongelaoContext))]
    [Migration("20181123155712_Products")]
    partial class Products
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BCongelao.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComercioId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int>("StatusOrder");

                    b.Property<string>("UserId");

                    b.HasKey("OrderId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("BCongelao.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ITBIS");

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<decimal>("Quantity");

                    b.Property<decimal>("Total");

                    b.Property<decimal>("UnitPrice");

                    b.Property<string>("UserId");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("BCongelao.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComercioId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.Property<int>("ProductTypeId");

                    b.Property<decimal>("Quantity");

                    b.Property<decimal>("QuantityUnit");

                    b.Property<int>("StatusProduct");

                    b.Property<int>("Unit");

                    b.Property<decimal>("UnitPrice");

                    b.Property<decimal>("UnitsInStock");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserId");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("BCongelao.Models.ProductLog", b =>
                {
                    b.Property<int>("ProductLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComercioId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("ProductId");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.Property<int>("ProductTypeId");

                    b.Property<decimal>("Quantity");

                    b.Property<decimal>("QuantityUnit");

                    b.Property<int>("StatusProduct");

                    b.Property<int>("Unit");

                    b.Property<decimal>("UnitPrice");

                    b.Property<decimal>("UnitsInStock");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserId");

                    b.HasKey("ProductLogId");

                    b.ToTable("ProductLogs");
                });

            modelBuilder.Entity("BCongelao.Models.OrderDetail", b =>
                {
                    b.HasOne("BCongelao.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BCongelao.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
