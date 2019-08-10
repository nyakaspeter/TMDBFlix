using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace TMDBFlix.Helpers
{
    /// <summary>
    /// Converts TMDb image path to image link string
    /// </summary>
    class IntToVotesStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var i = (int)value;
            return $"{i} {new ResourceLoader().GetString("Votes")}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
