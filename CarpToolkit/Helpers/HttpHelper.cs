using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CarpToolkit.Services;
using CommunityToolkit.Mvvm.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

namespace CarpToolkit.Helpers
{
    public class HttpHelper
    {
        public static async Task<UserModel?> LoginPostAsync(string account, string password)
        {
            using (var client = new HttpClient())
            {
                var config = Ioc.Default.GetRequiredService<ConfigService>().AppConfig;

                string url = $"{config.Host.address}:{config.Host.port.api}/login";
                var data = new StringContent("{\"account\":\""+account+"\", \"password\":\""+password+"\"}", System.Text.Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(url, data);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        UserModel user = JsonConvert.DeserializeObject<UserModel>(result);
                        return user;
                    }
                }
                catch(Exception ex)
                {
                    Ioc.Default.GetRequiredService<ILogger>().Error(ex.Message);
                    return null;
                }

                return null;
            }
        }

        public static async Task DownloadFile(string url, string filepath)
        {

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        long contentLength = content.Headers.ContentLength ?? -1;
                        using (var stream = await content.ReadAsStreamAsync())
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                byte[] buffer = new byte[4096];
                                int bytesRead = 0;
                                int totalBytesRead = 0;

                                //int currentProgress = 0;

                                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                                {
                                    totalBytesRead += bytesRead;
                                    //int currentProgress = (int)((double)totalBytesRead / contentLength * 100);
                                    //currentProgress++;
                                    //progress.Report(currentProgress);
                                    memoryStream.Write(buffer, 0, bytesRead);
                                }

                                File.WriteAllBytes($"{filepath}", memoryStream.ToArray());
                            }
                        }
                    }
                }
            }
        }
    }

    public class UserModel
    {
        public bool isUserValid { get; set; }
        public string account { get; set; }
        public string nickname { get; set; }
        public string password { get; set; }
    }
}
