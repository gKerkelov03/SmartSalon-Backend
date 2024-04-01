
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

    public class Validation
    {
        public class User
        {
            public const int FirstNameLength = 30;

            public const int LastNameLength = 30;

            public const int UserNameLength = 30;

            public const int PhoneNumberLength = 30;

            public const int EmailLength = 30;
        }
    }
}