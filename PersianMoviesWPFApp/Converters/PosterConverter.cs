using PersianMoviesWPFApp.Utilities;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PersianMoviesWPFApp.Converters
{
    public class PosterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value?.ToString()))
            {
                return Variable.ImageFullPath + value;
            }

            return Variable.DefaultPoster;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
