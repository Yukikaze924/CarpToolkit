using Newtonsoft.Json;
using System.IO;

namespace CarpToolkit.Helpers
{
    public class CacheHelper
    {
        public static string FilePath;
        public static Cache cache;

        public static void init(string filePath, Cache defaultCache)
        {
            FilePath = filePath;

            if (!CheckIfSettingsExists())
            {
                cache = defaultCache;
                Save();
            }
            else
            {
                cache = Load();
            }
        }

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(cache);
            File.WriteAllText(FilePath, json);
        }

        public static Cache? Load()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                cache = JsonConvert.DeserializeObject<Cache>(json);
                return cache;
            }

            return null;
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

    public class Cache
    {
        public bool isFirstTime;
        public bool isLoggedIn;
        public string? account;
        public string? nickname;

        public Cache(bool isFirstTime)
        {
            this.isFirstTime = isFirstTime;
        }
    }
}
