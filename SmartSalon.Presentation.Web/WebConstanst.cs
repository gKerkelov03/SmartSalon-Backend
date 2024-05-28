namespace SmartSalon.Presentation.Web;

internal static class WebConstants
{

    public const string AngularLocalhostCorsPolicy = "Angular-Localhost-Cors-Policy";
    public const string IdRoute = "{Id}";
    public const string IdRouteParameterName = "Id";
    public const string SettingsFilesFolderName = "Settings";

    internal static class PolicyNames
    {
        public const string IsAdminPolicy = nameof(IsAdminPolicy);
        public const string IsOwnerOrIsAdminPolicy = nameof(IsOwnerOrIsAdminPolicy);
        public const string IsTheSameUserOrIsAdminPolicy = nameof(IsTheSameUserOrIsAdminPolicy);
        public const string IsOwnerOfTheSalonOfTheWorkerOrIsAdminPolicy = nameof(IsOwnerOfTheSalonOfTheWorkerOrIsAdminPolicy);
        public const string IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerOrIsAdminPolicy = nameof(IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerOrIsAdminPolicy);
        public const string IsOwnerOfTheSalonOrIsAdminPolicy = nameof(IsOwnerOfTheSalonOrIsAdminPolicy);
        public const string IsOwnerOfTheSalonOrIsTheCustomerOrIsTheWorkerOrIsAdminPolicy = nameof(IsOwnerOfTheSalonOrIsTheCustomerOrIsTheWorkerOrIsAdminPolicy);
    }
}