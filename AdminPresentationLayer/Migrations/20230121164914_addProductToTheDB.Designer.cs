﻿// <auto-generated />
using System;
using AdminPresentationLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AdminPresentationLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230121164914_addProductToTheDB")]
    partial class addProductToTheDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AdminPresentationLayer.Models.Brand", b =>
                {
                    b.Property<int>("idBrand")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idBrand"));

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("info")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idBrand");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("AdminPresentationLayer.Models.Category", b =>
                {
                    b.Property<int>("idCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idCategory"));

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("info")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idCategory");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("AdminPresentationLayer.Models.Product", b =>
                {
                    b.Property<int>("idProduct")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idProduct"));

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<int>("idBrand")
                        .HasColumnType("int");

                    b.Property<int>("idCategory")
                        .HasColumnType("int");

                    b.Property<string>("imageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("info")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nameProduct")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<DateTime>("registerDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("stockQuantity")
                        .HasColumnType("int");

                    b.HasKey("idProduct");

                    b.HasIndex("idBrand");

                    b.HasIndex("idCategory");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AdminPresentationLayer.Models.Product", b =>
                {
                    b.HasOne("AdminPresentationLayer.Models.Brand", "brand")
                        .WithMany()
                        .HasForeignKey("idBrand")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminPresentationLayer.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("idCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("brand");
                });
#pragma warning restore 612, 618
        }
    }
}
