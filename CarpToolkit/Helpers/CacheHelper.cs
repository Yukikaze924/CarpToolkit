using Newtonsoft.Json;
using System.IO;

namespace CarpToolkit.Helpers
{
    public class CacheHelper
    {
        public static string FilePath;
        public static Cache Cache;

        public static void init(string filePath, Cache defaultCache)
        {
            FilePath = filePath;

            if (!CheckIfCacheExists())
            {
                Cache = defaultCache;
                Save();
            }
            else
            {
                Cache = Load();
            }
        }

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Cache);
            File.WriteAllText(FilePath, CryptHelper.EncryptString(json));
        }

        public static Cache? Load()
        {
            if (File.Exists(FilePath))
            {
                string json = CryptHelper.DecryptString(File.ReadAllText(FilePath));
                Cache = JsonConvert.DeserializeObject<Cache>(json);

                return Cache;
            }

            return null;
        }

        public static bool CheckIfCacheExists()
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
        public bool isFirstTime { get; set; } = true;
        public bool isLoggedIn { get; set; } = false;
        public string? account { get; set; }
        public string? nickname { get; set; }
        public string? password { get; set; }
        public string? avatar { get; set; }
    }
}
