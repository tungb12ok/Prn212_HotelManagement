﻿// <auto-generated />
using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(FUMiniHotelManagementContext))]
    [Migration("20240525105754_PRN212_HotelManagement_Context")]
    partial class PRN212_HotelManagement_Context
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataAccess.Models.BookingDetail", b =>
                {
                    b.Property<int>("BookingReservationId")
                        .HasColumnType("int")
                        .HasColumnName("BookingReservationID");

                    b.Property<int>("RoomId")
                        .HasColumnType("int")
                        .HasColumnName("RoomID");

                    b.Property<decimal?>("ActualPrice")
                        .HasColumnType("money");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("BookingReservationId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("BookingDetail", (string)null);
                });

            modelBuilder.Entity("DataAccess.Models.BookingReservation", b =>
                {
                    b.Property<int>("BookingReservationId")
                        .HasColumnType("int")
                        .HasColumnName("BookingReservationID");

                    b.Property<DateTime?>("BookingDate")
                        .HasColumnType("date");

                    b.Property<byte?>("BookingStatus")
                        .HasColumnType("tinyint");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("money");

                    b.HasKey("BookingReservationId");

                    b.HasIndex("CustomerId");

                    b.ToTable("BookingReservation", (string)null);
                });

            modelBuilder.Entity("DataAccess.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<DateTime?>("CustomerBirthday")
                        .HasColumnType("date");

                    b.Property<string>("CustomerFullName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte?>("CustomerStatus")
                        .HasColumnType("tinyint");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telephone")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("CustomerId");

                    b.HasIndex(new[] { "EmailAddress" }, "UQ__Customer__49A147406D0DB964")
                        .IsUnique();

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("DataAccess.Models.RoomInformation", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoomID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"), 1L, 1);

                    b.Property<string>("RoomDetailDescription")
                        .HasMaxLength(220)
                        .HasColumnType("nvarchar(220)");

                    b.Property<int?>("RoomMaxCapacity")
                        .HasColumnType("int");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("RoomPricePerDay")
                        .HasColumnType("money");

                    b.Property<byte?>("RoomStatus")
                        .HasColumnType("tinyint");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("int")
                        .HasColumnName("RoomTypeID");

                    b.HasKey("RoomId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("RoomInformation", (string)null);
                });

            modelBuilder.Entity("DataAccess.Models.RoomType", b =>
                {
                    b.Property<int>("RoomTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoomTypeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomTypeId"), 1L, 1);

                    b.Property<string>("RoomTypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TypeDescription")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("TypeNote")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("RoomTypeId");

                    b.ToTable("RoomType", (string)null);
                });

            modelBuilder.Entity("DataAccess.Models.BookingDetail", b =>
                {
                    b.HasOne("DataAccess.Models.BookingReservation", "BookingReservation")
                        .WithMany("BookingDetails")
                        .HasForeignKey("BookingReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BookingDetail_BookingReservation");

                    b.HasOne("DataAccess.Models.RoomInformation", "Room")
                        .WithMany("BookingDetails")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BookingDetail_RoomInformation");

                    b.Navigation("BookingReservation");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("DataAccess.Models.BookingReservation", b =>
                {
                    b.HasOne("DataAccess.Models.Customer", "Customer")
                        .WithMany("BookingReservations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BookingReservation_Customer");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("DataAccess.Models.RoomInformation", b =>
                {
                    b.HasOne("DataAccess.Models.RoomType", "RoomType")
                        .WithMany("RoomInformations")
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_RoomInformation_RoomType");

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("DataAccess.Models.BookingReservation", b =>
                {
                    b.Navigation("BookingDetails");
                });

            modelBuilder.Entity("DataAccess.Models.Customer", b =>
                {
                    b.Navigation("BookingReservations");
                });

            modelBuilder.Entity("DataAccess.Models.RoomInformation", b =>
                {
                    b.Navigation("BookingDetails");
                });

            modelBuilder.Entity("DataAccess.Models.RoomType", b =>
                {
                    b.Navigation("RoomInformations");
                });
#pragma warning restore 612, 618
        }
    }
}