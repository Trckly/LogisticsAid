CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TYPE public.gender AS ENUM ('female', 'male', 'special');
CREATE TYPE public.route_point_type AS ENUM ('loading', 'unloading');
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE public.addresses (
    id uuid NOT NULL,
    number character varying(10) NOT NULL,
    street character varying(100) NOT NULL,
    city character varying(50) NOT NULL,
    province character varying(50) NOT NULL,
    country character varying(50) NOT NULL,
    latitude double precision,
    longitude double precision,
    CONSTRAINT "PK_addresses" PRIMARY KEY (id)
);

CREATE TABLE public.companies (
    company_name character varying(50) NOT NULL,
    CONSTRAINT "PK_companies" PRIMARY KEY (company_name)
);

CREATE TABLE public.contact_info (
    id uuid NOT NULL,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    phone character varying(13) NOT NULL,
    birth_date date,
    email character varying(254),
    CONSTRAINT "PK_contact_info" PRIMARY KEY (id)
);

CREATE TABLE public.transport (
    licence_plate character varying(8) NOT NULL,
    truck_brand character varying(50) NOT NULL,
    trailer_licence_plate character varying(8) NOT NULL,
    company_name character varying(50) NOT NULL,
    CONSTRAINT "PK_transport" PRIMARY KEY (licence_plate),
    CONSTRAINT "FK_transport_companies_company_name" FOREIGN KEY (company_name) REFERENCES public.companies (company_name) ON DELETE CASCADE
);

CREATE TABLE public.carriers (
    contact_id uuid NOT NULL,
    company_name character varying(50) NOT NULL,
    CONSTRAINT "PK_carriers" PRIMARY KEY (contact_id),
    CONSTRAINT "FK_carriers_companies_company_name" FOREIGN KEY (company_name) REFERENCES public.companies (company_name) ON DELETE CASCADE,
    CONSTRAINT "FK_carriers_contact_info_contact_id" FOREIGN KEY (contact_id) REFERENCES public.contact_info (id) ON DELETE CASCADE
);

CREATE TABLE public.customers (
    contact_id uuid NOT NULL,
    company_name character varying(50) NOT NULL,
    CONSTRAINT "PK_customers" PRIMARY KEY (contact_id),
    CONSTRAINT "FK_customers_companies_company_name" FOREIGN KEY (company_name) REFERENCES public.companies (company_name) ON DELETE CASCADE,
    CONSTRAINT "FK_customers_contact_info_contact_id" FOREIGN KEY (contact_id) REFERENCES public.contact_info (id) ON DELETE CASCADE
);

CREATE TABLE public.drivers (
    contact_id uuid NOT NULL,
    licence character varying(50) NOT NULL,
    company_name character varying(50) NOT NULL,
    CONSTRAINT "PK_drivers" PRIMARY KEY (contact_id),
    CONSTRAINT "FK_drivers_companies_company_name" FOREIGN KEY (company_name) REFERENCES public.companies (company_name) ON DELETE CASCADE,
    CONSTRAINT "FK_drivers_contact_info_contact_id" FOREIGN KEY (contact_id) REFERENCES public.contact_info (id) ON DELETE CASCADE
);

CREATE TABLE public.logisticians (
    contact_id uuid NOT NULL,
    password_salt character varying(64) NOT NULL,
    password_hash character varying(128) NOT NULL,
    admin_privileges boolean NOT NULL,
    CONSTRAINT "PK_logisticians" PRIMARY KEY (contact_id),
    CONSTRAINT "FK_logisticians_contact_info_contact_id" FOREIGN KEY (contact_id) REFERENCES public.contact_info (id) ON DELETE CASCADE
);

