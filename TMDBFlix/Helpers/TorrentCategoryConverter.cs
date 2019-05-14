using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace TMDBFlix.Helpers
{
    /// <summary>
    /// Converts torrent attributes to category name
    /// </summary>
    class TorrentCategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = value as List<TMDBFlix.Core.Models.Attr>;
            foreach (var v in val)
            {
                if (v.Name.Equals("category") && v.Value.Length >= 2)
                {
                    if(v.Value.Substring(v.Value.Length - 2).Equals("30")) return "SD";
                    else if(v.Value.Substring(v.Value.Length - 2).Equals("40")) return "HD";
                    else if(v.Value.Substring(v.Value.Length - 2).Equals("70")) return "DVD";
                }
                
            }
            return "N/A";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
