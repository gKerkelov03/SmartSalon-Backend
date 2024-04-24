﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartSalon.Data;

#nullable disable

namespace SmartSalon.Data.Migrations
{
    [DbContext(typeof(SmartSalonDbContext))]
    partial class SmartSalonDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<TimeOnly>("From")
                        .HasColumnType("time");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("SmartSalon.Application.Domain.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Salons.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Salons.Salon", b =>
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

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SectionsEnabled")
                        .HasColumnType("bit");

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

                    b.Property<Guid>("WorkingTimeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Salons");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Salons.Specialty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Salons.WorkingTime", b =>
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

            modelBuilder.Entity("SmartSalon.Application.Domain.Services.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SectionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.HasIndex("SectionId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Services.Section", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Services.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<Guid>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

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

                    b.Property<int>("ExpirationInDays")
                        .HasColumnType("int");

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

            modelBuilder.Entity("SmartSalon.Application.Domain.Users.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

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
                        .IsRequired()
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
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
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

                    b.ToTable("Users", (string)null);

                    b.HasDiscriminator<string>("UserType").HasValue("Admin");

                    b.UseTphMappingStrategy();
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
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("SalonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("SalonId");

                    b.HasDiscriminator().HasValue("Worker");
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

                    b.HasOne("SmartSalon.Application.Domain.Salons.Salon", null)
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

                    b.HasOne("SmartSalon.Application.Domain.Salons.Salon", "Salon")
                        .WithMany()
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Services.Service", "Service")
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

            modelBuilder.Entity("SmartSalon.Application.Domain.Currency", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salons.Salon", null)
                        .WithMany("Currencies")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Salons.Image", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salons.Salon", "Salon")
                        .WithMany("Images")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Salons.Specialty", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salons.Salon", "Salon")
                        .WithMany("Specialties")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Salons.WorkingTime", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salons.Salon", "Salon")
                        .WithOne("WorkingTime")
                        .HasForeignKey("SmartSalon.Application.Domain.Salons.WorkingTime", "SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Services.Category", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salons.Salon", "Salon")
                        .WithMany("Categories")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Services.Section", "Section")
                        .WithMany("Categories")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Salon");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Services.Section", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salons.Salon", "Salon")
                        .WithMany("Sections")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Services.Service", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Services.Category", "Categorie")
                        .WithMany("Services")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Salons.Salon", "Salon")
                        .WithMany("Services")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartSalon.Application.Domain.Subscription", null)
                        .WithMany("ServicesIncluded")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Categorie");

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.SpecialSlot", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Services.Service", "Service")
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

            modelBuilder.Entity("SmartSalon.Application.Domain.Subscription", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salons.Salon", "Salon")
                        .WithMany()
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Users.Worker", b =>
                {
                    b.HasOne("SmartSalon.Application.Domain.Salons.Salon", "Salon")
                        .WithMany("Workers")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Salons.Salon", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Currencies");

                    b.Navigation("Images");

                    b.Navigation("Sections");

                    b.Navigation("Services");

                    b.Navigation("Specialties");

                    b.Navigation("Workers");

                    b.Navigation("WorkingTime");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Services.Category", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("SmartSalon.Application.Domain.Services.Section", b =>
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