CREATE TABLE public.orders (
    id uuid NOT NULL,
    readable_id bigint NOT NULL,
    logistician_id uuid NOT NULL,
    carrier_id uuid NOT NULL,
    customer_id uuid NOT NULL,
    driver_id uuid NOT NULL,
    transport_id character varying(8) NOT NULL,
    price numeric NOT NULL,
    CONSTRAINT "PK_orders" PRIMARY KEY (id),
    CONSTRAINT "FK_orders_carriers_carrier_id" FOREIGN KEY (carrier_id) REFERENCES public.carriers (contact_id) ON DELETE CASCADE,
    CONSTRAINT "FK_orders_customers_customer_id" FOREIGN KEY (customer_id) REFERENCES public.customers (contact_id) ON DELETE CASCADE,
    CONSTRAINT "FK_orders_drivers_driver_id" FOREIGN KEY (driver_id) REFERENCES public.drivers (contact_id) ON DELETE CASCADE,
    CONSTRAINT "FK_orders_logisticians_logistician_id" FOREIGN KEY (logistician_id) REFERENCES public.logisticians (contact_id) ON DELETE CASCADE,
    CONSTRAINT "FK_orders_transport_transport_id" FOREIGN KEY (transport_id) REFERENCES public.transport (licence_plate) ON DELETE CASCADE
);

CREATE TABLE public.route_points (
    order_id uuid NOT NULL,
    address_id uuid NOT NULL,
    type route_point_type NOT NULL,
    sequence integer NOT NULL,
    contact_info_id uuid NOT NULL,
    CONSTRAINT "PK_route_points" PRIMARY KEY (order_id),
    CONSTRAINT "FK_route_points_addresses_address_id" FOREIGN KEY (address_id) REFERENCES public.addresses (id) ON DELETE RESTRICT,
    CONSTRAINT "FK_route_points_contact_info_contact_info_id" FOREIGN KEY (contact_info_id) REFERENCES public.contact_info (id) ON DELETE RESTRICT,
    CONSTRAINT "FK_route_points_orders_order_id" FOREIGN KEY (order_id) REFERENCES public.orders (id) ON DELETE CASCADE
);

CREATE INDEX "IX_carriers_company_name" ON public.carriers (company_name);

CREATE UNIQUE INDEX "IX_contact_info_phone" ON public.contact_info (phone);

CREATE INDEX "IX_customers_company_name" ON public.customers (company_name);

CREATE INDEX "IX_drivers_company_name" ON public.drivers (company_name);

CREATE UNIQUE INDEX "IX_orders_carrier_id" ON public.orders (carrier_id);

CREATE UNIQUE INDEX "IX_orders_customer_id" ON public.orders (customer_id);

CREATE UNIQUE INDEX "IX_orders_driver_id" ON public.orders (driver_id);

CREATE UNIQUE INDEX "IX_orders_logistician_id" ON public.orders (logistician_id);

CREATE UNIQUE INDEX "IX_orders_transport_id" ON public.orders (transport_id);

CREATE INDEX "IX_route_points_address_id" ON public.route_points (address_id);

CREATE INDEX "IX_route_points_contact_info_id" ON public.route_points (contact_info_id);

CREATE INDEX "IX_transport_company_name" ON public.transport (company_name);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250321184748_Initial', '9.0.0');

ALTER TABLE public.route_points DROP CONSTRAINT "FK_route_points_orders_order_id";

DROP TABLE public.orders;

ALTER TABLE public.contact_info ALTER COLUMN birth_date TYPE timestamp with time zone;

