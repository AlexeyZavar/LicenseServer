﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using LicenseServer.Database;

#nullable disable

namespace LicenseServer.Database.Migrations
{
    [DbContext(typeof(XContext))]
    [Migration("20220328075413_AddFiles")]
    partial class AddFiles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LicenseServer.Database.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("LicenseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LicenseId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.License", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Until")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Licenses");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.LicenseFeature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("LicenseId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductFeatureId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LicenseId");

                    b.HasIndex("ProductFeatureId");

                    b.ToTable("LicenseFeatures");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.ProductFeature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductFeatures");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.ProductFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductFiles");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.ProductSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductSettings");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.Event", b =>
                {
                    b.HasOne("LicenseServer.Database.Entities.License", "License")
                        .WithMany()
                        .HasForeignKey("LicenseId");

                    b.Navigation("License");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.License", b =>
                {
                    b.HasOne("LicenseServer.Database.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.LicenseFeature", b =>
                {
                    b.HasOne("LicenseServer.Database.Entities.License", "License")
                        .WithMany("Features")
                        .HasForeignKey("LicenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LicenseServer.Database.Entities.ProductFeature", "ProductFeature")
                        .WithMany()
                        .HasForeignKey("ProductFeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("License");

                    b.Navigation("ProductFeature");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.ProductFeature", b =>
                {
                    b.HasOne("LicenseServer.Database.Entities.Product", "Product")
                        .WithMany("Features")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.ProductFile", b =>
                {
                    b.HasOne("LicenseServer.Database.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.ProductSetting", b =>
                {
                    b.HasOne("LicenseServer.Database.Entities.Product", "Product")
                        .WithMany("Settings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.License", b =>
                {
                    b.Navigation("Features");
                });

            modelBuilder.Entity("LicenseServer.Database.Entities.Product", b =>
                {
                    b.Navigation("Features");

                    b.Navigation("Settings");
                });
#pragma warning restore 612, 618
        }
    }
}
