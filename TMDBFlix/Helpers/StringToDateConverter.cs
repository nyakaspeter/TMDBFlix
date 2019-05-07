using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace TMDBFlix.Helpers
{
    /// <summary>
    /// Converts a date string to "yyyy. MM. dd." format date string
    /// </summary>
    class StringToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var str = value as string;
            if (str is null) return "N/A";
            var date = DateTime.Parse(str).ToString("yyyy. MM. dd.");
            if (date is null) return "N/A";
            return date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
