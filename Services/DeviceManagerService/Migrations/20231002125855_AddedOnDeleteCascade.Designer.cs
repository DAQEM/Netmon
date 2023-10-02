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
    [Migration("20231002125855_AddedOnDeleteCascade")]
    partial class AddedOnDeleteCascade
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DevicesLib.DBO.Component.Cpu.Core.CpuCoreDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CpuId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CpuId", "Index")
                        .IsUnique();

                    b.ToTable("CpuCores");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Cpu.Core.CpuCoreMetricsDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CpuCoreId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Load")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CpuCoreId");

                    b.ToTable("CpuCoreMetrics");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Cpu.CpuDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId", "Index")
                        .IsUnique();

                    b.ToTable("Cpus");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Cpu.CpuMetricsDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CpuId")
                        .HasColumnType("char(36)");

                    b.Property<int>("FifteenMinuteLoad")
                        .HasColumnType("int");

                    b.Property<int>("FiveMinuteLoad")
                        .HasColumnType("int");

                    b.Property<int>("OneMinuteLoad")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CpuId");

                    b.ToTable("CpuMetrics");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Disk.DiskDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("MountingPoint")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId", "Index")
                        .IsUnique();

                    b.ToTable("Disks");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Disk.DiskMetricsDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AllocationUnits")
                        .HasColumnType("int");

                    b.Property<Guid>("DiskId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TotalSpace")
                        .HasColumnType("int");

                    b.Property<int>("UsedSpace")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiskId");

                    b.ToTable("DiskMetrics");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Interface.InterfaceDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("MacAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId", "Index")
                        .IsUnique();

                    b.ToTable("Interfaces");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Interface.InterfaceMetricsDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<ulong>("InBroadcastPackets")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("InDiscards")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("InErrors")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("InMulticastPackets")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("InOctets")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("InUnicastPackets")
                        .HasColumnType("bigint unsigned");

                    b.Property<Guid>("InterfaceId")
                        .HasColumnType("char(36)");

                    b.Property<long>("Mtu")
                        .HasColumnType("bigint");

                    b.Property<ulong>("OutBroadcastPackets")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("OutDiscards")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("OutErrors")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("OutMulticastPackets")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("OutOctets")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("OutUnicastPackets")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("PhysAddress")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<uint>("Speed")
                        .HasColumnType("int unsigned");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("InterfaceId");

                    b.ToTable("InterfaceMetrics");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Memory.MemoryDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId", "Index")
                        .IsUnique();

                    b.ToTable("Memory");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Memory.MemoryMetricsDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AllocationUnits")
                        .HasColumnType("int");

                    b.Property<Guid>("MemoryId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TotalSpace")
                        .HasColumnType("int");

                    b.Property<int>("UsedSpace")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MemoryId");

                    b.ToTable("MemoryMetrics");
                });

            modelBuilder.Entity("DevicesLib.DBO.Device.DeviceConnectionDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AuthPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("AuthProtocol")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Community")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ContextName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("PrivacyPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PrivacyProtocol")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SNMPVersion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId")
                        .IsUnique();

                    b.ToTable("DeviceConnections");
                });

            modelBuilder.Entity("DevicesLib.DBO.Device.DeviceDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Contact")
                        .HasColumnType("longtext");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Location")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Cpu.Core.CpuCoreDBO", b =>
                {
                    b.HasOne("DevicesLib.DBO.Component.Cpu.CpuDBO", "Cpu")
                        .WithMany("CpuCores")
                        .HasForeignKey("CpuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cpu");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Cpu.Core.CpuCoreMetricsDBO", b =>
                {
                    b.HasOne("DevicesLib.DBO.Component.Cpu.Core.CpuCoreDBO", "CpuCore")
                        .WithMany("CpuCoreMetrics")
                        .HasForeignKey("CpuCoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CpuCore");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Cpu.CpuDBO", b =>
                {
                    b.HasOne("DevicesLib.DBO.Device.DeviceDBO", "Device")
                        .WithMany("Cpus")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Cpu.CpuMetricsDBO", b =>
                {
                    b.HasOne("DevicesLib.DBO.Component.Cpu.CpuDBO", "Cpu")
                        .WithMany("CpuMetrics")
                        .HasForeignKey("CpuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cpu");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Disk.DiskDBO", b =>
                {
                    b.HasOne("DevicesLib.DBO.Device.DeviceDBO", "Device")
                        .WithMany("Disks")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Disk.DiskMetricsDBO", b =>
                {
                    b.HasOne("DevicesLib.DBO.Component.Disk.DiskDBO", "Disk")
                        .WithMany("DiskMetrics")
                        .HasForeignKey("DiskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disk");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Interface.InterfaceDBO", b =>
                {
                    b.HasOne("DevicesLib.DBO.Device.DeviceDBO", "Device")
                        .WithMany("Interfaces")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Interface.InterfaceMetricsDBO", b =>
                {
                    b.HasOne("DevicesLib.DBO.Component.Interface.InterfaceDBO", "Interface")
                        .WithMany("InterfaceMetrics")
                        .HasForeignKey("InterfaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interface");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Memory.MemoryDBO", b =>
                {
                    b.HasOne("DevicesLib.DBO.Device.DeviceDBO", "Device")
                        .WithMany("Memory")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Memory.MemoryMetricsDBO", b =>
                {
                    b.HasOne("DevicesLib.DBO.Component.Memory.MemoryDBO", "Memory")
                        .WithMany("MemoryMetrics")
                        .HasForeignKey("MemoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Memory");
                });

            modelBuilder.Entity("DevicesLib.DBO.Device.DeviceConnectionDBO", b =>
                {
                    b.HasOne("DevicesLib.DBO.Device.DeviceDBO", "Device")
                        .WithOne("DeviceConnection")
                        .HasForeignKey("DevicesLib.DBO.Device.DeviceConnectionDBO", "DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Cpu.Core.CpuCoreDBO", b =>
                {
                    b.Navigation("CpuCoreMetrics");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Cpu.CpuDBO", b =>
                {
                    b.Navigation("CpuCores");

                    b.Navigation("CpuMetrics");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Disk.DiskDBO", b =>
                {
                    b.Navigation("DiskMetrics");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Interface.InterfaceDBO", b =>
                {
                    b.Navigation("InterfaceMetrics");
                });

            modelBuilder.Entity("DevicesLib.DBO.Component.Memory.MemoryDBO", b =>
                {
                    b.Navigation("MemoryMetrics");
                });

            modelBuilder.Entity("DevicesLib.DBO.Device.DeviceDBO", b =>
                {
                    b.Navigation("Cpus");

                    b.Navigation("DeviceConnection")
                        .IsRequired();

                    b.Navigation("Disks");

                    b.Navigation("Interfaces");

                    b.Navigation("Memory");
                });
#pragma warning restore 612, 618
        }
    }
}
