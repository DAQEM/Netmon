using DevicesLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevicesLib.Database;

public class DevicesDatabase : DbContext
{
    public DevicesDatabase(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<DeviceDTO> Devices { get; set; }
    
    public DbSet<DeviceOIDDTO> DeviceOIDs { get; set; }
    
    public DbSet<DiskDTO> Disks { get; set; }
    public DbSet<DiskMetricDTO> DiskMetrics { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeviceDTO>()
            .HasMany(d => d.OIDs)
            .WithOne(d => d.Device)
            .HasForeignKey(d => d.DeviceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<DeviceDTO>()
            .HasMany(d => d.Disks)
            .WithOne(d => d.Device)
            .HasForeignKey(d => d.DeviceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<DiskDTO>()
            .HasMany(d => d.Metrics)
            .WithOne(d => d.Disk)
            .HasForeignKey(d => d.DiskId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<DiskDTO>()
            .HasIndex(d => new { Mount = d.MountingPoint, d.DeviceId })
            .IsUnique();
        
        modelBuilder.Entity<DeviceDTO>()
            .HasIndex(d => d.IpAddress)
            .IsUnique();
    }
}