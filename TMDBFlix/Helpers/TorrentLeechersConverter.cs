using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace TMDBFlix.Helpers
{
    class TorrentLeechersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = value as List<TMDBFlix.Core.Models.Attr>;
            int peers = 0;
            int seeds = 0;
            foreach (var v in val)
            {
                if (v.Name.Equals("peers")) peers = int.Parse(v.Value);
                if (v.Name.Equals("seeders")) seeds = int.Parse(v.Value);
            }
            return $"Leech: {peers - seeds}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
