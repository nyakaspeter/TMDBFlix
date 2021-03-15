using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDBFlix.Models;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace TMDBFlix.Helpers
{
    /// <summary>
    /// Converts a search item to a string containing it's type
    /// </summary>
    class SearchItemInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var item = value as MultiSearchItem;
            var info = "";

            if (item.media_type.Equals("movie"))
            {
                info += new ResourceLoader().GetString("Movie");
                if (!item.release_date.Equals("")) info += " (" + item.release_date.Split("-")[0] + ")";
            }

            if (item.media_type.Equals("tv"))
            {
                info += new ResourceLoader().GetString("Show");
                if (!item.first_air_date.Equals("")) info += " (" + item.first_air_date.Split("-")[0] + ")";
            }

            if (item.media_type.Equals("person"))
            {
                info += new ResourceLoader().GetString("Person");
            }

            return info;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
