// <auto-generated />
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
    [Migration("20211209032205_EditOrder1")]
    partial class EditOrder1
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

                    b.Property<int?>("FK_DISCOUNTID")
                        .HasColumnType("int");

                    b.HasKey("DiscountId");

                    b.HasIndex("FK_DISCOUNTID");

                    b.ToTable("Discount");
                });

            modelBuilder.Entity("FwB.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"), 1L, 1);

                    b.Property<int?>("FK_ITEMID")
                        .HasColumnType("int");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("MenuId")
                        .HasColumnType("int");

                    b.Property<float?>("Price")
                        .IsRequired()
                        .HasColumnType("real");

                    b.HasKey("ItemId");

                    b.HasIndex("FK_ITEMID");

                    b.ToTable("Item");
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

                    b.Property<int?>("DiscountId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int?>("MenuId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("FwB.Models.Discount", b =>
                {
                    b.HasOne("FwB.Models.Order", null)
                        .WithMany("Discounts")
                        .HasForeignKey("FK_DISCOUNTID");
                });

            modelBuilder.Entity("FwB.Models.Item", b =>
                {
                    b.HasOne("FwB.Models.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("FK_ITEMID");
                });

            modelBuilder.Entity("FwB.Models.Menu", b =>
                {
                    b.HasOne("FwB.Models.Item", null)
                        .WithMany("Menus")
                        .HasForeignKey("FK_MENUID");

                    b.HasOne("FwB.Models.Order", null)
                        .WithMany("Menus")
                        .HasForeignKey("FK_MENUID");
                });

            modelBuilder.Entity("FwB.Models.Item", b =>
                {
                    b.Navigation("Menus");
                });

            modelBuilder.Entity("FwB.Models.Order", b =>
                {
                    b.Navigation("Discounts");

                    b.Navigation("Items");

                    b.Navigation("Menus");
                });
#pragma warning restore 612, 618
        }
    }
}
