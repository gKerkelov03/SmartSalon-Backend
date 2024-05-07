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
                    MondayFrom = table.Column<TimeOnly>(type: "time", nullable: false),
                    MondayTo = table.Column<TimeOnly>(type: "time", nullable: false),
                    TuesdayFrom = table.Column<TimeOnly>(type: "time", nullable: false),
                    TuesdayTo = table.Column<TimeOnly>(type: "time", nullable: false),
                    WednesdayFrom = table.Column<TimeOnly>(type: "time", nullable: false),
                    WednesdayTo = table.Column<TimeOnly>(type: "time", nullable: false),
                    ThursdayFrom = table.Column<TimeOnly>(type: "time", nullable: false),
                    ThursdayTo = table.Column<TimeOnly>(type: "time", nullable: false),
                    FridayFrom = table.Column<TimeOnly>(type: "time", nullable: false),
                    FridayTo = table.Column<TimeOnly>(type: "time", nullable: false),
                    SaturdayFrom = table.Column<TimeOnly>(type: "time", nullable: false),
                    SaturdayTo = table.Column<TimeOnly>(type: "time", nullable: false),
                    SundayFrom = table.Column<TimeOnly>(type: "time", nullable: false),
                    SundayTo = table.Column<TimeOnly>(type: "time", nullable: false)
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
                    From = table.Column<TimeOnly>(type: "time", nullable: false),
                    To = table.Column<TimeOnly>(type: "time", nullable: false),
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
                name: "SpecialSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<TimeOnly>(type: "time", nullable: false),
                    To = table.Column<TimeOnly>(type: "time", nullable: false),
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
                    { new Guid("071a21d1-bd6b-49a4-8cae-d2010dd95ce5"), "CNY", "China", "Chinese Yuan" },
                    { new Guid("078406ad-7bcc-45f7-a2c4-9b441ce851df"), "MYR", "Malaysia", "Malaysian Ringgit" },
                    { new Guid("080893fe-e916-472d-b2e8-2cb69b2298ad"), "BRL", "Brazil", "Brazilian Real" },
                    { new Guid("0ee53b88-f387-4e3a-b6f6-ddad12ddc4ac"), "PHP", "Philippines", "Philippine Peso" },
                    { new Guid("11a257f0-101b-4efe-af08-fe86ca293a50"), "NOK", "Norway", "Norwegian Krone" },
                    { new Guid("143b65bd-a7cc-4546-b3b6-97393774d9c2"), "ILS", "Israel", "Israeli New Shekel" },
                    { new Guid("14d31af8-e264-4bc3-a6bf-315b8f5f86be"), "ARS", "Argentina", "Argentine Peso" },
                    { new Guid("1c4e22f6-d55a-4441-ab97-e9f87255983b"), "HKD", "Hong Kong", "Hong Kong Dollar" },
                    { new Guid("1e7f65bb-6495-43ed-aa9d-8c7b80acfd11"), "DKK", "Denmark", "Danish Krone" },
                    { new Guid("34222c00-6c36-4904-8f18-2b11195fbaf3"), "TRY", "Turkey", "Turkish Lira" },
                    { new Guid("3f2c1552-d4be-45bd-85d0-04d503fcc416"), "BTC", null, "Bitcoin" },
                    { new Guid("4868d264-12d0-49e9-b1ac-b096c94794fd"), "SGD", "Singapore", "Singapore Dollar" },
                    { new Guid("4c8c79c7-d252-4ade-b5e3-521b92a1c534"), "MXN", "Mexico", "Mexican Peso" },
                    { new Guid("50d2bc54-df35-49c1-8b9a-2b2a6fb0f984"), "EUR", "Eurozone", "Euro" },
                    { new Guid("52476e17-1824-440d-8faf-6b54ba3638d4"), "PLN", "Poland", "Polish Zloty" },
                    { new Guid("5a447c0b-7527-444a-a405-66fe52612de8"), "SAR", "Saudi Arabia", "Saudi Riyal" },
                    { new Guid("62965c75-fa31-4b75-ac91-4aae76834c06"), "ZAR", "South Africa", "South African Rand" },
                    { new Guid("6b659dd6-2da7-4155-8c53-196432f91f49"), "ETH", null, "Ethereum" },
                    { new Guid("79df986c-72f3-4d5f-babe-4a7e3e31f66b"), "JPY", "Japan", "Japanese Yen" },
                    { new Guid("7c8b35ec-95cd-479d-810a-f83bb2c51757"), "THB", "Thailand", "Thai Baht" },
                    { new Guid("86116363-9266-4d3b-9995-1bc4ca84d85e"), "USD", "United States", "United States Dollar" },
                    { new Guid("91446a43-acee-4f89-abd1-35cdfcb02aa3"), "NZD", "New Zealand", "New Zealand Dollar" },
                    { new Guid("94124332-0228-4eb6-aa04-a8d097e3594b"), "CAD", "Canada", "Canadian Dollar" },
                    { new Guid("980ca807-343e-45aa-aeda-2024a76307e2"), "GBP", "United Kingdom", "British Pound Sterling" },
                    { new Guid("a4cc4b23-c716-4461-8007-463a95967042"), "RUB", "Russia", "Russian Ruble" },
                    { new Guid("b219c361-805c-44be-b423-f9f86f74e485"), "AED", "United Arab Emirates", "UAE Dirham" },
                    { new Guid("b4635b82-aa1a-4b75-ad01-4b7c3bb79035"), "KRW", "South Korea", "South Korean Won" },
                    { new Guid("c58ae5e6-a06b-435d-abc4-00ca75364b31"), "INR", "India", "Indian Rupee" },
                    { new Guid("cadea734-63b9-43c1-ac1d-9bd19bc341d5"), "AUD", "Australia", "Australian Dollar" },
                    { new Guid("ce4ea41f-24ff-4495-bcdf-762d3f11b1e4"), "SEK", "Sweden", "Swedish Krona" },
                    { new Guid("df2cadc8-3222-4829-bdeb-7c046ffcd70d"), "BGN", "Bulgaria", "Bulgarian Lev" },
                    { new Guid("e0007d55-7f0d-459e-9019-a58bd452b2d7"), "IDR", "Indonesia", "Indonesian Rupiah" },
                    { new Guid("f852f467-4322-45fa-abc8-bc0b29cfa668"), "CHF", "Switzerland", "Swiss Franc" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4dab9b99-ff64-4df7-a86d-3045237b9546"), "09c3aecd-f14e-4a70-8d24-0dbaa05ed13e", "Customer", "CUSTOMER" },
                    { new Guid("7157b4a1-1fab-4967-91ae-37bb8d6f4b67"), "ec232caf-f29f-4382-984e-a6d56c4b5270", "Owner", "OWNER" },
                    { new Guid("ade7d6f6-9525-446c-9e06-f45d637970a2"), "aa4c20a9-a962-460e-ac4f-6147ea89c887", "Admin", "ADMIN" },
                    { new Guid("eaadcb52-b338-449d-9751-f011ee72b789"), "3ed89e36-fcac-44fb-9b42-c335e5808ee0", "Worker", "WORKER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("76599f96-903d-4cb8-818a-95ef7fde5ed6"), 0, "0d6b75e0-6b36-4a85-8a4b-65e5ec0e8b1f", "gkerkelov03@abv.bg", true, "Georgi", "Kerkelov", false, null, "GKERKELOV03@ABV.BG", "GKERKELOV03@ABV.BG", "AQAAAAIAAYagAAAAEGJGDaN2YyOqE9oH+Qk+7nS7U52Ktg+o7ceWvVEyTFTvzDpNnVz7NsG9mYizXP1kAw==", "0895105609", false, "https://res.cloudinary.com/donhvedgr/image/upload/v1662969813/blank-profile-picture_cqowyq.webp", "189f913e-59bc-4bfc-84b1-c4d569b7163c", false, "gkerkelov03@abv.bg", "Admin" },
                    { new Guid("caa95241-6c64-48c3-82c4-1f8558872dd4"), 0, "eb40f3a4-f52d-41e9-95c6-70f4655bd879", "pivanov03@abv.bg", true, "Petar", "Ivanov", false, null, "PIVANOV03@ABV.BG", "PIVANOV03@ABV.BG", "AQAAAAIAAYagAAAAEClBcbRzt1xamG4jel9jJoK1QraEvzb6/NUuTf/Hv2VO/MklrtZvLQsHSSw1abpMdg==", "0899829897", false, "https://res.cloudinary.com/donhvedgr/image/upload/v1662969813/blank-profile-picture_cqowyq.webp", "c1d5a94f-12bb-4684-bf88-ed3f5cacc8b3", false, "pivanov03@abv.bg", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Salons",
                columns: new[] { "Id", "BookingsInAdvance", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "Location", "MainCurrencyId", "Name", "ProfilePictureUrl", "SubscriptionsEnabled", "TimePenalty", "WorkersCanDeleteBookings", "WorkersCanMoveBookings", "WorkersCanSetNonWorkingPeriods", "WorkingTimeId" },
                values: new object[,]
                {
                    { new Guid("4478b3a6-9ff9-4287-9399-bda89ff5b599"), 5, null, null, "Description", false, "Location", new Guid("df2cadc8-3222-4829-bdeb-7c046ffcd70d"), "Cosa Nostra", null, true, 5, false, true, true, new Guid("60cbb27e-d5e6-44a2-98ec-eabbfca37154") },
                    { new Guid("d7b18c1c-e838-497f-97d1-6fbbc63a792c"), 5, null, null, "Description", false, "Location", new Guid("df2cadc8-3222-4829-bdeb-7c046ffcd70d"), "Gosho shop", null, true, 5, false, true, true, new Guid("4c5d742e-4651-4724-8ed7-03b34c9b7965") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("4dab9b99-ff64-4df7-a86d-3045237b9546"), new Guid("76599f96-903d-4cb8-818a-95ef7fde5ed6") },
                    { new Guid("ade7d6f6-9525-446c-9e06-f45d637970a2"), new Guid("76599f96-903d-4cb8-818a-95ef7fde5ed6") },
                    { new Guid("4dab9b99-ff64-4df7-a86d-3045237b9546"), new Guid("caa95241-6c64-48c3-82c4-1f8558872dd4") },
                    { new Guid("ade7d6f6-9525-446c-9e06-f45d637970a2"), new Guid("caa95241-6c64-48c3-82c4-1f8558872dd4") }
                });

            migrationBuilder.InsertData(
                table: "WorkingTimes",
                columns: new[] { "Id", "FridayFrom", "FridayTo", "MondayFrom", "MondayTo", "SalonId", "SaturdayFrom", "SaturdayTo", "SundayFrom", "SundayTo", "ThursdayFrom", "ThursdayTo", "TuesdayFrom", "TuesdayTo", "WednesdayFrom", "WednesdayTo" },
                values: new object[,]
                {
                    { new Guid("4c5d742e-4651-4724-8ed7-03b34c9b7965"), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new Guid("d7b18c1c-e838-497f-97d1-6fbbc63a792c"), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0) },
                    { new Guid("60cbb27e-d5e6-44a2-98ec-eabbfca37154"), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new Guid("4478b3a6-9ff9-4287-9399-bda89ff5b599"), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0) }
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
