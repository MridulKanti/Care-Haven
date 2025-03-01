﻿// <auto-generated />
using System;
using CareHaven.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CareHaven.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250212111002_foreignkey")]
    partial class foreignkey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CareHaven.Models.Donation", b =>
                {
                    b.Property<int>("DonationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonationId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DonationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrphanageId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DonationId");

                    b.HasIndex("OrphanageId");

                    b.HasIndex("UserId");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("CareHaven.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FeedbackText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("CareHaven.Models.Orphanage", b =>
                {
                    b.Property<int>("OrphanageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrphanageId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EstablishmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Founder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrphanageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrphanageId");

                    b.ToTable("Orphanages");
                });

            modelBuilder.Entity("CareHaven.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CareHaven.Models.Donation", b =>
                {
                    b.HasOne("CareHaven.Models.Orphanage", "Orphanage")
                        .WithMany()
                        .HasForeignKey("OrphanageId");

                    b.HasOne("CareHaven.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orphanage");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CareHaven.Models.Feedback", b =>
                {
                    b.HasOne("CareHaven.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
