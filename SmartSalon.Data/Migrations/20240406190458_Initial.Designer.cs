﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartSalon.Data;

#nullable disable

namespace SmartSalon.Data.Migrations
{
    [DbContext(typeof(SmartSalonDbContext))]
    [Migration("20240406190458_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryService", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServicesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoriesId", "ServicesId");

                    b.HasIndex("ServicesId");

                    b.ToTable("CategoryService");
                });

            modelBuilder.Entity("CustomerSubscription", b =>
                {
                    b.Property<Guid>("ActiveCustomersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubscriptionsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ActiveCustomersId", "SubscriptionsId");

                    b.HasIndex("SubscriptionsId");

                    b.ToTable("CustomerSubscription");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("Logins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("OwnerSalon", b =>
                {
                    b.Property<Guid>("OwnersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SalonsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OwnersId", "SalonsId");

                    b.HasIndex("SalonsId");

                    b.ToTable("OwnerSalon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("From")
                        .HasColumnType("time");

                    b.Property<Guid>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeOnly>("To")
                        .HasColumnType("time");

                    b.Property<Guid>("WorkerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SalonId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SectionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Salon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DefaultBookingsInAdvance")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<int>("DefaultTimePenalty")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid?>("MainPictureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("SubscriptionsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("WorkersCanMoveBookings")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("WorkersCanSetNonWorkingPeriods")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<Guid?>("WorkingTimeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MainPictureId")
                        .IsUnique()
                        .HasFilter("[MainPictureId] IS NOT NULL");

                    b.ToTable("Salons");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Section", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<Guid>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.SpecialSlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<Guid>("ExpirationInDays")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeOnly>("From")
                        .HasColumnType("time");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeOnly>("To")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("SpecialSlots");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Specialty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("SalonSpecialties");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AllowedBookingsInAdvance")
                        .HasColumnType("int");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<Guid>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Tier")
                        .HasColumnType("int");

                    b.Property<int>("TimePenaltyInDays")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Token", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Users.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ProfilePictureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("ProfilePictureId");

                    b.ToTable("Users", (string)null);

                    b.HasDiscriminator<string>("UserType").HasValue("Admin");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.WorkingTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeOnly>("FridayFrom")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("FridayTo")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("MondayFrom")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("MondayTo")
                        .HasColumnType("time");

                    b.Property<Guid>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeOnly>("SaturdayFrom")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("SaturdayTo")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("SundayFrom")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("SundayTo")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("ThursdayFrom")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("ThursdayTo")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("TuesdayFrom")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("TuesdayTo")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("WednesdayFrom")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("WednesdayTo")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("SalonId")
                        .IsUnique();

                    b.ToTable("WorkingTimes");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Users.Customer", b =>
                {
                    b.HasBaseType("SmartSalon.Application.Domain.Users.User");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Users.Owner", b =>
                {
                    b.HasBaseType("SmartSalon.Application.Domain.Users.User");

                    b.HasDiscriminator().HasValue("Owner");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Users.Worker", b =>
                {
                    b.HasBaseType("SmartSalon.Application.Domain.Users.User");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid?>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("SalonId");

                    b.HasDiscriminator().HasValue("Worker");
                });

            modelBuilder.Entity("CategoryService", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Service", null)
                        .WithMany()
                        .HasForeignKey("ServicesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CustomerSubscription", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Users.Customer", null)
                        .WithMany()
                        .HasForeignKey("ActiveCustomersId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Subscription", null)
                        .WithMany()
                        .HasForeignKey("SubscriptionsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("OwnerSalon", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Users.Owner", null)
                        .WithMany()
                        .HasForeignKey("OwnersId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Salon", null)
                        .WithMany()
                        .HasForeignKey("SalonsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Booking", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Users.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Salon", "Salon")
                        .WithMany()
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Users.Worker", "Worker")
                        .WithMany("Calendar")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Salon");

                    b.Navigation("Service");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Category", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Section", null)
                        .WithMany("Categories")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Image", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salon", null)
                        .WithMany("Images")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Salon", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Image", "MainPicture")
                        .WithOne()
                        .HasForeignKey("SmartSalon.Application.Domain.Salon", "MainPictureId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("MainPicture");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Service", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salon", "Salon")
                        .WithMany("Services")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Subscription", null)
                        .WithMany("ServicesIncluded")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.SpecialSlot", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Subscription", null)
                        .WithMany("SpecialSlots")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Service");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Specialty", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salon", "Salon")
                        .WithMany("Specialties")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Subscription", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salon", "Salon")
                        .WithMany()
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Token", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Users.User", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Image", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfilePictureId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ProfilePicture");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.WorkingTime", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salon", "Salon")
                        .WithOne("WorkingTime")
                        .HasForeignKey("SmartSalon.Application.Domain.WorkingTime", "SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Users.Worker", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salon", "Salon")
                        .WithMany("Workers")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Salon", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Services");

                    b.Navigation("Specialties");

                    b.Navigation("Workers");

                    b.Navigation("WorkingTime");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Section", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Subscription", b =>
                {
                    b.Navigation("ServicesIncluded");

                    b.Navigation("SpecialSlots");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Users.Customer", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Users.Worker", b =>
                {
                    b.Navigation("Calendar");
                });
#pragma warning restore 612, 618
        }
    }
}
