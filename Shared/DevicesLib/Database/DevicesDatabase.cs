using DevicesLib.DBO.Component.Cpu;
using DevicesLib.DBO.Component.Cpu.Core;
using DevicesLib.DBO.Component.Disk;
using DevicesLib.DBO.Component.Interface;
using DevicesLib.DBO.Component.Memory;
using DevicesLib.DBO.Device;
using Microsoft.EntityFrameworkCore;

namespace DevicesLib.Database;

public class DevicesDatabase : DbContext
{
    public DevicesDatabase(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<DeviceDBO> Devices { get; set; }
    public DbSet<DeviceConnectionDBO> DeviceConnections { get; set; }
    
    public DbSet<CpuDBO> Cpus { get; set; }
    public DbSet<CpuMetricsDBO> CpuMetrics { get; set; }
    public DbSet<CpuCoreDBO> CpuCores { get; set; }
    public DbSet<CpuCoreMetricsDBO> CpuCoreMetrics { get; set; }
    
    public DbSet<DiskDBO> Disks { get; set; }
    public DbSet<DiskMetricsDBO> DiskMetrics { get; set; }
    
    public DbSet<InterfaceDBO> Interfaces { get; set; }
    public DbSet<InterfaceMetricsDBO> InterfaceMetrics { get; set; }
    
    public DbSet<MemoryDBO> Memory { get; set; }
    public DbSet<MemoryMetricsDBO> MemoryMetrics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeviceConnectionDBO>()
            .HasOne(d => d.Device)
            .WithOne(d => d.DeviceConnection)
            .HasForeignKey<DeviceConnectionDBO>(d => d.DeviceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<CpuDBO>()
            .HasOne(c => c.Device)
            .WithMany(c => c.Cpus)
            .HasForeignKey(c => c.DeviceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<CpuMetricsDBO>()
            .HasOne(c => c.Cpu)
            .WithMany(c => c.CpuMetrics)
            .HasForeignKey(c => c.CpuId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<CpuCoreDBO>()
            .HasOne(c => c.Cpu)
            .WithMany(c => c.CpuCores)
            .HasForeignKey(c => c.CpuId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<CpuCoreMetricsDBO>()
            .HasOne(c => c.CpuCore)
            .WithMany(c => c.CpuCoreMetrics)
            .HasForeignKey(c => c.CpuCoreId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<DiskDBO>()
            .HasOne(d => d.Device)
            .WithMany(d => d.Disks)
            .HasForeignKey(d => d.DeviceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<DiskMetricsDBO>()
            .HasOne(d => d.Disk)
            .WithMany(d => d.DiskMetrics)
            .HasForeignKey(d => d.DiskId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<InterfaceDBO>()
            .HasOne(i => i.Device)
            .WithMany(i => i.Interfaces)
            .HasForeignKey(i => i.DeviceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<InterfaceMetricsDBO>()
            .HasOne(i => i.Interface)
            .WithMany(i => i.InterfaceMetrics)
            .HasForeignKey(i => i.InterfaceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<MemoryDBO>()
            .HasOne(m => m.Device)
            .WithMany(m => m.Memory)
            .HasForeignKey(m => m.DeviceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<MemoryMetricsDBO>()
            .HasOne(m => m.Memory)
            .WithMany(m => m.MemoryMetrics)
            .HasForeignKey(m => m.MemoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<CpuDBO>()
            .HasIndex(core => new { core.DeviceId, core.Index })
            .IsUnique();
        
        modelBuilder.Entity<CpuCoreDBO>()
            .HasIndex(core => new { core.CpuId, core.Index })
            .IsUnique();
        
        modelBuilder.Entity<DiskDBO>()
            .HasIndex(disk => new { disk.DeviceId, disk.Index })
            .IsUnique();
        
        modelBuilder.Entity<InterfaceDBO>()
            .HasIndex(@interface => new { @interface.DeviceId, @interface.Index })
            .IsUnique();
        
        modelBuilder.Entity<MemoryDBO>()
            .HasIndex(memory => new { memory.DeviceId, memory.Index })
            .IsUnique();

    }
}