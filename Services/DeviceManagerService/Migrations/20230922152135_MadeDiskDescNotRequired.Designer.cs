﻿// <auto-generated />
using System;
using DevicesLib.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DeviceManagerService.Migrations
{
    [DbContext(typeof(DevicesDatabase))]
    [Migration("20230922152135_MadeDiskDescNotRequired")]
    partial class MadeDiskDescNotRequired
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DevicesLib.Entities.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Community")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Contact")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Location")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IpAddress")
                        .IsUnique();

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("DevicesLib.Entities.DeviceOID", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("DeviceOIDs");
                });

            modelBuilder.Entity("DevicesLib.Entities.Disk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<long>("AllocationUnit")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Mount")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("Total")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("Mount", "DeviceId")
                        .IsUnique();

                    b.ToTable("Disks");
                });

            modelBuilder.Entity("DevicesLib.Entities.DiskMetric", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DiskId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<long>("Value")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DiskId");

                    b.ToTable("DiskMetrics");
                });

            modelBuilder.Entity("DevicesLib.Entities.DeviceOID", b =>
                {
                    b.HasOne("DevicesLib.Entities.Device", "Device")
                        .WithMany("OIDs")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("DevicesLib.Entities.Disk", b =>
                {
                    b.HasOne("DevicesLib.Entities.Device", "Device")
                        .WithMany("Disks")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("DevicesLib.Entities.DiskMetric", b =>
                {
                    b.HasOne("DevicesLib.Entities.Disk", "Disk")
                        .WithMany("Metrics")
                        .HasForeignKey("DiskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disk");
                });

            modelBuilder.Entity("DevicesLib.Entities.Device", b =>
                {
                    b.Navigation("Disks");

                    b.Navigation("OIDs");
                });

            modelBuilder.Entity("DevicesLib.Entities.Disk", b =>
                {
                    b.Navigation("Metrics");
                });
#pragma warning restore 612, 618
        }
    }
}
