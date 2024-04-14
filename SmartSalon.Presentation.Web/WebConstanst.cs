namespace SmartSalon.Presentation.Web;

internal static class WebConstants
{
    public const string IdRoute = "{Id}";
    public const string SettingsFilesFolderName = "Settings";

    internal static class PolicyNames
    {
        public const string IsAdminPolicy = nameof(IsAdminPolicy);
        public const string IsOwnerOrAdminPolicy = nameof(IsOwnerOrAdminPolicy);
        public const string IsTheSameUserOrAdminPolicy = nameof(IsTheSameUserOrAdminPolicy);
        public const string IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerPolicy = nameof(IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerPolicy);
        public const string IsOwnerOfTheSalonOrIsAdminPolicy = nameof(IsOwnerOfTheSalonOrIsAdminPolicy);
    }
}