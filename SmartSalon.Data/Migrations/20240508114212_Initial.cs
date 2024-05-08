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
                    { new Guid("09474154-7873-4d38-a807-02b1d0b868ce"), "PLN", "Poland", "Polish Zloty" },
                    { new Guid("190ea253-ae1e-4d97-86ac-fb352e4bf65e"), "CNY", "China", "Chinese Yuan" },
                    { new Guid("1929324c-892c-4e94-b499-ab02a01ce38f"), "BGN", "Bulgaria", "Bulgarian Lev" },
                    { new Guid("1b7119cd-8fe1-48d4-a20d-5b62803d843c"), "AUD", "Australia", "Australian Dollar" },
                    { new Guid("1d7b48f3-3899-4ad8-92d7-a5edab0d00fe"), "SAR", "Saudi Arabia", "Saudi Riyal" },
                    { new Guid("271fc71f-d890-4657-a659-03f628711736"), "KRW", "South Korea", "South Korean Won" },
                    { new Guid("30a0fc5f-e8db-42ca-bb2a-b5bf9bb8e500"), "MYR", "Malaysia", "Malaysian Ringgit" },
                    { new Guid("3b822ddf-8ccd-4d14-aee8-f2234844543f"), "NZD", "New Zealand", "New Zealand Dollar" },
                    { new Guid("3c8fb0cb-783d-48a5-a8ad-545ac8cdb2e7"), "RUB", "Russia", "Russian Ruble" },
                    { new Guid("483d6494-f73b-4cb3-9b76-13dc63e67f99"), "SEK", "Sweden", "Swedish Krona" },
                    { new Guid("57fdd427-5fdd-4ae4-80e0-5eff11a37d5b"), "ZAR", "South Africa", "South African Rand" },
                    { new Guid("5c4da8a3-1505-4070-b26c-d9fe7f3eb4cf"), "DKK", "Denmark", "Danish Krone" },
                    { new Guid("67d9428c-2d9f-4035-8f18-916e59200e33"), "ARS", "Argentina", "Argentine Peso" },
                    { new Guid("70228ac0-6ef4-45f4-bfdd-6e55a13e4487"), "EUR", "Eurozone", "Euro" },
                    { new Guid("70cf26af-0803-4543-a093-0d45bcb837a6"), "USD", "United States", "United States Dollar" },
                    { new Guid("74823046-f5c5-4c1a-9243-a543ee34b97b"), "PHP", "Philippines", "Philippine Peso" },
                    { new Guid("8252f121-e5dc-4697-8f6b-4679eb4a069f"), "IDR", "Indonesia", "Indonesian Rupiah" },
                    { new Guid("961a3462-1a23-40a6-9389-37fcf7cc6822"), "ETH", null, "Ethereum" },
                    { new Guid("a3677c09-7ab1-46d8-9dc7-75e89621f062"), "THB", "Thailand", "Thai Baht" },
                    { new Guid("aa29153a-28dd-401c-8f0a-e97b13076fbf"), "HKD", "Hong Kong", "Hong Kong Dollar" },
                    { new Guid("b5e3f125-4f87-460d-8141-276fb8cce9a2"), "NOK", "Norway", "Norwegian Krone" },
                    { new Guid("bceec1d7-c712-40d3-a8b8-3f1bdf4b61e2"), "TRY", "Turkey", "Turkish Lira" },
                    { new Guid("c20a3a0d-5ef2-4f3d-a0d4-379b329190e1"), "CHF", "Switzerland", "Swiss Franc" },
                    { new Guid("c3d70c29-05d8-4781-aced-049ae6fcb813"), "MXN", "Mexico", "Mexican Peso" },
                    { new Guid("d398b870-359c-4065-ba1d-7d77f366cb44"), "BTC", null, "Bitcoin" },
                    { new Guid("dba531d2-2720-4a60-a905-8dd6e552f311"), "SGD", "Singapore", "Singapore Dollar" },
                    { new Guid("dc189a94-7dba-4591-9ab3-2e4dc023b585"), "CAD", "Canada", "Canadian Dollar" },
                    { new Guid("e7cdc6e2-f441-4fcc-8267-5230e68af8ab"), "JPY", "Japan", "Japanese Yen" },
                    { new Guid("e823095c-dcfe-44a2-b1f6-832068fd9861"), "AED", "United Arab Emirates", "UAE Dirham" },
                    { new Guid("f5a39241-b07a-4969-96ee-5ff93d30d80f"), "GBP", "United Kingdom", "British Pound Sterling" },
                    { new Guid("f6c21b92-c010-49b3-b4f6-8787fac7d76b"), "INR", "India", "Indian Rupee" },
                    { new Guid("f6cf6001-fa40-494d-9881-12adb3330a23"), "BRL", "Brazil", "Brazilian Real" },
                    { new Guid("f9df2d7c-9726-4b3a-890e-b1e2cd19615c"), "ILS", "Israel", "Israeli New Shekel" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1f8e8921-5afa-4bd2-8bb2-e4b6d6809e53"), "fc8fbd17-6ef1-4b34-acce-2517a3c85697", "Worker", "WORKER" },
                    { new Guid("74827b6c-3a86-461e-afc1-d9ebf7fe7fed"), "6600dbaa-4109-453b-bf30-23dac5d56e84", "Customer", "CUSTOMER" },
                    { new Guid("7751b427-09fb-4ee9-bfdb-a1c0a803459a"), "412a4ca9-2c4e-4bb6-8997-2d9b56f147e6", "Admin", "ADMIN" },
                    { new Guid("ccf57d36-e4d9-48e6-93ef-1ed120008895"), "7142944d-342c-425d-9763-bf5ac0675f14", "Owner", "OWNER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("3f685e89-8c61-47c0-b9a6-e96c279798de"), 0, "e1042945-7d05-4dac-8f75-cca04c9b1ce8", "gkerkelov03@abv.bg", true, "Georgi", "Kerkelov", false, null, "GKERKELOV03@ABV.BG", "GKERKELOV03@ABV.BG", "AQAAAAIAAYagAAAAEAFJZigcoM++TBG4fKnIfY1BrxkkgA1bfXB/7Q8Dz8EWtGdYOstTjO9SjDU771Ws5w==", "0895105609", false, "https://res.cloudinary.com/donhvedgr/image/upload/v1662969813/blank-profile-picture_cqowyq.webp", "a13d29a0-b03a-495f-8325-e89a4e364375", false, "gkerkelov03@abv.bg", "Admin" },
                    { new Guid("ea916748-33a8-4785-a883-9e1c449d7f22"), 0, "a31678dd-8cd5-4edd-af7f-433f63c5cc73", "pivanov03@abv.bg", true, "Petar", "Ivanov", false, null, "PIVANOV03@ABV.BG", "PIVANOV03@ABV.BG", "AQAAAAIAAYagAAAAEImpNmtfwuKPZE+Bxov/OdWtIsxLamrnezlqeaoy8dw2xZBlH16OVWHmc8gP+OjkVw==", "0899829897", false, "https://res.cloudinary.com/donhvedgr/image/upload/v1662969813/blank-profile-picture_cqowyq.webp", "380c0786-f70e-4895-a71b-fc42f3160365", false, "pivanov03@abv.bg", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Salons",
                columns: new[] { "Id", "BookingsInAdvance", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "Location", "MainCurrencyId", "Name", "ProfilePictureUrl", "SubscriptionsEnabled", "TimePenalty", "WorkersCanDeleteBookings", "WorkersCanMoveBookings", "WorkersCanSetNonWorkingPeriods", "WorkingTimeId" },
                values: new object[,]
                {
                    { new Guid("216061ac-3614-450b-bf36-825d499ea89b"), 5, null, null, "Description", false, "Location", new Guid("1929324c-892c-4e94-b499-ab02a01ce38f"), "Cosa Nostra", null, true, 5, false, true, true, new Guid("4a727edd-4953-4505-8a45-d9b78a68b26f") },
                    { new Guid("e6f3364d-699d-415f-906b-71e2304b43d5"), 5, null, null, "Description", false, "Location", new Guid("1929324c-892c-4e94-b499-ab02a01ce38f"), "Gosho shop", null, true, 5, false, true, true, new Guid("d099c161-25b4-428f-843d-5a6a20b227a2") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("74827b6c-3a86-461e-afc1-d9ebf7fe7fed"), new Guid("3f685e89-8c61-47c0-b9a6-e96c279798de") },
                    { new Guid("7751b427-09fb-4ee9-bfdb-a1c0a803459a"), new Guid("3f685e89-8c61-47c0-b9a6-e96c279798de") },
                    { new Guid("74827b6c-3a86-461e-afc1-d9ebf7fe7fed"), new Guid("ea916748-33a8-4785-a883-9e1c449d7f22") },
                    { new Guid("7751b427-09fb-4ee9-bfdb-a1c0a803459a"), new Guid("ea916748-33a8-4785-a883-9e1c449d7f22") }
                });

            migrationBuilder.InsertData(
                table: "WorkingTimes",
                columns: new[] { "Id", "FridayClosingTime", "FridayOpeningTime", "MondayClosingTime", "MondayOpeningTime", "SalonId", "SaturdayClosingTime", "SaturdayOpeningTime", "SundayClosingTime", "SundayOpeningTime", "ThursdayClosingTime", "ThursdayOpeningTime", "TuesdayClosingTime", "TuesdayOpeningTime", "WednesdayClosingTime", "WednesdayOpeningTime" },
                values: new object[,]
                {
                    { new Guid("4a727edd-4953-4505-8a45-d9b78a68b26f"), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new Guid("216061ac-3614-450b-bf36-825d499ea89b"), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0) },
                    { new Guid("d099c161-25b4-428f-843d-5a6a20b227a2"), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new Guid("e6f3364d-699d-415f-906b-71e2304b43d5"), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0) }
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
