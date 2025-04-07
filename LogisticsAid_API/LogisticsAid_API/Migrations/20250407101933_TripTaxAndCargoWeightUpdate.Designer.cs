﻿// <auto-generated />
using System;
using LogisticsAid_API.Context;
using LogisticsAid_API.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    [DbContext(typeof(LogisticsAidDbContext))]
    [Migration("20250407101933_TripTaxAndCargoWeightUpdate")]
    partial class TripTaxAndCargoWeightUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "gender", new[] { "female", "male", "special" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "route_point_type", new[] { "loading", "unloading" });
            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LogisticsAid_API.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("country");

                    b.Property<double?>("Latitude")
                        .HasColumnType("double precision")
                        .HasColumnName("latitude");

                    b.Property<double?>("Longitude")
                        .HasColumnType("double precision")
                        .HasColumnName("longitude");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("number");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("province");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("street");

                    b.HasKey("Id");

                    b.HasIndex("Country", "Province", "City", "Street", "Number")
                        .IsUnique();

                    b.ToTable("addresses", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Carrier", b =>
                {
                    b.Property<Guid>("ContactId")
                        .HasColumnType("uuid")
                        .HasColumnName("contact_id");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("company_name");

                    b.HasKey("ContactId");

                    b.ToTable("carriers", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Company", b =>
                {
                    b.Property<string>("CompanyName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("company_name");

                    b.HasKey("CompanyName");

                    b.ToTable("companies", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.ContactInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("Email")
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)")
                        .HasColumnName("phone");

                    b.HasKey("Id");

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("contact_info", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Customer", b =>
                {
                    b.Property<Guid>("ContactId")
                        .HasColumnType("uuid")
                        .HasColumnName("contact_id");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("company_name");

                    b.HasKey("ContactId");

                    b.ToTable("customers", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Driver", b =>
                {
                    b.Property<Guid>("ContactId")
                        .HasColumnType("uuid")
                        .HasColumnName("contact_id");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("company_name");

                    b.Property<string>("License")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("licence");

                    b.HasKey("ContactId");

                    b.ToTable("drivers", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Logistician", b =>
                {
                    b.Property<Guid>("ContactId")
                        .HasColumnType("uuid")
                        .HasColumnName("contact_id");

                    b.Property<bool>("HasAdminPrivileges")
                        .HasColumnType("boolean")
                        .HasColumnName("admin_privileges");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("password_hash");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("password_salt");

                    b.HasKey("ContactId");

                    b.ToTable("logisticians", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.RoutePoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid")
                        .HasColumnName("address_id");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("company_name");

                    b.Property<Guid>("ContactInfoId")
                        .HasColumnType("uuid")
                        .HasColumnName("contact_info_id");

                    b.Property<int>("Sequence")
                        .HasColumnType("integer")
                        .HasColumnName("sequence");

                    b.Property<Guid>("TripId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<ERoutePointType>("Type")
                        .HasColumnType("route_point_type")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ContactInfoId");

                    b.HasIndex("TripId");

                    b.ToTable("route_points", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Transport", b =>
                {
                    b.Property<string>("LicencePlate")
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)")
                        .HasColumnName("licence_plate");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("company_name");

                    b.Property<string>("TrailerLicencePlate")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)")
                        .HasColumnName("trailer_licence_plate");

                    b.Property<string>("TruckBrand")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("truck_brand");

                    b.HasKey("LicencePlate");

                    b.ToTable("transport", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Trip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CargoName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("cargo_name");

                    b.Property<decimal>("CargoWeight")
                        .HasColumnType("numeric")
                        .HasColumnName("cargo_weight");

                    b.Property<Guid>("CarrierId")
                        .HasColumnType("uuid")
                        .HasColumnName("carrier_id");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uuid")
                        .HasColumnName("driver_id");

                    b.Property<DateTime>("LoadingDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("loading_date");

                    b.Property<Guid>("LogisticianId")
                        .HasColumnType("uuid")
                        .HasColumnName("logistician_id");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<string>("ReadableId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("readable_id");

                    b.Property<string>("TransportId")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)")
                        .HasColumnName("transport_id");

                    b.Property<DateTime>("UnloadingDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("unloading_date");

                    b.Property<bool>("WithTax")
                        .HasColumnType("boolean")
                        .HasColumnName("with_tax");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId")
                        .IsUnique();

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.HasIndex("DriverId")
                        .IsUnique();

                    b.HasIndex("LogisticianId")
                        .IsUnique();

                    b.HasIndex("TransportId")
                        .IsUnique();

                    b.ToTable("trips", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Carrier", b =>
                {
                    b.HasOne("LogisticsAid_API.Entities.ContactInfo", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Customer", b =>
                {
                    b.HasOne("LogisticsAid_API.Entities.ContactInfo", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Driver", b =>
                {
                    b.HasOne("LogisticsAid_API.Entities.ContactInfo", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Logistician", b =>
                {
                    b.HasOne("LogisticsAid_API.Entities.ContactInfo", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.RoutePoint", b =>
                {
                    b.HasOne("LogisticsAid_API.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LogisticsAid_API.Entities.ContactInfo", "ContactInfo")
                        .WithMany()
                        .HasForeignKey("ContactInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogisticsAid_API.Entities.Trip", "Trip")
                        .WithMany("RoutePoints")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("ContactInfo");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Trip", b =>
                {
                    b.HasOne("LogisticsAid_API.Entities.Carrier", "Carrier")
                        .WithOne()
                        .HasForeignKey("LogisticsAid_API.Entities.Trip", "CarrierId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LogisticsAid_API.Entities.Customer", "Customer")
                        .WithOne()
                        .HasForeignKey("LogisticsAid_API.Entities.Trip", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LogisticsAid_API.Entities.Driver", "Driver")
                        .WithOne()
                        .HasForeignKey("LogisticsAid_API.Entities.Trip", "DriverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LogisticsAid_API.Entities.Logistician", "Logistician")
                        .WithOne()
                        .HasForeignKey("LogisticsAid_API.Entities.Trip", "LogisticianId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LogisticsAid_API.Entities.Transport", "Transport")
                        .WithOne()
                        .HasForeignKey("LogisticsAid_API.Entities.Trip", "TransportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Carrier");

                    b.Navigation("Customer");

                    b.Navigation("Driver");

                    b.Navigation("Logistician");

                    b.Navigation("Transport");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Trip", b =>
                {
                    b.Navigation("RoutePoints");
                });
#pragma warning restore 612, 618
        }
    }
}
