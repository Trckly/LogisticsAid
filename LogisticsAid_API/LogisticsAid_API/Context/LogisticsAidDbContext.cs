using LogisticsAid_API.Entities;
using Microsoft.EntityFrameworkCore;
namespace LogisticsAid_API.Context;

public sealed class LogisticsAidDbContext : DbContext
{
    public DbSet<ContactInfo> ContactInfos { get; set; }
    public DbSet<Logistician> Logisticians { get; set; }
    public DbSet<Carrier> Carriers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Transport> Transport { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<RoutePoint> RoutePoints { get; set; }


    public LogisticsAidDbContext()
    {
        Database.EnsureCreated();
    }

    public LogisticsAidDbContext(DbContextOptions<LogisticsAidDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("public");
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<ContactInfo>(entity =>
        {
            entity.HasIndex(ci => ci.Phone)
                .IsUnique();
        });


        modelBuilder.Entity<Logistician>(entity =>
        {
            entity.HasOne(l => l.Contact)
                .WithMany()
                .HasForeignKey(l => l.ContactId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<Carrier>(entity =>
        {
            entity.HasOne(c => c.Contact)
                .WithMany()
                .HasForeignKey(l => l.ContactId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.Company)
                .WithMany()
                .HasForeignKey(l => l.CompanyName)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasOne(c => c.Contact)
                .WithMany()
                .HasForeignKey(c => c.ContactId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.Company)
                .WithMany()
                .HasForeignKey(c => c.CompanyName)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasOne(d => d.Contact)
                .WithMany()
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Company)
                .WithMany()
                .HasForeignKey(d => d.CompanyName)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Company>(entity => { });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasOne(t => t.Company)
                .WithMany()
                .HasForeignKey(t => t.CompanyName)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Address>(entity => { });

        modelBuilder.Entity<RoutePoint>(entity =>
        {
            entity.HasOne(rp => rp.Address)
                .WithMany()
                .HasForeignKey(rp => rp.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(rp => rp.ContactInfo)
                .WithMany()
                .HasForeignKey(rp => rp.ContactInfoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne(t => t.Logistician)
                .WithOne()
                .HasForeignKey<Order>(t => t.LogisticianId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.Carrier)
                .WithOne()
                .HasForeignKey<Order>(t => t.CarrierId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.Customer)
                .WithOne()
                .HasForeignKey<Order>(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.Driver)
                .WithOne()
                .HasForeignKey<Order>(t => t.DriverId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.Transport)
                .WithOne()
                .HasForeignKey<Order>(t => t.TransportId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(o => o.RoutePoints)
                .WithOne(rp => rp.Order)
                .HasForeignKey(rp => rp.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}