CREATE TABLE public.trips (
    id uuid NOT NULL,
    readable_id bigint NOT NULL,
    logistician_id uuid NOT NULL,
    carrier_id uuid NOT NULL,
    customer_id uuid NOT NULL,
    driver_id uuid NOT NULL,
    transport_id character varying(8) NOT NULL,
    price numeric NOT NULL,
    CONSTRAINT "PK_trips" PRIMARY KEY (id),
    CONSTRAINT "FK_trips_carriers_carrier_id" FOREIGN KEY (carrier_id) REFERENCES public.carriers (contact_id) ON DELETE CASCADE,
    CONSTRAINT "FK_trips_customers_customer_id" FOREIGN KEY (customer_id) REFERENCES public.customers (contact_id) ON DELETE CASCADE,
    CONSTRAINT "FK_trips_drivers_driver_id" FOREIGN KEY (driver_id) REFERENCES public.drivers (contact_id) ON DELETE CASCADE,
    CONSTRAINT "FK_trips_logisticians_logistician_id" FOREIGN KEY (logistician_id) REFERENCES public.logisticians (contact_id) ON DELETE CASCADE,
    CONSTRAINT "FK_trips_transport_transport_id" FOREIGN KEY (transport_id) REFERENCES public.transport (licence_plate) ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_trips_carrier_id" ON public.trips (carrier_id);

CREATE UNIQUE INDEX "IX_trips_customer_id" ON public.trips (customer_id);

CREATE UNIQUE INDEX "IX_trips_driver_id" ON public.trips (driver_id);

CREATE UNIQUE INDEX "IX_trips_logistician_id" ON public.trips (logistician_id);

CREATE UNIQUE INDEX "IX_trips_transport_id" ON public.trips (transport_id);

ALTER TABLE public.route_points ADD CONSTRAINT "FK_route_points_trips_order_id" FOREIGN KEY (order_id) REFERENCES public.trips (id) ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250329192319_QuickFix', '9.0.0');

ALTER TABLE public.trips ADD date_created timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';

ALTER TABLE public.trips ADD loading_date timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';

ALTER TABLE public.trips ADD unloading_date timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250329194709_TripRework', '9.0.0');

ALTER TABLE public.route_points ADD company_name character varying(100) NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250329195816_RoutePointRework', '9.0.0');

ALTER TABLE public.carriers DROP CONSTRAINT "FK_carriers_companies_company_name";

ALTER TABLE public.customers DROP CONSTRAINT "FK_customers_companies_company_name";

ALTER TABLE public.drivers DROP CONSTRAINT "FK_drivers_companies_company_name";

ALTER TABLE public.transport DROP CONSTRAINT "FK_transport_companies_company_name";

DROP INDEX public."IX_transport_company_name";

DROP INDEX public."IX_drivers_company_name";

DROP INDEX public."IX_customers_company_name";

DROP INDEX public."IX_carriers_company_name";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250331175314_ReworkCompany', '9.0.0');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250401170744_RoutePointFix', '9.0.0');

ALTER TABLE public.trips ALTER COLUMN readable_id TYPE character varying(20);

ALTER TABLE public.trips ADD cargo_name character varying(50) NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250402093507_UpdateTrip', '9.0.0');

ALTER TABLE public.trips DROP CONSTRAINT "FK_trips_carriers_carrier_id";

ALTER TABLE public.trips DROP CONSTRAINT "FK_trips_customers_customer_id";

ALTER TABLE public.trips DROP CONSTRAINT "FK_trips_drivers_driver_id";

ALTER TABLE public.trips DROP CONSTRAINT "FK_trips_logisticians_logistician_id";

ALTER TABLE public.trips DROP CONSTRAINT "FK_trips_transport_transport_id";

ALTER TABLE public.route_points DROP CONSTRAINT "PK_route_points";

ALTER TABLE public.route_points ADD id uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

ALTER TABLE public.route_points ADD CONSTRAINT "PK_route_points" PRIMARY KEY (id);

CREATE INDEX "IX_route_points_order_id" ON public.route_points (order_id);

ALTER TABLE public.trips ADD CONSTRAINT "FK_trips_carriers_carrier_id" FOREIGN KEY (carrier_id) REFERENCES public.carriers (contact_id) ON DELETE RESTRICT;

ALTER TABLE public.trips ADD CONSTRAINT "FK_trips_customers_customer_id" FOREIGN KEY (customer_id) REFERENCES public.customers (contact_id) ON DELETE RESTRICT;

ALTER TABLE public.trips ADD CONSTRAINT "FK_trips_drivers_driver_id" FOREIGN KEY (driver_id) REFERENCES public.drivers (contact_id) ON DELETE RESTRICT;

ALTER TABLE public.trips ADD CONSTRAINT "FK_trips_logisticians_logistician_id" FOREIGN KEY (logistician_id) REFERENCES public.logisticians (contact_id) ON DELETE RESTRICT;

ALTER TABLE public.trips ADD CONSTRAINT "FK_trips_transport_transport_id" FOREIGN KEY (transport_id) REFERENCES public.transport (licence_plate) ON DELETE RESTRICT;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250405154807_TripDeleteBehaviorUpdated', '9.0.0');

