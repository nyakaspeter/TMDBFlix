using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDBFlix.Core.Models;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace TMDBFlix.Helpers
{
    class SearchItemImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var item = value as MultiSearchItem;

            if (item.media_type.Equals("movie") || item.media_type.Equals("tv"))
            {
                if (item.poster_path != null) return "https://image.tmdb.org/t/p/w500" + item.poster_path;
            }

            if (item.media_type.Equals("person"))
            {
                if (item.profile_path != null) return "https://image.tmdb.org/t/p/w500" + item.profile_path;
            }

            return "ms-appx:///Assets/Placeholder.jpg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
