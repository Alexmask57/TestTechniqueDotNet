﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestTechniqueDotnet.Context;

#nullable disable

namespace TestTechniqueDotnet.Migrations
{
    [DbContext(typeof(PetStoreContext))]
    [Migration("20240205101548_Update")]
    partial class Update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TestTechniqueDotnet.Models.Animal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("MasterId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("TransactionId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("TestTechniqueDotnet.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("TestTechniqueDotnet.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AnimalId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId")
                        .IsUnique();

                    b.HasIndex("ClientId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("TestTechniqueDotnet.Models.Animal", b =>
                {
                    b.HasOne("TestTechniqueDotnet.Models.Client", null)
                        .WithMany("Animals")
                        .HasForeignKey("TransactionId");
                });

            modelBuilder.Entity("TestTechniqueDotnet.Models.Transaction", b =>
                {
                    b.HasOne("TestTechniqueDotnet.Models.Animal", null)
                        .WithOne()
                        .HasForeignKey("TestTechniqueDotnet.Models.Transaction", "AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestTechniqueDotnet.Models.Client", null)
                        .WithMany("Transactions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestTechniqueDotnet.Models.Client", b =>
                {
                    b.Navigation("Animals");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}