ALTER TABLE public.route_points DROP CONSTRAINT "FK_route_points_contact_info_contact_info_id";

CREATE UNIQUE INDEX "IX_addresses_country_province_city_street_number" ON public.addresses (country, province, city, street, number);

ALTER TABLE public.route_points ADD CONSTRAINT "FK_route_points_contact_info_contact_info_id" FOREIGN KEY (contact_info_id) REFERENCES public.contact_info (id) ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250405155428_AddressIndexUpdate', '9.0.0');

ALTER TABLE public.trips ADD cargo_weight numeric NOT NULL DEFAULT 0.0;

ALTER TABLE public.trips ADD with_tax boolean NOT NULL DEFAULT FALSE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250407101933_TripTaxAndCargoWeightUpdate', '9.0.0');

DROP INDEX public."IX_trips_carrier_id";

DROP INDEX public."IX_trips_customer_id";

DROP INDEX public."IX_trips_driver_id";

DROP INDEX public."IX_trips_logistician_id";

DROP INDEX public."IX_trips_transport_id";

CREATE INDEX "IX_trips_carrier_id" ON public.trips (carrier_id);

CREATE INDEX "IX_trips_customer_id" ON public.trips (customer_id);

CREATE INDEX "IX_trips_driver_id" ON public.trips (driver_id);

CREATE INDEX "IX_trips_logistician_id" ON public.trips (logistician_id);

CREATE UNIQUE INDEX "IX_trips_readable_id" ON public.trips (readable_id);

