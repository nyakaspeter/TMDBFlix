using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace TMDBFlix.Helpers
{
    /// <summary>
    /// Converts TMDb image path to image link string
    /// </summary>
    class FloatToOneDecimalStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var f = (float)value;
            return $"{f:0.0}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
