
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
}