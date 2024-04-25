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
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                name: "Salons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultTimePenalty = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    DefaultBookingsInAdvance = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    SubscriptionsEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SectionsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    WorkersCanMoveBookings = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    WorkersCanSetNonWorkingPeriods = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    WorkingTimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencySalon",
                columns: table => new
                {
                    CurrenciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalonsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencySalon", x => new { x.CurrenciesId, x.SalonsId });
                    table.ForeignKey(
                        name: "FK_CurrencySalon_Currencies_CurrenciesId",
                        column: x => x.CurrenciesId,
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
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Users_Salons_SalonId",
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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    From = table.Column<TimeOnly>(type: "time", nullable: false),
                    To = table.Column<TimeOnly>(type: "time", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                    { new Guid("12192bb3-7dce-4e86-8abb-42391d0da5f1"), "NZD", "New Zealand", "New Zealand Dollar" },
                    { new Guid("1432b166-3fe3-4b52-97d8-149c30d84103"), "ZAR", "South Africa", "South African Rand" },
                    { new Guid("18251110-da00-4fab-a4b6-0c293bfaeda7"), "PLN", "Poland", "Polish Zloty" },
                    { new Guid("1c67685b-d91f-4d3d-9f71-64fc1e79c6c8"), "MXN", "Mexico", "Mexican Peso" },
                    { new Guid("253c5944-a713-44fc-a63c-33469b50a84b"), "SAR", "Saudi Arabia", "Saudi Riyal" },
                    { new Guid("31fe350e-a18e-42d6-84e3-28131c5b0863"), "CNY", "China", "Chinese Yuan" },
                    { new Guid("39ab7507-0996-4189-87aa-e2f23f0cd59a"), "EUR", "Eurozone", "Euro" },
                    { new Guid("3eca4342-ec37-4434-8d09-03a4b7e4a887"), "JPY", "Japan", "Japanese Yen" },
                    { new Guid("4127a796-5d3a-4795-aaea-da7e8b3a6479"), "HKD", "Hong Kong", "Hong Kong Dollar" },
                    { new Guid("4aa0945c-7994-47b2-aa05-1409fca7f301"), "USD", "United States", "United States Dollar" },
                    { new Guid("54f0ea26-8c3e-4eeb-94c2-77bcf9a0cad1"), "NOK", "Norway", "Norwegian Krone" },
                    { new Guid("5d3a6ef1-1322-4832-8bb3-4adc1fbe9184"), "THB", "Thailand", "Thai Baht" },
                    { new Guid("60f1efe2-f05c-4caf-a76c-f508c3e2b720"), "SGD", "Singapore", "Singapore Dollar" },
                    { new Guid("6622f2bd-ce85-4aba-bf0a-7e1db8c7995f"), "CAD", "Canada", "Canadian Dollar" },
                    { new Guid("6e0e181b-033b-449c-b674-2e0bab1ef896"), "KRW", "South Korea", "South Korean Won" },
                    { new Guid("6e164664-33b0-432f-8b40-ad751fb83359"), "RUB", "Russia", "Russian Ruble" },
                    { new Guid("7b1bb34e-580f-4748-9902-85571af5346a"), "IDR", "Indonesia", "Indonesian Rupiah" },
                    { new Guid("88468f2d-dc5a-4f7b-8948-c7857fe3d061"), "SEK", "Sweden", "Swedish Krona" },
                    { new Guid("a2c01a7d-60fe-4e28-9161-5bdcf45b2e92"), "PHP", "Philippines", "Philippine Peso" },
                    { new Guid("b1ec7fa1-47a2-4c6b-a595-3debf62866f2"), "ILS", "Israel", "Israeli New Shekel" },
                    { new Guid("b8d9bed2-afd4-45d3-8898-4278092b0e35"), "DKK", "Denmark", "Danish Krone" },
                    { new Guid("b906558f-5e18-4d9b-92ed-1c531417dac2"), "BRL", "Brazil", "Brazilian Real" },
                    { new Guid("cbe8cd8b-16f0-4b07-8a45-76ffba9657ff"), "ARS", "Argentina", "Argentine Peso" },
                    { new Guid("d16d3aad-098b-46b4-854c-a74b6f9ae056"), "BGN", "Bulgaria", "Bulgarian Lev" },
                    { new Guid("d88f57be-372c-4fcd-b1c3-2bca1202b44d"), "MYR", "Malaysia", "Malaysian Ringgit" },
                    { new Guid("e10af2c5-558e-409b-b9c4-4039a798f010"), "INR", "India", "Indian Rupee" },
                    { new Guid("e489281f-9e77-4246-b11e-030775f8f2eb"), "AED", "United Arab Emirates", "UAE Dirham" },
                    { new Guid("ef0c3b7c-c990-418e-b764-24ea6690b7e3"), "CHF", "Switzerland", "Swiss Franc" },
                    { new Guid("f1763b88-a4e9-4a5f-9256-754fd72428c3"), "TRY", "Turkey", "Turkish Lira" },
                    { new Guid("f783780a-a1d3-4676-a0b1-83d371fa65ed"), "AUD", "Australia", "Australian Dollar" },
                    { new Guid("fbd01121-ca2a-40be-883b-b98ea519ccf0"), "GBP", "United Kingdom", "British Pound Sterling" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("476f536c-5c81-4484-8620-1682bb655d1b"), "3d961200-85de-4f65-b8b1-46b863b2e44e", "Owner", "OWNER" },
                    { new Guid("73daad71-f82b-46ac-84f8-3df8f67957de"), "bc38118b-edf8-42c4-8795-3212257e480a", "Worker", "WORKER" },
                    { new Guid("761c28c7-96f9-4df5-8d82-604722764be6"), "6b447ca6-d5bf-45e6-ba20-dc5cca35c5c1", "Admin", "ADMIN" },
                    { new Guid("981b548a-e90a-49a2-9215-571d171245d7"), "f8912a0f-e7e1-4616-abea-248c14df0d3a", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "Salons",
                columns: new[] { "Id", "DefaultBookingsInAdvance", "DefaultTimePenalty", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "Location", "Name", "ProfilePictureUrl", "SectionsEnabled", "SubscriptionsEnabled", "WorkersCanMoveBookings", "WorkersCanSetNonWorkingPeriods", "WorkingTimeId" },
                values: new object[,]
                {
                    { new Guid("1a84447a-0185-4c48-a11c-2030d02c0184"), 5, 5, null, null, "Description", false, "Location", "Gosho shop", null, false, true, true, true, new Guid("e025826c-b023-466d-9353-94d35967b916") },
                    { new Guid("83ef4a42-58dc-482b-bd31-5355bb0a8004"), 5, 5, null, null, "Description", false, "Location", "Cosa Nostra", null, false, true, true, true, new Guid("8e3df9fd-cb8c-42c6-aa37-66970680d3b8") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DeletedBy", "DeletedOn", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("3abb3356-5519-4629-b810-33145bc3b075"), 0, "7ad70d28-386d-48cb-9a53-4df104765689", null, null, "gkerkelov03@abv.bg", false, "Georgi", false, "Kerkelov", false, null, "GKERKELOV03@ABV.BG", "GKERKELOV03@ABV.BG", "AQAAAAIAAYagAAAAENSin22iKpt0pPEqKbS5zfNwxD/kpPhJvdrNdvD2DSt7wIqA1piULVbInQochjZN1g==", "0895105609", false, null, "c0de5c15-5570-42ab-bc4e-daa092e792f1", false, "gkerkelov03@abv.bg", "Admin" },
                    { new Guid("7b2eef30-e168-4100-b592-d2f878489760"), 0, "bed1016d-4f11-4e9d-bcce-64fe5a7adbb9", null, null, "pivanov03@abv.bg", false, "Petar", false, "Ivanov", false, null, "PIVANOV03@ABV.BG", "PIVANOV03@ABV.BG", "AQAAAAIAAYagAAAAEH/6FrhRNORCm6OCBsisOf0SiAa7u3Jt+yLt+NA3EbvZ8G9Wy9EVXK67kdD7qV0exQ==", "0899829897", false, null, "bcbd72e7-c3f2-49df-97d6-f73fa8ac02e9", false, "pivanov03@abv.bg", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("761c28c7-96f9-4df5-8d82-604722764be6"), new Guid("3abb3356-5519-4629-b810-33145bc3b075") },
                    { new Guid("761c28c7-96f9-4df5-8d82-604722764be6"), new Guid("7b2eef30-e168-4100-b592-d2f878489760") }
                });

            migrationBuilder.InsertData(
                table: "WorkingTimes",
                columns: new[] { "Id", "FridayFrom", "FridayTo", "MondayFrom", "MondayTo", "SalonId", "SaturdayFrom", "SaturdayTo", "SundayFrom", "SundayTo", "ThursdayFrom", "ThursdayTo", "TuesdayFrom", "TuesdayTo", "WednesdayFrom", "WednesdayTo" },
                values: new object[,]
                {
                    { new Guid("8e3df9fd-cb8c-42c6-aa37-66970680d3b8"), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new Guid("83ef4a42-58dc-482b-bd31-5355bb0a8004"), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0) },
                    { new Guid("e025826c-b023-466d-9353-94d35967b916"), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new Guid("1a84447a-0185-4c48-a11c-2030d02c0184"), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0), new TimeOnly(7, 0, 0), new TimeOnly(19, 0, 0) }
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
                name: "IX_Users_SalonId",
                table: "Users",
                column: "SalonId");

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
                name: "Logins");

            migrationBuilder.DropTable(
                name: "OwnerSalon");

            migrationBuilder.DropTable(
                name: "SpecialSlots");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "WorkingTimes");

            migrationBuilder.DropTable(
                name: "Currencies");

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
        }
    }
}
