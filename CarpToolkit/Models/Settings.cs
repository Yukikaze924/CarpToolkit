namespace CarpToolkit.Models
{
    public class Settings
    {
        public bool IsPaneOpen { get; set; }
        public string AppTheme { get; set; }
        public string PaneLocation { get; set; }

        public Settings(bool val1, string val2, string paneLocation)
        {
            IsPaneOpen = val1;
            AppTheme = val2;
            PaneLocation = paneLocation;
        }
    }
}
