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
    public DbSet<RoutePointTrip> RoutePointTrip { get; set; }
    public DbSet<ContactInfoCustomerCompany> ContactInfoCustomerCompany { get; set; }
    public DbSet<ContactInfoCarrierCompany> ContactInfoCarrierCompany { get; set; }


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

            entity.HasMany(ci => ci.CustomerCompanies)
                .WithMany(cc => cc.Contacts)
                .UsingEntity<ContactInfoCustomerCompany>(
                    r => r
                        .HasOne<CustomerCompany>(cicc => cicc.CustomerCompany)
                        .WithMany(cc => cc.ContactInfoCustomerCompanies)
                        .HasForeignKey(cicc => cicc.CustomerCompanyId)
                        .OnDelete(DeleteBehavior.Restrict),
                    l => l
                        .HasOne<ContactInfo>(cicc => cicc.ContactInfo)
                        .WithMany(ci => ci.ContactInfoCustomerCompany)
                        .HasForeignKey(cicc => cicc.ContactInfoId)
                        .OnDelete(DeleteBehavior.Restrict));
            
            entity.HasMany(ci => ci.CarrierCompanies)
                .WithMany(cc => cc.Contacts)
                .UsingEntity<ContactInfoCarrierCompany>(
                    r => r
                        .HasOne<CarrierCompany>(cicc => cicc.CarrierCompany)
                        .WithMany(cc => cc.ContactInfoCarrierCompany)
                        .HasForeignKey(cicc => cicc.CarrierCompanyId)
                        .OnDelete(DeleteBehavior.Restrict),
                    l => l
                        .HasOne<ContactInfo>(cicc => cicc.ContactInfo)
                        .WithMany(ci => ci.ContactInfoCarrierCompany)
                        .HasForeignKey(cicc => cicc.ContactInfoId)
                        .OnDelete(DeleteBehavior.Restrict));
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
            entity.HasMany(cc => cc.Contacts)
                .WithMany(ci => ci.CarrierCompanies)
                .UsingEntity<ContactInfoCarrierCompany>(
                    r => r
                        .HasOne<ContactInfo>(cicc => cicc.ContactInfo)
                        .WithMany(ci => ci.ContactInfoCarrierCompany)
                        .HasForeignKey(cicc => cicc.ContactInfoId)
                        .OnDelete(DeleteBehavior.Restrict),
                    l => l
                        .HasOne<CarrierCompany>(cicc => cicc.CarrierCompany)
                        .WithMany(cc => cc.ContactInfoCarrierCompany)
                        .HasForeignKey(cicc => cicc.CarrierCompanyId)
                        .OnDelete(DeleteBehavior.Restrict));
        });

        modelBuilder.Entity<CustomerCompany>(entity =>
        {
            entity.HasMany(ci => ci.Contacts)
                .WithMany(cc => cc.CustomerCompanies)
                .UsingEntity<ContactInfoCustomerCompany>(
                    r => r
                        .HasOne<ContactInfo>(cicc => cicc.ContactInfo)
                        .WithMany(ci => ci.ContactInfoCustomerCompany)
                        .HasForeignKey(cicc => cicc.ContactInfoId)
                        .OnDelete(DeleteBehavior.Restrict),
                    l => l
                        .HasOne<CustomerCompany>(cicc => cicc.CustomerCompany)
                        .WithMany(cc => cc.ContactInfoCustomerCompanies)
                        .HasForeignKey(cicc => cicc.CustomerCompanyId)
                        .OnDelete(DeleteBehavior.Restrict));
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasOne(d => d.ContactInfo)
                .WithMany()
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(d => d.CarrierCompany)
                .WithMany()
                .HasForeignKey(d => d.CarrierCompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Company>();

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasOne(d => d.CarrierCompany)
                .WithMany()
                .HasForeignKey(d => d.CarrierCompanyId)
                .OnDelete(DeleteBehavior.Restrict);
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
            
            entity.HasMany(t => t.Trips)
                .WithMany(rp => rp.RoutePoints)
                .UsingEntity<RoutePointTrip>(
                    r => r
                        .HasOne<Trip>(rpt => rpt.Trip)
                        .WithMany(rp => rp.RoutePointTrips)
                        .HasForeignKey(rpt => rpt.TripId)
                        .OnDelete(DeleteBehavior.Cascade),
                    l => l
                        .HasOne<RoutePoint>(rpt => rpt.RoutePoint)
                        .WithMany(t => t.RoutePointTrips)
                        .HasForeignKey(rpt => rpt.RoutePointId)
                        .OnDelete(DeleteBehavior.Restrict));
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
                        .OnDelete(DeleteBehavior.Cascade));
            
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_Trip_DateValidation", 
                "\"loading_date\" <= \"unloading_date\""));
        });
        
        modelBuilder.Entity<RoutePointTrip>(entity =>
        {
            entity.HasKey(rpt => new { rpt.RoutePointId, rpt.TripId });
            
            entity.HasOne(rpt => rpt.RoutePoint)
                .WithMany(rp => rp.RoutePointTrips)
                .HasForeignKey(rpt => rpt.RoutePointId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(rpt => rpt.Trip)
                .WithMany(t => t.RoutePointTrips)
                .HasForeignKey(rpt => rpt.TripId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ContactInfoCustomerCompany>(entity =>
        {
            entity.HasKey(cicc => new {cicc.ContactInfoId, cicc.CustomerCompanyId});
            
            entity.HasOne(cicc => cicc.ContactInfo)
                .WithMany(ci => ci.ContactInfoCustomerCompany)
                .HasForeignKey(cicc => cicc.ContactInfoId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(cicc => cicc.CustomerCompany)
                .WithMany(cc => cc.ContactInfoCustomerCompanies)
                .HasForeignKey(cicc => cicc.CustomerCompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ContactInfoCarrierCompany>(entity =>
        {
            entity.HasKey(cicc => new {cicc.ContactInfoId, cicc.CarrierCompanyId});
            
            entity.HasOne(cicc => cicc.ContactInfo)
                .WithMany(ci => ci.ContactInfoCarrierCompany)
                .HasForeignKey(cicc => cicc.ContactInfoId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(cicc => cicc.CarrierCompany)
                .WithMany(cc => cc.ContactInfoCarrierCompany)
                .HasForeignKey(cicc => cicc.CarrierCompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}