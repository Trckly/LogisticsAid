using LogisticsAid_API.Entities;
using LogisticsAid_API.Entities.Auxiliary;
using Microsoft.EntityFrameworkCore;
namespace LogisticsAid_API.Context;

public sealed class LogisticsAidDbContext : DbContext
{
    public DbSet<ContactInfo> ContactInfo { get; set; }
    public DbSet<Logistician> Logisticians { get; set; }
    public DbSet<CarrierCompany> CarrierCompanies { get; set; }
    public DbSet<CustomerCompany> CustomerCompanies { get; set; }
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
            
            entity.HasIndex(ci => ci.Email)
                .IsUnique();

            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_ContactInfo_SingleCompany",
                    "customer_company_id IS NULL OR carrier_company_id IS NULL");
                
                // t.HasCheckConstraint("CK_ContactInfo_AtLeastOneCompany", 
                //     "customer_company_id IS NOT NULL OR carrier_company_id IS NOT NULL");
            });
        });


        modelBuilder.Entity<Logistician>(entity =>
        {
            entity.HasOne(l => l.ContactInfo)
                .WithMany()
                .HasForeignKey(l => l.ContactId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<CarrierCompany>(entity =>
        {
            entity.HasMany(c => c.Contacts)
                .WithOne(c => c.CarrierCompany)
                .HasForeignKey(c => c.CarrierCompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasMany(c => c.Transport)
                .WithOne(t => t.CarrierCompany)
                .HasForeignKey(t => t.CarrierCompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasMany(c => c.Drivers)
                .WithOne(d => d.CarrierCompany)
                .HasForeignKey(d => d.CarrierCompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CustomerCompany>(entity =>
        {
            entity.HasMany(c => c.Contacts)
                .WithOne(c => c.CustomerCompany)
                .HasForeignKey(c => c.CustomerCompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasOne(d => d.ContactInfo)
                .WithMany()
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(d => d.CarrierCompany)
                .WithMany(c => c.Drivers)
                .HasForeignKey(d => d.CarrierCompanyId)
                .OnDelete(DeleteBehavior.Cascade);

        });

        modelBuilder.Entity<Company>(entity => { });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasOne(t => t.CarrierCompany)
                .WithMany(cc => cc.Transport)
                .HasForeignKey(t => t.CarrierCompanyId)
                .OnDelete(DeleteBehavior.Cascade);
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
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasIndex(t => t.ReadableId)
                .IsUnique();
            
            entity.HasOne(t => t.Logistician)
                .WithMany()
                .HasForeignKey(t => t.LogisticianId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.CarrierCompany)
                .WithMany()
                .HasForeignKey(t => t.CarrierCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.CustomerCompany)
                .WithMany()
                .HasForeignKey(t => t.CustomerCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Driver)
                .WithMany()
                .HasForeignKey(t => t.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Truck)
                .WithMany()
                .HasForeignKey(t => t.TruckId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Trailer)
                .WithMany()
                .HasForeignKey(t => t.TrailerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(t => t.RoutePoints)
                .WithMany(rp => rp.Trips)
                .UsingEntity<RoutePointTrip>(
                    r => r
                        .HasOne<RoutePoint>(rpt => rpt.RoutePoint)
                        .WithMany(rp => rp.RoutePointTrips)
                        .HasForeignKey(rpt => rpt.RoutePointId)
                        .OnDelete(DeleteBehavior.Restrict),
                    l => l
                        .HasOne<Trip>(rpt => rpt.Trip)
                        .WithMany(t => t.RoutePointTrips)
                        .HasForeignKey(rpt => rpt.TripId)
                        .OnDelete(DeleteBehavior.Restrict));
        });
        
        modelBuilder.Entity<RoutePointTrip>(entity => 
            entity.
                HasKey(rpt => new { rpt.RoutePointId, rpt.TripId })
            );
    }
}