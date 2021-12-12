﻿// <auto-generated />
using System;
using FwB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FwB.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211212085323_IsFood")]
    partial class IsFood
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FwB.Models.Discount", b =>
                {
                    b.Property<int>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiscountId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscountName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ItemsItemId")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("Value")
                        .HasColumnType("int");

                    b.HasKey("DiscountId");

                    b.HasIndex("ItemsItemId");

                    b.ToTable("Discount");
                });

            modelBuilder.Entity("FwB.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("DiscountPrice")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("MenusMenuId")
                        .HasColumnType("int");

                    b.Property<double?>("Price")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("isFood")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("MenusMenuId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("FwB.Models.ItemByIdOrder", b =>
                {
                    b.Property<int>("ItemByIdOrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemByIdOrderID"), 1L, 1);

                    b.Property<int?>("ItemsItemId")
                        .HasColumnType("int");

                    b.Property<int?>("OrdersOrderId")
                        .HasColumnType("int");

                    b.HasKey("ItemByIdOrderID");

                    b.HasIndex("ItemsItemId");

                    b.HasIndex("OrdersOrderId");

                    b.ToTable("ItemByIdOrders");
                });

            modelBuilder.Entity("FwB.Models.ListOrder", b =>
                {
                    b.Property<int>("ListOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ListOrderId"), 1L, 1);

                    b.Property<int?>("OrdersOrderId")
                        .HasColumnType("int");

                    b.HasKey("ListOrderId");

                    b.HasIndex("OrdersOrderId");

                    b.ToTable("ListOrder");
                });

            modelBuilder.Entity("FwB.Models.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuId"), 1L, 1);

                    b.Property<int?>("FK_MENUID")
                        .HasColumnType("int");

                    b.Property<string>("MenuName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MenuId");

                    b.HasIndex("FK_MENUID");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("FwB.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FK_DISCOUNTID")
                        .HasColumnType("int");

                    b.Property<int?>("FK_ITEMID")
                        .HasColumnType("int");

                    b.Property<int?>("IdToGetListOrder")
                        .HasColumnType("int");

                    b.Property<int?>("OrderItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("FK_DISCOUNTID");

                    b.HasIndex("FK_ITEMID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("FwB.Models.Discount", b =>
                {
                    b.HasOne("FwB.Models.Item", "Items")
                        .WithMany()
                        .HasForeignKey("ItemsItemId");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("FwB.Models.Item", b =>
                {
                    b.HasOne("FwB.Models.Menu", "Menus")
                        .WithMany()
                        .HasForeignKey("MenusMenuId");

                    b.Navigation("Menus");
                });

            modelBuilder.Entity("FwB.Models.ItemByIdOrder", b =>
                {
                    b.HasOne("FwB.Models.Item", "Items")
                        .WithMany()
                        .HasForeignKey("ItemsItemId");

                    b.HasOne("FwB.Models.Order", "Orders")
                        .WithMany()
                        .HasForeignKey("OrdersOrderId");

                    b.Navigation("Items");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("FwB.Models.ListOrder", b =>
                {
                    b.HasOne("FwB.Models.Order", "Orders")
                        .WithMany()
                        .HasForeignKey("OrdersOrderId");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("FwB.Models.Menu", b =>
                {
                    b.HasOne("FwB.Models.Order", null)
                        .WithMany("Menus")
                        .HasForeignKey("FK_MENUID");
                });

            modelBuilder.Entity("FwB.Models.Order", b =>
                {
                    b.HasOne("FwB.Models.Discount", "Discounts")
                        .WithMany()
                        .HasForeignKey("FK_DISCOUNTID");

                    b.HasOne("FwB.Models.Item", "Items")
                        .WithMany()
                        .HasForeignKey("FK_ITEMID");

                    b.Navigation("Discounts");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("FwB.Models.Order", b =>
                {
                    b.Navigation("Menus");
                });
#pragma warning restore 612, 618
        }
    }
}
