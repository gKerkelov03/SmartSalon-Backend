
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
            var binPath = Environment.CurrentDirectory;

            var partToRemove = Path.Combine(Path.DirectorySeparatorChar + "bin", "Debug", "net8.0");
            var appDirectory = binPath.Replace(partToRemove, string.Empty);

            return appDirectory;
        }
    }

    public static class Validation
    {
        public static class Role
        {
            public const int MaxNameLength = 30;
        }

        public static class User
        {
            public const int MaxFirstNameLength = 30;
            public const int MaxLastNameLength = 30;
            public const int MaxUserNameLength = 50;
            public const int MaxPhoneNumberLength = 30;
            public const int MaxEmailLength = 50;
        }

        public static class Worker
        {
            public const int MaxJobTitleLength = 30;
            public const int MaxNicknameLength = 30;
        }

        public static class Image
        {
            public const int MaxUrlLength = 200;
        }

        public static class Category
        {
            public const int MaxNameLength = 30;
        }

        public static class Section
        {
            public const int MaxNameLength = 30;
        }

        public static class Currency
        {
            public const int MaxCodeLength = 3;
            public const int MaxNameLength = 30;
            public const int MaxCountryLength = 30;
        }

        public static class Salon
        {
            public const int MaxNameLength = 30;
            public const int MaxDescriptionLength = 30;
            public const int MaxLocationLength = 30;
            public const int MaxDefaultTimePenalty = 20;
            public const int MaxDefaultBookingsInAdvance = 20;
        }

        public static class Service
        {
            public const int MaxNameLength = 30;
            public const int MaxDescriptionLength = 30;
            public const int MaxDuration = 300;
            public const int MinPrice = 0;
            public const int MaxPrice = 100_000;
        }

        public static class Specialty
        {
            public const int MaxTextLength = 200;
        }
    }
}