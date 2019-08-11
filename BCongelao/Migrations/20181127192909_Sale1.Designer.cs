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
    [Migration("20181127192909_Sale1")]
    partial class Sale1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BCongelao.Models.Balance", b =>
                {
                    b.Property<int>("BalanceId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("PaymentType");

                    b.Property<decimal>("Total");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UserId");

                    b.HasKey("BalanceId");

                    b.ToTable("Balance");
                });

            modelBuilder.Entity("BCongelao.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CustomerName")
                        .IsRequired();

                    b.Property<string>("Location");

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<int>("StatusCustomer");

                    b.Property<string>("UserId");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("BCongelao.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComercioId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<decimal>("ITBIS");

                    b.Property<DateTime>("OrderDate");

                    b.Property<decimal>("Quantity");

                    b.Property<int>("StatusOrder");

                    b.Property<decimal>("Total");

                    b.Property<decimal>("TotalNoITBIS");

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

                    b.Property<int>("Category");

                    b.Property<int>("ComercioId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Envase");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.Property<int>("ProductTypeId");

                    b.Property<decimal>("Quantity");

                    b.Property<decimal>("QuantityUnit");

                    b.Property<decimal>("QuantityUnit2");

                    b.Property<int>("StatusProduct");

                    b.Property<int>("Unit");

                    b.Property<int>("Unit2");

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

            modelBuilder.Entity("BCongelao.Models.Sale", b =>
                {
                    b.Property<int>("SaleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CustomerDescription");

                    b.Property<int>("CustomerId");

                    b.Property<decimal>("Debt");

                    b.Property<decimal>("ITBIS");

                    b.Property<decimal>("Paid");

                    b.Property<int>("PaymentType");

                    b.Property<decimal>("Quantity");

                    b.Property<DateTime>("SaleDate");

                    b.Property<int>("StatusSale");

                    b.Property<decimal>("Total");

                    b.Property<decimal>("TotalNoITBIS");

                    b.Property<string>("UserId");

                    b.HasKey("SaleId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Sale");
                });

            modelBuilder.Entity("BCongelao.Models.SaleDetail", b =>
                {
                    b.Property<int>("SaleDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ITBIS");

                    b.Property<int>("ProductId");

                    b.Property<decimal>("Quantity");

                    b.Property<int>("SaleId");

                    b.Property<decimal>("Total");

                    b.Property<decimal>("UnitPrice");

                    b.Property<string>("UserId");

                    b.HasKey("SaleDetailId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SaleId");

                    b.ToTable("SaleDetail");
                });

            modelBuilder.Entity("BCongelao.Models.TransferPayment", b =>
                {
                    b.Property<int>("TransferPaymentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("PaymentTypeFrom");

                    b.Property<int>("PaymentTypeTo");

                    b.Property<decimal>("Total");

                    b.Property<string>("UserId");

                    b.HasKey("TransferPaymentId");

                    b.ToTable("TransferPayment");
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

            modelBuilder.Entity("BCongelao.Models.Sale", b =>
                {
                    b.HasOne("BCongelao.Models.Customer", "Customer")
                        .WithMany("Sales")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BCongelao.Models.SaleDetail", b =>
                {
                    b.HasOne("BCongelao.Models.Product", "Product")
                        .WithMany("SaleDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BCongelao.Models.Sale", "Sale")
                        .WithMany("SaleDetails")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
