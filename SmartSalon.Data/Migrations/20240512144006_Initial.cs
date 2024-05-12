using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartSalon.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimePenalty = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    BookingsInAdvance = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    SubscriptionsEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    WorkersCanMoveBookings = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    WorkersCanDeleteBookings = table.Column<bool>(type: "bit", nullable: false),
                    WorkersCanSetNonWorkingPeriods = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    WorkingTimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MainCurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salons_Currencies_MainCurrencyId",
                        column: x => x.MainCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Logins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurrencySalon",
                columns: table => new
                {
                    AcceptedCurrenciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalonsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencySalon", x => new { x.AcceptedCurrenciesId, x.SalonsId });
                    table.ForeignKey(
                        name: "FK_CurrencySalon_Currencies_AcceptedCurrenciesId",
                        column: x => x.AcceptedCurrenciesId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrencySalon_Salons_SalonsId",
                        column: x => x.SalonsId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTitles_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OwnerSalon",
                columns: table => new
                {
                    OwnersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalonsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerSalon", x => new { x.OwnersId, x.SalonsId });
                    table.ForeignKey(
                        name: "FK_OwnerSalon_Salons_SalonsId",
                        column: x => x.SalonsId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OwnerSalon_Users_OwnersId",
                        column: x => x.OwnersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalonWorker",
                columns: table => new
                {
                    SalonsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalonWorker", x => new { x.SalonsId, x.WorkersId });
                    table.ForeignKey(
                        name: "FK_SalonWorker_Salons_SalonsId",
                        column: x => x.SalonsId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalonWorker_Users_WorkersId",
                        column: x => x.WorkersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specialties_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimePenaltyInDays = table.Column<int>(type: "int", nullable: false),
                    AllowedBookingsInAdvance = table.Column<int>(type: "int", nullable: false),
                    Tier = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkingTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MondayOpeningTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    MondayClosingTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    TuesdayOpeningTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    TuesdayClosingTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    WednesdayOpeningTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    WednesdayClosingTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ThursdayOpeningTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ThursdayClosingTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    FridayOpeningTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    FridayClosingTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    SaturdayOpeningTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    SaturdayClosingTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    SundayOpeningTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    SundayClosingTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingTimes_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobTitleWorker",
                columns: table => new
                {
                    JobTitlesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitleWorker", x => new { x.JobTitlesId, x.WorkersId });
                    table.ForeignKey(
                        name: "FK_JobTitleWorker_JobTitles_JobTitlesId",
                        column: x => x.JobTitlesId,
                        principalTable: "JobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobTitleWorker_Users_WorkersId",
                        column: x => x.WorkersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSubscription",
                columns: table => new
                {
                    ActiveCustomersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSubscription", x => new { x.ActiveCustomersId, x.SubscriptionsId });
                    table.ForeignKey(
                        name: "FK_CustomerSubscription_Subscriptions_SubscriptionsId",
                        column: x => x.SubscriptionsId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSubscription_Users_ActiveCustomersId",
                        column: x => x.ActiveCustomersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobTitleService",
                columns: table => new
                {
                    JobTitlesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitleService", x => new { x.JobTitlesId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_JobTitleService_JobTitles_JobTitlesId",
                        column: x => x.JobTitlesId,
                        principalTable: "JobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobTitleService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpecialSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    ExpirationInDays = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialSlots_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpecialSlots_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("17b01adc-8258-46bd-9443-2157d42ca3a4"), "AUD", "Australia", "Australian Dollar" },
                    { new Guid("2ab761b2-57c6-40f4-8243-a8e60ed468be"), "SAR", "Saudi Arabia", "Saudi Riyal" },
                    { new Guid("2b010949-f9b5-4432-91fd-dec97768b228"), "AED", "United Arab Emirates", "UAE Dirham" },
                    { new Guid("2de11c1e-386f-4e84-ba00-1a67fc3a5769"), "SGD", "Singapore", "Singapore Dollar" },
                    { new Guid("2de8f9c5-e1c4-4b0a-bcc3-a8f0449e2cf1"), "NZD", "New Zealand", "New Zealand Dollar" },
                    { new Guid("3408ad86-dd29-4c6a-a362-d7e1cb8fe741"), "MXN", "Mexico", "Mexican Peso" },
                    { new Guid("38254dfc-4334-41dd-a78b-26dad29c8558"), "GBP", "United Kingdom", "British Pound Sterling" },
                    { new Guid("47663599-abcc-41c6-9d9f-bb17634d1683"), "TRY", "Turkey", "Turkish Lira" },
                    { new Guid("500e07d6-27db-46ef-bd44-6fdd086f53bc"), "HKD", "Hong Kong", "Hong Kong Dollar" },
                    { new Guid("58cf6cbb-d6e1-4d28-b3ca-f64158f5e88d"), "THB", "Thailand", "Thai Baht" },
                    { new Guid("5ab9aa03-b753-4b7b-ab37-7901d8c8ee9a"), "ILS", "Israel", "Israeli New Shekel" },
                    { new Guid("5ddd1cda-734b-4bdd-bd26-072b22d2c805"), "BTC", null, "Bitcoin" },
                    { new Guid("67061e63-4a7a-471e-ab6e-997196982e0d"), "BRL", "Brazil", "Brazilian Real" },
                    { new Guid("6a076ab5-362e-42b2-b5d0-7df5a9681fe3"), "MYR", "Malaysia", "Malaysian Ringgit" },
                    { new Guid("a3900e0c-0ca5-4c85-a98b-132d5cfa09fe"), "PHP", "Philippines", "Philippine Peso" },
                    { new Guid("aa94ace0-a446-4a86-b333-6db21e5475eb"), "RUB", "Russia", "Russian Ruble" },
                    { new Guid("aef5e918-a6ec-4089-a371-3c8af9646191"), "BGN", "Bulgaria", "Bulgarian Lev" },
                    { new Guid("af97f241-c597-4164-91a2-a4c47a014fa3"), "KRW", "South Korea", "South Korean Won" },
                    { new Guid("b58930ab-8c3b-42bd-ab5a-d1c95cbf65fc"), "JPY", "Japan", "Japanese Yen" },
                    { new Guid("bcc80eaa-e0c3-42a2-b7d0-8ba567832338"), "ZAR", "South Africa", "South African Rand" },
                    { new Guid("bd6c7e89-5748-4ecc-aa05-574cd8615d35"), "ARS", "Argentina", "Argentine Peso" },
                    { new Guid("c1142d64-2b46-43b5-89a8-f7b83a3ace55"), "CAD", "Canada", "Canadian Dollar" },
                    { new Guid("c522b5ad-1f48-4c64-add8-6dd503b9f0f7"), "NOK", "Norway", "Norwegian Krone" },
                    { new Guid("de040d8d-c9e4-480c-809c-dd612c9a4901"), "SEK", "Sweden", "Swedish Krona" },
                    { new Guid("e0be4f27-5d11-4005-b080-a45b737bc92e"), "IDR", "Indonesia", "Indonesian Rupiah" },
                    { new Guid("e1feb648-aa9c-4a5d-8c92-9ad60e1dab97"), "USD", "United States", "United States Dollar" },
                    { new Guid("e454e8b4-66b4-40f4-a865-5c378e92c345"), "ETH", null, "Ethereum" },
                    { new Guid("e901f5f6-c4b7-4d78-bd98-6f8417957665"), "DKK", "Denmark", "Danish Krone" },
                    { new Guid("e9e77de3-bb69-4bb5-b956-c151cadc49c5"), "PLN", "Poland", "Polish Zloty" },
                    { new Guid("f625499e-a7b9-47c8-a6aa-4871b3ba5b1a"), "CNY", "China", "Chinese Yuan" },
                    { new Guid("f8d5197b-2942-49fc-9209-47bca3a42e30"), "CHF", "Switzerland", "Swiss Franc" },
                    { new Guid("f923d15f-82ef-4a70-ad58-c90f8a731663"), "INR", "India", "Indian Rupee" },
                    { new Guid("f9b4d295-7dac-4be5-bda7-f54379e73285"), "EUR", "Eurozone", "Euro" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6104d7fc-b900-46b6-87c3-dc4468c46729"), "383e57f1-02a6-4549-81f3-434ba47d1c7f", "Worker", "WORKER" },
                    { new Guid("a6ad51f3-76e2-4664-8fb9-8aa8f10feaba"), "ba00bc83-e365-4d34-868a-5d6002ce374d", "Owner", "OWNER" },
                    { new Guid("b0653647-2010-4d3e-a36d-8e81b0d32f1d"), "bf8dda82-6e06-47f3-a7a5-76b55e1b6cb9", "Customer", "CUSTOMER" },
                    { new Guid("f364f068-a656-4d6e-8708-6ba6bc4a3130"), "f58d4558-28ea-4d8b-a214-d011cfe62b49", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("31066763-885a-4e61-aa28-f5f96a4ef8f2"), 0, "b6703c94-7548-4c30-826e-d755bef8159d", "pivanov03@abv.bg", true, "Petar", "Ivanov", false, null, "PIVANOV03@ABV.BG", "PIVANOV03@ABV.BG", "AQAAAAIAAYagAAAAEJIwGk0ApVaH02M3nkG7lHAyJgimnHpPl3p79Qz9oWCPKXECr5pmVgPOJW0LA3hkog==", "0899829897", false, "https://res.cloudinary.com/donhvedgr/image/upload/v1662969813/blank-profile-picture_cqowyq.webp", "ef58dd8a-b9fc-41a3-a15c-38a59e5c99f6", false, "pivanov03@abv.bg", "Admin" },
                    { new Guid("9eb7de92-aefb-40f8-b1f3-78e8c2975d4e"), 0, "28e81381-0340-4a09-a7b0-4798e7daa0a0", "gkerkelov03@abv.bg", true, "Georgi", "Kerkelov", false, null, "GKERKELOV03@ABV.BG", "GKERKELOV03@ABV.BG", "AQAAAAIAAYagAAAAEIvJ/Pfbr6VqVMiPEXt5DdMNtFAJPEYdbQhZmjTtjbXqKR+0kqngBtKk5WfcZlY/lA==", "0895105609", false, "https://res.cloudinary.com/donhvedgr/image/upload/v1662969813/blank-profile-picture_cqowyq.webp", "bf76e3db-4fcd-421c-964b-aff836d44381", false, "gkerkelov03@abv.bg", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Salons",
                columns: new[] { "Id", "BookingsInAdvance", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "Location", "MainCurrencyId", "Name", "ProfilePictureUrl", "SubscriptionsEnabled", "TimePenalty", "WorkersCanDeleteBookings", "WorkersCanMoveBookings", "WorkersCanSetNonWorkingPeriods", "WorkingTimeId" },
                values: new object[,]
                {
                    { new Guid("8e9e5f90-3e3e-4e1a-9b24-ba93a687705a"), 5, null, null, "Description", false, "Location", new Guid("aef5e918-a6ec-4089-a371-3c8af9646191"), "Cosa Nostra", null, true, 5, false, true, true, new Guid("d3ee4925-905e-4c4f-99f8-0ac749a4fbcc") },
                    { new Guid("ad117d5c-2359-4555-9582-22f00546723c"), 5, null, null, "Description", false, "Location", new Guid("aef5e918-a6ec-4089-a371-3c8af9646191"), "Gosho shop", null, true, 5, false, true, true, new Guid("3d7ef01b-a43b-421a-9f78-112f313bb1ba") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("b0653647-2010-4d3e-a36d-8e81b0d32f1d"), new Guid("31066763-885a-4e61-aa28-f5f96a4ef8f2") },
                    { new Guid("f364f068-a656-4d6e-8708-6ba6bc4a3130"), new Guid("31066763-885a-4e61-aa28-f5f96a4ef8f2") },
                    { new Guid("b0653647-2010-4d3e-a36d-8e81b0d32f1d"), new Guid("9eb7de92-aefb-40f8-b1f3-78e8c2975d4e") },
                    { new Guid("f364f068-a656-4d6e-8708-6ba6bc4a3130"), new Guid("9eb7de92-aefb-40f8-b1f3-78e8c2975d4e") }
                });

            migrationBuilder.InsertData(
                table: "WorkingTimes",
                columns: new[] { "Id", "FridayClosingTime", "FridayOpeningTime", "MondayClosingTime", "MondayOpeningTime", "SalonId", "SaturdayClosingTime", "SaturdayOpeningTime", "SundayClosingTime", "SundayOpeningTime", "ThursdayClosingTime", "ThursdayOpeningTime", "TuesdayClosingTime", "TuesdayOpeningTime", "WednesdayClosingTime", "WednesdayOpeningTime" },
                values: new object[,]
                {
                    { new Guid("3d7ef01b-a43b-421a-9f78-112f313bb1ba"), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new Guid("ad117d5c-2359-4555-9582-22f00546723c"), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0) },
                    { new Guid("d3ee4925-905e-4c4f-99f8-0ac749a4fbcc"), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new Guid("8e9e5f90-3e3e-4e1a-9b24-ba93a687705a"), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SalonId",
                table: "Bookings",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ServiceId",
                table: "Bookings",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_WorkerId",
                table: "Bookings",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SalonId",
                table: "Categories",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SectionId",
                table: "Categories",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencySalon_SalonsId",
                table: "CurrencySalon",
                column: "SalonsId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscription_SubscriptionsId",
                table: "CustomerSubscription",
                column: "SubscriptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_SalonId",
                table: "Images",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_SalonId",
                table: "JobTitles",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitleService_ServicesId",
                table: "JobTitleService",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitleWorker_WorkersId",
                table: "JobTitleWorker",
                column: "WorkersId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_UserId",
                table: "Logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerSalon_SalonsId",
                table: "OwnerSalon",
                column: "SalonsId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salons_MainCurrencyId",
                table: "Salons",
                column: "MainCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalonWorker_WorkersId",
                table: "SalonWorker",
                column: "WorkersId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_SalonId",
                table: "Sections",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CategoryId",
                table: "Services",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_SalonId",
                table: "Services",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_SubscriptionId",
                table: "Services",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialSlots_ServiceId",
                table: "SpecialSlots",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialSlots_SubscriptionId",
                table: "SpecialSlots",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_SalonId",
                table: "Specialties",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SalonId",
                table: "Subscriptions",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimes_SalonId",
                table: "WorkingTimes",
                column: "SalonId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "CurrencySalon");

            migrationBuilder.DropTable(
                name: "CustomerSubscription");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "JobTitleService");

            migrationBuilder.DropTable(
                name: "JobTitleWorker");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "OwnerSalon");

            migrationBuilder.DropTable(
                name: "SalonWorker");

            migrationBuilder.DropTable(
                name: "SpecialSlots");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "WorkingTimes");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Salons");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
