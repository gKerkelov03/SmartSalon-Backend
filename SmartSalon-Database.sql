USE SmartSalon;

CREATE TABLE BookingTimes (
    FromTime TIME (7)         NOT NULL,
    ToTime   TIME (7)         NOT NULL,
    Id       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_BookingTimes PRIMARY KEY CLUSTERED (FromTime ASC, ToTime ASC)
);

CREATE TABLE Bookings (
    Id           UNIQUEIDENTIFIER NOT NULL,
    Date         DATE             NOT NULL,
    TimeId       UNIQUEIDENTIFIER NOT NULL,
    TimeFrom     TIME (7)         NOT NULL,
    TimeTo       TIME (7)         NOT NULL,
    CustomerId   UNIQUEIDENTIFIER NOT NULL,
    SalonId      UNIQUEIDENTIFIER NOT NULL,
    WorkerId     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_Bookings PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT FK_Bookings_BookingTimes_TimeFrom_TimeTo FOREIGN KEY (TimeFrom, TimeTo) REFERENCES BookingTimes (FromTime, ToTime),
    CONSTRAINT FK_Bookings_Customers_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customers (Id),
    CONSTRAINT FK_Bookings_Salons_SalonId FOREIGN KEY (SalonId) REFERENCES Salons (Id),
    CONSTRAINT FK_Bookings_Workers_WorkerId FOREIGN KEY (WorkerId) REFERENCES Workers (Id)
);

CREATE TABLE Image (
    Id  UNIQUEIDENTIFIER NOT NULL,
    Url NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT PK_Image PRIMARY KEY CLUSTERED (Id ASC)
);

CREATE TABLE Salons (
    Id                             UNIQUEIDENTIFIER NOT NULL,
    Name                           NVARCHAR (MAX)   NOT NULL,
    Description                    NVARCHAR (MAX)   NOT NULL,
    Location                       NVARCHAR (MAX)   NOT NULL,
    SubscriptionsEnabled           BIT              NOT NULL,
    WorkersCanMoveBookings         BIT              NOT NULL,
    WorkersCanSetNonWorkingPeriods BIT              NOT NULL,
    OwnerId                        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_Salons PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT FK_Salons_Owners_OwnerId FOREIGN KEY (OwnerId) REFERENCES Owners (Id)
);

CREATE TABLE SalonSalonService (
    SalonsId   UNIQUEIDENTIFIER NOT NULL,
    ServicesId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_SalonSalonService PRIMARY KEY CLUSTERED (SalonsId ASC, ServicesId ASC),
    CONSTRAINT FK_SalonSalonService_Salons_SalonsId FOREIGN KEY (SalonsId) REFERENCES Salons (Id),
    CONSTRAINT FK_SalonSalonService_SalonServices_ServicesId FOREIGN KEY (ServicesId) REFERENCES SalonServices (Id)
);

CREATE TABLE SalonSalonSpecialty (
    SalonSpecialtiesId UNIQUEIDENTIFIER NOT NULL,
    SalonsId           UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_SalonSalonSpecialty PRIMARY KEY CLUSTERED (SalonSpecialtiesId ASC, SalonsId ASC),
    CONSTRAINT FK_SalonSalonSpecialty_Salons_SalonsId FOREIGN KEY (SalonsId) REFERENCES Salons (Id),
    CONSTRAINT FK_SalonSalonSpecialty_SalonSpecialties_SalonSpecialtiesId FOREIGN KEY (SalonSpecialtiesId) REFERENCES SalonSpecialties (Id)
);

CREATE TABLE Subscriptions (
    Id                       UNIQUEIDENTIFIER NOT NULL,
    TimePenaltyInDays        INT              NOT NULL,
    AllowedBookingsInAdvance INT              NOT NULL,
    Tier                     INT              NOT NULL,
    Duration                 INT              NOT NULL,
    SalonId                  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_Subscriptions PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT FK_Subscriptions_Salons_SalonId FOREIGN KEY (SalonId) REFERENCES Salons (Id)
);

CREATE TABLE SpecialSlots (
    Id              UNIQUEIDENTIFIER NOT NULL,
    BookingTimeId   UNIQUEIDENTIFIER NOT NULL,
    BookingTimeFrom TIME (7)         NOT NULL,
    BookingTimeTo   TIME (7)         NOT NULL,
    CONSTRAINT PK_SpecialSlots PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT FK_SpecialSlots_BookingTimes_BookingTimeFrom_BookingTimeTo FOREIGN KEY (BookingTimeFrom, BookingTimeTo) REFERENCES BookingTimes (FromTime, ToTime)
);

CREATE TABLE SpecialSlotSubscription (
    SpecialSlotsId UNIQUEIDENTIFIER NOT NULL,
    SubscriptionId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_SpecialSlotSubscription PRIMARY KEY CLUSTERED (SpecialSlotsId ASC, SubscriptionId ASC),
    CONSTRAINT FK_SpecialSlotSubscription_SpecialSlots_SpecialSlotsId FOREIGN KEY (SpecialSlotsId) REFERENCES SpecialSlots (Id),
    CONSTRAINT FK_SpecialSlotSubscription_Subscriptions_SubscriptionId FOREIGN KEY (SubscriptionId) REFERENCES Subscriptions (Id)
);

CREATE TABLE AspNetUsers (
    Id                   UNIQUEIDENTIFIER   NOT NULL,
    FirstName            NVARCHAR (MAX)     NOT NULL,
    LastName             NVARCHAR (MAX)     NOT NULL,
    ProfilePictureUrl    NVARCHAR (MAX)     NOT NULL,
    RoleId               UNIQUEIDENTIFIER   NOT NULL,
    CreatedOn            DATETIME2 (7)      NOT NULL,
    CreatedBy            UNIQUEIDENTIFIER   NULL,
    LastModifiedOn       DATETIME2 (7)      NULL,
    LastModifiedBy       UNIQUEIDENTIFIER   NULL,
    IsDeleted            BIT                NOT NULL,
    DeletedOn            DATETIME2 (7)      NULL,
    DeletedBy            UNIQUEIDENTIFIER   NULL,
    UserName             NVARCHAR (256)     NULL,
);