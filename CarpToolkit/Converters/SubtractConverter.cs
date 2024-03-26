using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace CarpToolkit.Converters
{
    public class SubtractConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                int intValue;
                if (int.TryParse(value.ToString(), out intValue))
                {
                    return intValue - 500;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
