using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using static IronPython.Runtime.Profiler;

namespace CarpToolkit.Helpers
{
    public static class ImageHelper
    {
        public static Bitmap LoadFromResource(Uri resourceUri)
        {
            return new Bitmap(AssetLoader.Open(resourceUri));
        }

        public static Bitmap LoadFromWeb(Uri url)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                var data = response.Content.ReadAsByteArrayAsync().Result;
                return new Bitmap(new MemoryStream(data));
            }
            catch
            {
                //return new Bitmap(AssetLoader.Open(new Uri("avares://CarpToolkit/Assets/Images/Github.png")));
                return null;
            }
        }

        public static Bitmap LoadFromBase64(string stringBase64)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(stringBase64);
                Bitmap bitmap = new Bitmap(new MemoryStream(imageBytes));

                return bitmap;
            }
            catch
            {
                return null;
            }
        }
    }
}
