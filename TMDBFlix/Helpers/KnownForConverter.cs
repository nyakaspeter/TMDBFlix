using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDBFlix.Core.Models;
using Windows.UI.Xaml.Data;

namespace TMDBFlix.Helpers
{
    class KnownForConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var knownfor = value as List<Performance>;
            var list = "";

            foreach (var v in knownfor)
            {
                if (v.media_type.Equals("tv")) list += v.name + ", ";
                if (v.media_type.Equals("movie")) list += v.title + ", ";
            }

            if (list.Length >= 2) list = list.Substring(0, list.Length - 2);
            else return "";

            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
