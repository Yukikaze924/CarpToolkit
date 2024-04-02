using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using CarpToolkit.Helpers;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;

namespace CarpToolkit.Converters
{
    public class StringToUrlBitmapConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ImageHelper.LoadFromResource(new Uri(value.ToString()));
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
