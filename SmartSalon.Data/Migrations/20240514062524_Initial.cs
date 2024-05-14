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
                    GoogleMapsLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    { new Guid("11612daf-ab0e-4947-8b09-b3ec7d0b60e5"), "SAR", "Saudi Arabia", "Saudi Riyal" },
                    { new Guid("154204de-5d35-4a2a-97f4-178db44e81c2"), "USD", "United States", "United States Dollar" },
                    { new Guid("2012f3b4-157c-44d2-87e1-53f443737d62"), "CHF", "Switzerland", "Swiss Franc" },
                    { new Guid("2d4ec577-423d-471a-8223-a3221adf6b89"), "HKD", "Hong Kong", "Hong Kong Dollar" },
                    { new Guid("34a76289-f5cb-41c8-bd90-e2292dcfa716"), "ILS", "Israel", "Israeli New Shekel" },
                    { new Guid("35ca5615-4897-48f4-9c93-18c042e44289"), "RUB", "Russia", "Russian Ruble" },
                    { new Guid("3d143a89-5192-4b62-af00-01da7f10f3a4"), "ZAR", "South Africa", "South African Rand" },
                    { new Guid("400fc833-e38b-4fe3-b193-c0c1fb7b009d"), "GBP", "United Kingdom", "British Pound Sterling" },
                    { new Guid("436dca7d-1c2d-488c-b978-937980a4765e"), "BGN", "Bulgaria", "Bulgarian Lev" },
                    { new Guid("46aa5aee-0453-422c-aab4-eb560a42eaaf"), "MXN", "Mexico", "Mexican Peso" },
                    { new Guid("4ac544c0-96d2-430b-9017-1d270b7cf7fd"), "DKK", "Denmark", "Danish Krone" },
                    { new Guid("5a9084f3-c566-4357-937d-f347440b80d7"), "PHP", "Philippines", "Philippine Peso" },
                    { new Guid("5aac227c-6209-476f-bbaf-e43c68656bf9"), "NOK", "Norway", "Norwegian Krone" },
                    { new Guid("5b4d656b-85da-42b4-8794-5522437548ee"), "EUR", "Eurozone", "Euro" },
                    { new Guid("5e32a03d-e42b-490e-9dab-e279fd641795"), "JPY", "Japan", "Japanese Yen" },
                    { new Guid("62e8e7ed-9981-49cc-bbd4-2bb46c4db4f4"), "CAD", "Canada", "Canadian Dollar" },
                    { new Guid("6e72a20c-4beb-40bc-a6c5-a42441887eec"), "NZD", "New Zealand", "New Zealand Dollar" },
                    { new Guid("74c3d30f-3340-4a6a-bc0b-4b964e575440"), "SGD", "Singapore", "Singapore Dollar" },
                    { new Guid("750a253b-64dd-4991-a347-f390aca9b7e9"), "BTC", null, "Bitcoin" },
                    { new Guid("7ac4b26b-9e8a-45d9-beda-089049f2528a"), "THB", "Thailand", "Thai Baht" },
                    { new Guid("7d7d070b-5f9a-4e0b-856e-25b1c077f708"), "AED", "United Arab Emirates", "UAE Dirham" },
                    { new Guid("91fda4ee-48c5-45cd-abaa-5be8ee256fd0"), "IDR", "Indonesia", "Indonesian Rupiah" },
                    { new Guid("a3385497-ae68-4367-9d91-b9aa4b88a120"), "ARS", "Argentina", "Argentine Peso" },
                    { new Guid("c690a42b-e3eb-47ee-82b5-d0edb0fcdcca"), "BRL", "Brazil", "Brazilian Real" },
                    { new Guid("ca93dc15-5059-4216-ad43-2b8858b2adea"), "TRY", "Turkey", "Turkish Lira" },
                    { new Guid("d1a85d03-92d9-4e3b-8cec-d9245f82fd24"), "KRW", "South Korea", "South Korean Won" },
                    { new Guid("d2e5103f-0820-4bf0-8d07-a0231e5fa18b"), "PLN", "Poland", "Polish Zloty" },
                    { new Guid("da206e5c-532a-46e1-b4eb-7f73ab97e1c6"), "AUD", "Australia", "Australian Dollar" },
                    { new Guid("da80bbf6-fa2e-43e7-bffa-96d1a83ef466"), "MYR", "Malaysia", "Malaysian Ringgit" },
                    { new Guid("e126ecb1-e9fa-4495-a836-c960a19f72ad"), "SEK", "Sweden", "Swedish Krona" },
                    { new Guid("eb27e4b4-cec5-4c1a-91d8-d7f5441ef3a0"), "CNY", "China", "Chinese Yuan" },
                    { new Guid("eb5fca3e-9f54-4eba-8798-f99c2c4f81af"), "INR", "India", "Indian Rupee" },
                    { new Guid("ef7566a8-b355-4267-af89-35c70764bb1e"), "ETH", null, "Ethereum" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("00eb9242-4227-430c-8571-bdebe012a4d5"), "3a4c44ce-6785-45e3-b427-9381768ea8c4", "Owner", "OWNER" },
                    { new Guid("54e75d9c-0fee-424b-8315-3043743d9ef9"), "bc16fd5a-7879-4844-925e-63f7994844ec", "Worker", "WORKER" },
                    { new Guid("595c1794-760c-48d9-8e08-3454530158ae"), "fba2ed8e-6ef8-484b-abdc-0c1ea020fa74", "Customer", "CUSTOMER" },
                    { new Guid("bd88ea77-fdd5-4d40-b7b9-9e8b33f95e7f"), "6070e2ec-4953-4a2b-9cf6-945f5ced2bc9", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("51aaba70-6af0-48e6-8d02-452249c18e9c"), 0, "5d1169bc-9380-4560-88c6-4e27d0f4cb07", "gkerkelov03@abv.bg", true, "Georgi", "Kerkelov", false, null, "GKERKELOV03@ABV.BG", "GKERKELOV03@ABV.BG", "AQAAAAIAAYagAAAAENNNNCC3quNtQDgNBQK9/CZEQkrYO8rLQb5ciKkze0rnVs92p35G3K1H8V3jhF8DSg==", "0895105609", false, "https://res.cloudinary.com/donhvedgr/image/upload/v1662969813/blank-profile-picture_cqowyq.webp", "840e476b-d8c8-459c-aab1-6ae41a27a512", false, "gkerkelov03@abv.bg", "Admin" },
                    { new Guid("cf44d19a-dc91-47ce-bdd7-c9e4fc2f922c"), 0, "d5d8332b-2fc4-4f74-9060-52bf998cb28f", "pivanov03@abv.bg", true, "Petar", "Ivanov", false, null, "PIVANOV03@ABV.BG", "PIVANOV03@ABV.BG", "AQAAAAIAAYagAAAAEAcqmEErp1KPdUYCy7EuDqySuIIdqpzwRezJaORi/GmkYGtQtHP69ruFzHffv6YL2g==", "0899829897", false, "https://res.cloudinary.com/donhvedgr/image/upload/v1662969813/blank-profile-picture_cqowyq.webp", "47a94729-3dd5-40bc-9841-0752228417ce", false, "pivanov03@abv.bg", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Salons",
                columns: new[] { "Id", "BookingsInAdvance", "Country", "DeletedBy", "DeletedOn", "Description", "GoogleMapsLocation", "IsDeleted", "MainCurrencyId", "Name", "ProfilePictureUrl", "SubscriptionsEnabled", "TimePenalty", "WorkersCanDeleteBookings", "WorkersCanMoveBookings", "WorkersCanSetNonWorkingPeriods", "WorkingTimeId" },
                values: new object[,]
                {
                    { new Guid("416d46c4-a3b2-46aa-b248-fb434f6953c0"), 5, "BULGARIA", null, null, "Description", "Location", false, new Guid("436dca7d-1c2d-488c-b978-937980a4765e"), "Cosa Nostra", null, true, 5, false, true, true, new Guid("e01cfde9-7c2b-4bef-a25b-83f22f359159") },
                    { new Guid("87ade6a9-909b-4a80-97f4-19b8ac8d17fb"), 5, "BULGARIA", null, null, "Description", "Location", false, new Guid("436dca7d-1c2d-488c-b978-937980a4765e"), "Gosho shop", null, true, 5, false, true, true, new Guid("a9ea4624-05fb-423e-acce-6b9b974e9852") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("595c1794-760c-48d9-8e08-3454530158ae"), new Guid("51aaba70-6af0-48e6-8d02-452249c18e9c") },
                    { new Guid("bd88ea77-fdd5-4d40-b7b9-9e8b33f95e7f"), new Guid("51aaba70-6af0-48e6-8d02-452249c18e9c") },
                    { new Guid("595c1794-760c-48d9-8e08-3454530158ae"), new Guid("cf44d19a-dc91-47ce-bdd7-c9e4fc2f922c") },
                    { new Guid("bd88ea77-fdd5-4d40-b7b9-9e8b33f95e7f"), new Guid("cf44d19a-dc91-47ce-bdd7-c9e4fc2f922c") }
                });

            migrationBuilder.InsertData(
                table: "WorkingTimes",
                columns: new[] { "Id", "FridayClosingTime", "FridayOpeningTime", "MondayClosingTime", "MondayOpeningTime", "SalonId", "SaturdayClosingTime", "SaturdayOpeningTime", "SundayClosingTime", "SundayOpeningTime", "ThursdayClosingTime", "ThursdayOpeningTime", "TuesdayClosingTime", "TuesdayOpeningTime", "WednesdayClosingTime", "WednesdayOpeningTime" },
                values: new object[,]
                {
                    { new Guid("a9ea4624-05fb-423e-acce-6b9b974e9852"), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new Guid("87ade6a9-909b-4a80-97f4-19b8ac8d17fb"), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0) },
                    { new Guid("e01cfde9-7c2b-4bef-a25b-83f22f359159"), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new Guid("416d46c4-a3b2-46aa-b248-fb434f6953c0"), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0) }
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
