using AccountService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Database;

public class AccountDatabase : DbContext
{
    public AccountDatabase(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<User> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Session> Sessions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users);
        
        modelBuilder.Entity<Role>()
            .HasMany(r => r.Users)
            .WithMany(u => u.Roles);
        
        modelBuilder.Entity<Session>()
            .HasOne(s => s.User)
            .WithMany(u => u.Sessions)
            .HasForeignKey(s => s.UserId);
    }
}