using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<AddressModel> Addresses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
            entity.HasIndex(u => u.Username).IsUnique();
            entity.Property(u => u.Password).IsRequired();
        });

        modelBuilder.Entity<AddressModel>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(a => new { a.Name, a.UserId }).IsUnique();
            entity.Property(a => a.CEP).IsRequired().HasMaxLength(8);
            entity.Property(a => a.PublicPlace).IsRequired().HasMaxLength(200);
            entity.Property(a => a.Complement).HasMaxLength(100);
            entity.Property(a => a.District).IsRequired().HasMaxLength(100);
            entity.Property(a => a.City).IsRequired().HasMaxLength(100);
            entity.Property(a => a.FederalUnit).IsRequired().HasMaxLength(2);
            entity.Property(a => a.Number).IsRequired().HasMaxLength(10);

            entity.HasOne(a => a.User)
                  .WithMany(u => u.Addresses)
                  .HasForeignKey(a => a.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
