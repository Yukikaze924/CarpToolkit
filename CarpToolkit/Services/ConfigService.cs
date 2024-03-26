using CarpToolkit.Helpers;
using CarpToolkit.Models;
using Newtonsoft.Json;
using System.IO;

namespace CarpToolkit.Services
{
    public class ConfigService
    {
        public required string Path;
        public required AppConfig AppConfig;

        //public ConfigService(string path, AppConfig appConfig)
        //{
        //    Path = path;
        //    AppConfig = appConfig;
        //}

        /// <summary>
        /// Only work in Ioc
        /// </summary>
        public ConfigService Build()
        {
            if (!File.Exists(Path))
            {
                File.WriteAllText(Path, CryptHelper.EncryptString(JsonConvert.SerializeObject(AppConfig)));
            }
            else
            {
                AppConfig = JsonConvert.DeserializeObject<AppConfig>(CryptHelper.DecryptString(File.ReadAllText(Path)));
            }

            return this;
        }
    }
}
