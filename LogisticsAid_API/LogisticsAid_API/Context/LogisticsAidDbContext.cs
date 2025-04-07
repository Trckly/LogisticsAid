﻿using LogisticsAid_API.Entities;
using Microsoft.EntityFrameworkCore;
namespace LogisticsAid_API.Context;

public sealed class LogisticsAidDbContext : DbContext
{
    public DbSet<ContactInfo> ContactInfo { get; set; }
    public DbSet<Logistician> Logisticians { get; set; }
    public DbSet<Carrier> Carriers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Transport> Transport { get; set; }
    public DbSet<Trip> Trips { get; set; }
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
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasOne(c => c.Contact)
                .WithMany()
                .HasForeignKey(c => c.ContactId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasOne(d => d.Contact)
                .WithMany()
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade);

        });

        modelBuilder.Entity<Company>(entity => { });

        modelBuilder.Entity<Transport>(entity =>
        {
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity
                .HasIndex(address => new
                    { address.Country, address.Province, address.City, address.Street, address.Number })
                .IsUnique();
        });

        modelBuilder.Entity<RoutePoint>(entity =>
        {
            entity.HasOne(rp => rp.Address)
                .WithMany()
                .HasForeignKey(rp => rp.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(rp => rp.ContactInfo)
                .WithMany()
                .HasForeignKey(rp => rp.ContactInfoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasIndex(t => t.ReadableId)
                .IsUnique();
            
            entity.HasOne(t => t.Logistician)
                .WithMany()
                .HasForeignKey(t => t.LogisticianId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Carrier)
                .WithMany()
                .HasForeignKey(t => t.CarrierId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Customer)
                .WithMany()
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Driver)
                .WithMany()
                .HasForeignKey(t => t.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Transport)
                .WithMany()
                .HasForeignKey(t => t.TransportId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(o => o.RoutePoints)
                .WithOne(rp => rp.Trip)
                .HasForeignKey(rp => rp.TripId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}