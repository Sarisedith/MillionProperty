using Microsoft.EntityFrameworkCore;
using Million.PropertyManagement.Domain.Entities;

namespace Million.PropertyManagement.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<PropertyImage> PropertyImages => Set<PropertyImage>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<PropertyTrace> PropertyTraces => Set<PropertyTrace>();
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Owner>(e =>
        {
            e.ToTable("Owners");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(200).IsRequired();
            e.Property(x => x.Address).HasMaxLength(300);
        });

        builder.Entity<Property>(e =>
        {
            e.ToTable("Properties");
            e.HasKey(x => x.Id);
            e.Property(x => x.Address).HasMaxLength(200).IsRequired();
            e.Property(x => x.City).HasMaxLength(100).IsRequired();
            e.Property(x => x.Price).HasColumnType("decimal(18,2)");
            e.HasMany(x => x.Images).WithOne().HasForeignKey(i => i.PropertyId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.Traces).WithOne().HasForeignKey(t => t.PropertyId).OnDelete(DeleteBehavior.Cascade);
            e.HasQueryFilter(p => !p.IsDeleted);
            e.HasIndex(x => new { x.City, x.Price });
        });

        builder.Entity<PropertyImage>(e =>
        {
            e.ToTable("PropertyImages");
            e.HasKey(x => x.Id);
            e.Property(x => x.Url).HasMaxLength(500).IsRequired();
        });

        builder.Entity<PropertyTrace>(e =>
        {
            e.ToTable("PropertyTraces");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(200).IsRequired();
            e.Property(x => x.Value).HasColumnType("decimal(18,2)");
            e.Property(x => x.Tax).HasColumnType("decimal(18,2)");
        });

        builder.Entity<User>(e =>
        {
            e.ToTable("Users");
            e.HasKey(x => x.Id);
            e.Property(x => x.Username).HasMaxLength(100).IsRequired();
            e.Property(x => x.PasswordHash).HasMaxLength(255).IsRequired();
            e.Property(x => x.Role).HasMaxLength(50).IsRequired();
        });
    }
}
