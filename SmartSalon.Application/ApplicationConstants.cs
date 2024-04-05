
namespace SmartSalon.Application;

public static class ApplicationConstants
{
    public const string SystemName = "SmartSalon";

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
        public static class User
        {
            public const int FirstNameLength = 30;
            public const int LastNameLength = 30;
            public const int UserNameLength = 50;
            public const int PhoneNumberLength = 30;
            public const int EmailLength = 50;
        }

        public static class Worker
        {
            public const int JobTitleLength = 30;
            public const int NicknameLength = 30;
        }

        public static class Salon
        {
            public const int NameLength = 30;
            public const int DescriptionLength = 30;
            public const int LocationLength = 30;
            public const int MaximumDefaultTimePenalty = 20;
            public const int MaximumDefaultBookingsInAdvance = 20;
        }
    }
}