using Avalonia;
using Avalonia.Styling;
using CarpToolkit.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CarpToolkit.Helpers
{
    public class SettingsHelper
    {
        public static string FilePath;
        public static Settings Settings;

        public static void init(string filePath, Settings defaultSettings)
        {
            FilePath = filePath;

            if (!CheckIfSettingsExists())
            {
                Settings = defaultSettings;
                Save();
            }
            else
            {
                Settings = Load();
            }
        }

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Settings);
            File.WriteAllText(FilePath, json);
        }

        public static Settings? Load()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                Settings = JsonConvert.DeserializeObject<Settings>(json);
                return Settings;
            }

            return null;
        }

        public static void ChangeThemeVariantByString(string value)
        {
            if (value != null)
            {
                switch (value)
                {
                    case "Default":
                        App.Current!.RequestedThemeVariant = ThemeVariant.Default;
                        break;

                    case "Light":
                        App.Current!.RequestedThemeVariant = ThemeVariant.Light;
                        break;

                    case "Dark":
                        App.Current!.RequestedThemeVariant = ThemeVariant.Dark;
                        break;
                }
            }
        }

        public static bool CheckIfSettingsExists()
        {
            if (File.Exists(FilePath))
            {
                return true;
            }
            else { return false; }
        }
    }
}
