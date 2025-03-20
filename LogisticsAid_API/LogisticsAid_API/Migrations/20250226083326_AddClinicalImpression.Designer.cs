﻿// <auto-generated />
using System;
using HealthQ_API.Context;
using HealthQ_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HealthQ_API.Migrations
{
    [DbContext(typeof(HealthqDbContext))]
    [Migration("20250226083326_AddClinicalImpression")]
    partial class AddClinicalImpression
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
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_type", new[] { "administrator", "doctor", "patient" });
            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LogisticsAid_API.Entities.Auxiliary.DoctorPatient", b =>
                {
                    b.Property<string>("DoctorId")
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)");

                    b.Property<string>("PatientId")
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)");

                    b.HasKey("DoctorId", "PatientId");

                    b.HasIndex("PatientId");

                    b.ToTable("doctor_patient", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Auxiliary.PatientQuestionnaire", b =>
                {
                    b.Property<string>("PatientId")
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)");

                    b.Property<Guid>("QuestionnaireId")
                        .HasColumnType("uuid");

                    b.HasKey("PatientId", "QuestionnaireId");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("patient_questionnaire", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.ClinicalImpressionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ClinicalImpressionContent")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("questionnaire_content");

                    b.Property<Guid>("QuestionnaireId")
                        .HasColumnType("uuid")
                        .HasColumnName("questionnaire_id");

                    b.HasKey("Id");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("clinical_impressions", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.DoctorModel", b =>
                {
                    b.Property<string>("UserEmail")
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)")
                        .HasColumnName("user_email");

                    b.HasKey("UserEmail");

                    b.ToTable("doctors", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.FileModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content_type");

                    b.Property<byte[]>("FileData")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("file_data");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_name");

                    b.HasKey("Id");

                    b.ToTable("Files", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.PatientModel", b =>
                {
                    b.Property<string>("UserEmail")
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)")
                        .HasColumnName("user_email");

                    b.HasKey("UserEmail");

                    b.ToTable("patients", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.QuestionnaireModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)")
                        .HasColumnName("owner_email");

                    b.Property<string>("QuestionnaireContent")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("questionnaire_content");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("questionnaires", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.UserModel", b =>
                {
                    b.Property<string>("Email")
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)")
                        .HasColumnName("email");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<EGender>("Gender")
                        .HasColumnType("gender")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

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

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)")
                        .HasColumnName("phone_number");

                    b.Property<EUserType>("UserType")
                        .HasColumnType("user_type")
                        .HasColumnName("user_type");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("username");

                    b.HasKey("Email");

                    b.ToTable("users", "public");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Auxiliary.DoctorPatient", b =>
                {
                    b.HasOne("LogisticsAid_API.Entities.DoctorModel", "Doctor")
                        .WithMany("DoctorPatients")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogisticsAid_API.Entities.PatientModel", "Patient")
                        .WithMany("DoctorPatients")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.Auxiliary.PatientQuestionnaire", b =>
                {
                    b.HasOne("LogisticsAid_API.Entities.PatientModel", "Patient")
                        .WithMany("PatientQuestionnaires")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogisticsAid_API.Entities.QuestionnaireModel", "Questionnaire")
                        .WithMany("PatientQuestionnaires")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.ClinicalImpressionModel", b =>
                {
                    b.HasOne("LogisticsAid_API.Entities.QuestionnaireModel", "Questionnaire")
                        .WithMany("ClinicalImpressions")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.DoctorModel", b =>
                {
                    b.HasOne("LogisticsAid_API.Entities.UserModel", "User")
                        .WithOne("Doctor")
                        .HasForeignKey("LogisticsAid_API.Entities.DoctorModel", "UserEmail")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.PatientModel", b =>
                {
                    b.HasOne("LogisticsAid_API.Entities.UserModel", "User")
                        .WithOne("Patient")
                        .HasForeignKey("LogisticsAid_API.Entities.PatientModel", "UserEmail")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.QuestionnaireModel", b =>
                {
                    b.HasOne("LogisticsAid_API.Entities.DoctorModel", "Owner")
                        .WithMany("Questionnaires")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.DoctorModel", b =>
                {
                    b.Navigation("DoctorPatients");

                    b.Navigation("Questionnaires");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.PatientModel", b =>
                {
                    b.Navigation("DoctorPatients");

                    b.Navigation("PatientQuestionnaires");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.QuestionnaireModel", b =>
                {
                    b.Navigation("ClinicalImpressions");

                    b.Navigation("PatientQuestionnaires");
                });

            modelBuilder.Entity("LogisticsAid_API.Entities.UserModel", b =>
                {
                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
