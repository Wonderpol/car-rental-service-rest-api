﻿// <auto-generated />
using System;
using CarRentalRestApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRentalRestApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220521102850_AddedKeyAttribute")]
    partial class AddedKeyAttribute
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("CarRentalRestApi.Models.Auth.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("CarRentalRestApi.Models.Auth.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CarRentalRestApi.Models.RentVehicleModels.Rent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("EndRentTimestamp")
                        .HasColumnType("INTEGER");

                    b.Property<long>("StartRentTimestamp")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("CarRentalRestApi.Models.VehicleModels.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("HorsePower")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Millage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .HasColumnType("TEXT");

                    b.Property<double>("PricePerDay")
                        .HasColumnType("REAL");

                    b.Property<string>("RegistrationPlate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TypeOfVehicle")
                        .HasColumnType("INTEGER");

                    b.Property<long>("VinNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Vehicle");
                });

            modelBuilder.Entity("CarRentalRestApi.Models.VehicleModels.Car", b =>
                {
                    b.HasBaseType("CarRentalRestApi.Models.VehicleModels.Vehicle");

                    b.Property<double>("Acceleration")
                        .HasColumnType("REAL");

                    b.Property<string>("ChassisType")
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TransmissionType")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Car");
                });

            modelBuilder.Entity("CarRentalRestApi.Models.VehicleModels.Caravan", b =>
                {
                    b.HasBaseType("CarRentalRestApi.Models.VehicleModels.Vehicle");

                    b.Property<double>("Height")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsBathroomInside")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfAllowedPeople")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfAxis")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Space")
                        .HasColumnType("REAL");

                    b.Property<double>("TotalLength")
                        .HasColumnType("REAL");

                    b.Property<double>("Width")
                        .HasColumnType("REAL");

                    b.HasDiscriminator().HasValue("Caravan");
                });

            modelBuilder.Entity("CarRentalRestApi.Models.RentVehicleModels.Rent", b =>
                {
                    b.HasOne("CarRentalRestApi.Models.Auth.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("CarRentalRestApi.Models.VehicleModels.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");

                    b.Navigation("User");

                    b.Navigation("Vehicle");
                });
#pragma warning restore 612, 618
        }
    }
}
