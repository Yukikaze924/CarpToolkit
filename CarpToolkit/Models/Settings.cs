namespace CarpToolkit.Models
{
    public class Settings
    {
        public string AppTheme { get; set; } = "Default";
        public int Width { get; set; } = 960;
        public int Height { get; set; } = 600;
        public bool UseDefault { get; set; } = true;
        public bool IsPaneOpen { get; set; } = true;
        public string PaneLocation { get; set; } = "Left";

        //public Settings(bool val1, string val2, string paneLocation)
        //{
        //    IsPaneOpen = val1;
        //    AppTheme = val2;
        //    PaneLocation = paneLocation;
        //}
    }
}
