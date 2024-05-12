
namespace SmartSalon.Application;

public static class ApplicationConstants
{
    public const string AdminRoleName = "Admin";
    public const string OwnerRoleName = "Owner";
    public const string WorkerRoleName = "Worker";
    public const string CustomerRoleName = "Customer";

    public static string AppDirectory
    {
        get
        {
            var presentationLayerName = "SmartSalon.Presentation.Web";

            var partToRemove = Path.DirectorySeparatorChar + presentationLayerName;
            var partToRemoveIfBin = Path.Combine(Path.DirectorySeparatorChar + presentationLayerName, "bin", "Debug", "net8.0");

            return Environment.CurrentDirectory
                .Replace(partToRemoveIfBin, string.Empty)
                .Replace(partToRemove, string.Empty);
        }
    }

    public static class Validation
    {
        public static class Role
        {
            public const int MaxNameLength = 50;
        }

        public static class User
        {
            public const int MaxFirstNameLength = 50;
            public const int MaxLastNameLength = 50;
            public const int MaxUserNameLength = 50;
            public const int MaxPhoneNumberLength = 50;
            public const int MaxEmailLength = 50;
            public const int MinPasswordLength = 6;
        }

        public static class Worker
        {
            public const int MaxNicknameLength = 50;
        }

        public static class Salon
        {
            public const int MaxNameLength = 50;
            public const int MaxDescriptionLength = 50;
            public const int MaxGoogleMapsLocationLength = 50;
            public const int MaxTimePenalty = 20;
            public const int MaxBookingsInAdvance = 20;
        }

        public static class Specialty
        {
            public const int MaxTextLength = 200;
        }

        public static class JobTitle
        {
            public const int MaxNameLength = 200;
        }

        public static class Currency
        {
            public const int MaxCodeLength = 3;
            public const int MaxNameLength = 50;
            public const int MaxCountryLength = 50;
        }

        public static class Service
        {
            public const int MaxNameLength = 50;
            public const int MaxDescriptionLength = 50;
            public const int MaxDuration = 500;
            public const int MinPrice = 0;
            public const int MaxPrice = 100_000;
        }

        public static class Category
        {
            public const int MaxNameLength = 50;
        }

        public static class Section
        {
            public const int MaxNameLength = 50;
        }
    }
}