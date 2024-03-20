using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarpToolkit.Helpers
{
    public class HttpHelper
    {
        public static async Task<UserModel?> LoginPostAsync(string apiUrl, string account, string password)
        {
            using (var client = new HttpClient())
            {
                string url = apiUrl;
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
                    SerilogService.logger.Error(ex.Message);
                    return null;
                }

                SerilogService.logger.Error("HttpClient accidently break out. Might because of response code is bad!");
                return null;
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
