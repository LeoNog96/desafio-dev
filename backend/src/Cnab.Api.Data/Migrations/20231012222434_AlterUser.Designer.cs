﻿// <auto-generated />
using System;
using Cnab.Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cnab.Api.Data.Migrations
{
    [DbContext(typeof(CnabContext))]
    [Migration("20231012222434_AlterUser")]
    partial class AlterUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cnab.Api.Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Card")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)")
                        .HasColumnName("card");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)")
                        .HasColumnName("cpf");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<bool>("Removed")
                        .HasColumnType("boolean")
                        .HasColumnName("removed");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("removed_at");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasMaxLength(19)
                        .HasColumnType("character varying(19)")
                        .HasColumnName("store_name");

                    b.Property<string>("StoreOwner")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("store_owner");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UploadedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("uploaded_by");

                    b.Property<double>("Value")
                        .HasColumnType("double precision")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("Card");

                    b.HasIndex("Cpf");

                    b.HasIndex("Date");

                    b.HasIndex("StoreName");

                    b.HasIndex("StoreOwner");

                    b.HasIndex("Type");

                    b.HasIndex("UploadedBy");

                    b.HasIndex("UploadedBy", "Date");

                    b.HasIndex("UploadedBy", "Date", "Cpf");

                    b.HasIndex("UploadedBy", "Date", "Cpf", "Type");

                    b.ToTable("transactions", (string)null);
                });

            modelBuilder.Entity("Cnab.Api.Domain.Entities.TransactionTypes", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Kind")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)")
                        .HasColumnName("kind");

                    b.Property<char>("Signal")
                        .HasMaxLength(1)
                        .HasColumnType("character(1)")
                        .HasColumnName("signal");

                    b.HasKey("Id");

                    b.HasIndex("Kind");

                    b.ToTable("transaction_types", (string)null);
                });

            modelBuilder.Entity("Cnab.Api.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("password");

                    b.Property<bool>("Removed")
                        .HasColumnType("boolean")
                        .HasColumnName("removed");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("removed_at");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Login");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Cnab.Api.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Cnab.Api.Domain.Entities.TransactionTypes", "TransactionType")
                        .WithMany("Transactions")
                        .HasForeignKey("Type")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Cnab.Api.Domain.Entities.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UploadedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("TransactionType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cnab.Api.Domain.Entities.TransactionTypes", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Cnab.Api.Domain.Entities.User", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