CREATE INDEX "IX_trips_transport_id" ON public.trips (transport_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250407105141_UpdateTripFKRelations', '9.0.0');

ALTER TABLE public.transport RENAME COLUMN trailer_licence_plate TO trailer_license_plate;

ALTER TABLE public.transport RENAME COLUMN licence_plate TO license_plate;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250410072311_FixTypos', '9.0.0');

ALTER TABLE public.trips RENAME COLUMN price TO customer_price;

ALTER TABLE public.trips ADD carrier_price numeric NOT NULL DEFAULT 0.0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250414084929_TripPriceUpdate', '9.0.0');

ALTER TABLE public.route_points DROP CONSTRAINT "FK_route_points_trips_order_id";

DROP INDEX public."IX_route_points_order_id";

ALTER TABLE public.route_points DROP COLUMN order_id;

CREATE TABLE public.route_point_trip (
    route_point_id uuid NOT NULL,
    trip_id uuid NOT NULL,
    CONSTRAINT "PK_route_point_trip" PRIMARY KEY (route_point_id, trip_id),
    CONSTRAINT "FK_route_point_trip_route_points_route_point_id" FOREIGN KEY (route_point_id) REFERENCES public.route_points (id) ON DELETE RESTRICT,
    CONSTRAINT "FK_route_point_trip_trips_trip_id" FOREIGN KEY (trip_id) REFERENCES public.trips (id) ON DELETE RESTRICT
);

CREATE INDEX "IX_route_point_trip_trip_id" ON public.route_point_trip (trip_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250414110807_RoutePointTrip_MTM', '9.0.0');

ALTER TABLE public.route_points DROP CONSTRAINT "FK_route_points_contact_info_contact_info_id";

ALTER TABLE public.trips DROP CONSTRAINT "FK_trips_carriers_carrier_id";

ALTER TABLE public.trips DROP CONSTRAINT "FK_trips_customers_customer_id";

ALTER TABLE public.trips DROP CONSTRAINT "FK_trips_transport_transport_id";

DROP TABLE public.carriers;

DROP TABLE public.customers;

DROP INDEX public."IX_trips_carrier_id";

DROP INDEX public."IX_trips_customer_id";

DROP INDEX public."IX_route_points_contact_info_id";

ALTER TABLE public.trips DROP COLUMN carrier_id;

ALTER TABLE public.trips DROP COLUMN customer_id;

ALTER TABLE public.transport DROP COLUMN company_name;

ALTER TABLE public.transport DROP COLUMN trailer_license_plate;

ALTER TABLE public.transport DROP COLUMN truck_brand;

ALTER TABLE public.route_points DROP COLUMN contact_info_id;

ALTER TABLE public.drivers DROP COLUMN company_name;

ALTER TABLE public.trips RENAME COLUMN transport_id TO truck_id;

ALTER INDEX public."IX_trips_transport_id" RENAME TO "IX_trips_truck_id";

CREATE TYPE public.transport_type AS ENUM ('trailer', 'truck');
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

ALTER TABLE public.trips ADD carrier_company_id character varying(100) NOT NULL DEFAULT '';

ALTER TABLE public.trips ADD customer_company_id character varying(100) NOT NULL DEFAULT '';

ALTER TABLE public.trips ADD trailer_id character varying(8) NOT NULL DEFAULT '';

ALTER TABLE public.transport ADD brand character varying(100);

ALTER TABLE public.transport ADD carrier_company_id character varying(100) NOT NULL DEFAULT '';

ALTER TABLE public.transport ADD type transport_type NOT NULL DEFAULT 'truck'::transport_type;

ALTER TABLE public.route_points ALTER COLUMN company_name DROP NOT NULL;

ALTER TABLE public.route_points ADD additional_info character varying(500);

ALTER TABLE public.drivers ADD carrier_company_id character varying(100) NOT NULL DEFAULT '';

UPDATE public.contact_info SET email = '' WHERE email IS NULL;
ALTER TABLE public.contact_info ALTER COLUMN email SET NOT NULL;
ALTER TABLE public.contact_info ALTER COLUMN email SET DEFAULT '';

ALTER TABLE public.contact_info ADD carrier_company_id character varying(50);

ALTER TABLE public.contact_info ADD customer_company_id character varying(50);

ALTER TABLE public.addresses ALTER COLUMN street DROP NOT NULL;

ALTER TABLE public.addresses ALTER COLUMN province DROP NOT NULL;

ALTER TABLE public.addresses ALTER COLUMN number DROP NOT NULL;

ALTER TABLE public.addresses ALTER COLUMN country DROP NOT NULL;

CREATE TABLE public.carrier_companies (
    company_name character varying(250) NOT NULL,
    CONSTRAINT "PK_carrier_companies" PRIMARY KEY (company_name)
);

CREATE TABLE public.customer_companies (
    company_name character varying(250) NOT NULL,
    CONSTRAINT "PK_customer_companies" PRIMARY KEY (company_name)
);

CREATE INDEX "IX_trips_carrier_company_id" ON public.trips (carrier_company_id);

CREATE INDEX "IX_trips_customer_company_id" ON public.trips (customer_company_id);

CREATE INDEX "IX_trips_trailer_id" ON public.trips (trailer_id);

CREATE INDEX "IX_transport_carrier_company_id" ON public.transport (carrier_company_id);

CREATE INDEX "IX_drivers_carrier_company_id" ON public.drivers (carrier_company_id);

CREATE INDEX "IX_contact_info_carrier_company_id" ON public.contact_info (carrier_company_id);

CREATE INDEX "IX_contact_info_customer_company_id" ON public.contact_info (customer_company_id);

CREATE UNIQUE INDEX "IX_contact_info_email" ON public.contact_info (email);

ALTER TABLE public.contact_info ADD CONSTRAINT "CK_ContactInfo_SingleCompany" CHECK (customer_company_id IS NULL OR carrier_company_id IS NULL);

ALTER TABLE public.contact_info ADD CONSTRAINT "FK_contact_info_carrier_companies_carrier_company_id" FOREIGN KEY (carrier_company_id) REFERENCES public.carrier_companies (company_name) ON DELETE CASCADE;

ALTER TABLE public.contact_info ADD CONSTRAINT "FK_contact_info_customer_companies_customer_company_id" FOREIGN KEY (customer_company_id) REFERENCES public.customer_companies (company_name) ON DELETE CASCADE;

ALTER TABLE public.drivers ADD CONSTRAINT "FK_drivers_carrier_companies_carrier_company_id" FOREIGN KEY (carrier_company_id) REFERENCES public.carrier_companies (company_name) ON DELETE CASCADE;

ALTER TABLE public.transport ADD CONSTRAINT "FK_transport_carrier_companies_carrier_company_id" FOREIGN KEY (carrier_company_id) REFERENCES public.carrier_companies (company_name) ON DELETE CASCADE;

ALTER TABLE public.trips ADD CONSTRAINT "FK_trips_carrier_companies_carrier_company_id" FOREIGN KEY (carrier_company_id) REFERENCES public.carrier_companies (company_name) ON DELETE RESTRICT;

ALTER TABLE public.trips ADD CONSTRAINT "FK_trips_customer_companies_customer_company_id" FOREIGN KEY (customer_company_id) REFERENCES public.customer_companies (company_name) ON DELETE RESTRICT;

ALTER TABLE public.trips ADD CONSTRAINT "FK_trips_transport_trailer_id" FOREIGN KEY (trailer_id) REFERENCES public.transport (license_plate) ON DELETE RESTRICT;

ALTER TABLE public.trips ADD CONSTRAINT "FK_trips_transport_truck_id" FOREIGN KEY (truck_id) REFERENCES public.transport (license_plate) ON DELETE RESTRICT;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250418170053_TripRelatedEntitiesOverhaul', '9.0.0');

ALTER TABLE public.contact_info DROP CONSTRAINT "FK_contact_info_carrier_companies_carrier_company_id";

ALTER TABLE public.contact_info DROP CONSTRAINT "FK_contact_info_customer_companies_customer_company_id";

DROP INDEX public."IX_contact_info_carrier_company_id";

DROP INDEX public."IX_contact_info_customer_company_id";

ALTER TABLE public.contact_info DROP COLUMN carrier_company_id;

ALTER TABLE public.contact_info DROP COLUMN customer_company_id;

ALTER TABLE public.contact_info ADD "CarrierCompanyCompanyName" character varying(250);

ALTER TABLE public.contact_info ADD "CustomerCompanyCompanyName" character varying(250);

CREATE INDEX "IX_contact_info_CarrierCompanyCompanyName" ON public.contact_info ("CarrierCompanyCompanyName");

CREATE INDEX "IX_contact_info_CustomerCompanyCompanyName" ON public.contact_info ("CustomerCompanyCompanyName");

ALTER TABLE public.contact_info ADD CONSTRAINT "FK_contact_info_carrier_companies_CarrierCompanyCompanyName" FOREIGN KEY ("CarrierCompanyCompanyName") REFERENCES public.carrier_companies (company_name);

ALTER TABLE public.contact_info ADD CONSTRAINT "FK_contact_info_customer_companies_CustomerCompanyCompanyName" FOREIGN KEY ("CustomerCompanyCompanyName") REFERENCES public.customer_companies (company_name);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250428083409_CompanyFromContactInfoRemoved', '9.0.0');

ALTER TABLE public.drivers DROP CONSTRAINT "FK_drivers_carrier_companies_carrier_company_id";

ALTER TABLE public.transport DROP CONSTRAINT "FK_transport_carrier_companies_carrier_company_id";

DROP INDEX public."IX_transport_carrier_company_id";

DROP INDEX public."IX_drivers_carrier_company_id";

ALTER TABLE public.transport DROP COLUMN carrier_company_id;

ALTER TABLE public.drivers DROP COLUMN carrier_company_id;

ALTER TABLE public.transport ADD "CarrierCompanyCompanyName" character varying(250);

ALTER TABLE public.drivers ADD "CarrierCompanyCompanyName" character varying(250);

CREATE INDEX "IX_transport_CarrierCompanyCompanyName" ON public.transport ("CarrierCompanyCompanyName");

CREATE INDEX "IX_drivers_CarrierCompanyCompanyName" ON public.drivers ("CarrierCompanyCompanyName");

ALTER TABLE public.drivers ADD CONSTRAINT "FK_drivers_carrier_companies_CarrierCompanyCompanyName" FOREIGN KEY ("CarrierCompanyCompanyName") REFERENCES public.carrier_companies (company_name);

ALTER TABLE public.transport ADD CONSTRAINT "FK_transport_carrier_companies_CarrierCompanyCompanyName" FOREIGN KEY ("CarrierCompanyCompanyName") REFERENCES public.carrier_companies (company_name);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250428093550_CircularDependencyRemove', '9.0.0');

COMMIT;

