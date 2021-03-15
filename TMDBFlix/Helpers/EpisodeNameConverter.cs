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
    /// Converts Episode to string "Episode X: Episode Name"
    /// </summary>
    class EpisodeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var ep = value as Episode;
            if (ep is null) return "N/A";
            return new ResourceLoader().GetString("Episode/Text") + " " + ep.episode_number + ": " + ep.name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
