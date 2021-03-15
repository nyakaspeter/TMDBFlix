using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDBFlix.Models;
using Windows.UI.Xaml.Data;

namespace TMDBFlix.Helpers
{
    /// <summary>
    /// Converts a search item to a string containing it's title
    /// </summary>
    class SearchItemNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var item = value as MultiSearchItem;

            if (item.media_type.Equals("movie")) return item.title;
            else return item.name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
