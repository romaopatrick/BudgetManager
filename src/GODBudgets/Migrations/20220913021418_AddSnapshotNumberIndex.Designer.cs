﻿// <auto-generated />
using System;
using GODBudgets.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GODBudgets.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20220913021418_AddSnapshotNumberIndex")]
    partial class AddSnapshotNumberIndex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GODBudgets.Budget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Details")
                        .HasColumnType("text")
                        .HasColumnName("details");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean")
                        .HasColumnName("enabled");

                    b.Property<long>("OrderNumber")
                        .HasColumnType("bigint")
                        .HasColumnName("order_number");

                    b.Property<decimal?>("ProposedValue")
                        .HasColumnType("numeric")
                        .HasColumnName("proposed_value");

                    b.Property<bool>("SendEmailOnComplete")
                        .HasColumnType("boolean")
                        .HasColumnName("send_email_on_complete");

                    b.Property<long>("SnapshotNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("snapshot_number");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("SnapshotNumber"));

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("Version")
                        .HasColumnType("integer")
                        .HasColumnName("version");

                    b.Property<int?>("WorkingDaysToComplete")
                        .HasColumnType("integer")
                        .HasColumnName("working_days_to_complete");

                    b.HasKey("Id")
                        .HasName("pk_budgets");

                    b.HasAlternateKey("SnapshotNumber")
                        .HasName("ak_budgets_snapshot_number");

                    b.HasIndex("SnapshotNumber")
                        .HasDatabaseName("ix_budgets_snapshot_number");

                    b.ToTable("budgets", (string)null);
                });

            modelBuilder.Entity("GODBudgets.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<string>("CreatedByEmail")
                        .HasColumnType("text")
                        .HasColumnName("created_by_email");

                    b.Property<string>("Snapshot")
                        .HasColumnType("jsonb")
                        .HasColumnName("snapshot");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_events");

                    b.ToTable("events", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
