﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace TMDBFlix.Helpers
{
    /// <summary>
    /// Converts torrent size to Gigabytes or Megabytes
    /// </summary>
    class TorrentSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = value as string;
            var bytes = double.Parse(val);
            var megabytes = bytes / 1024 / 1024;
            if (megabytes > 1024) return String.Format("{0:0.##} GB", megabytes/1024);
            else return String.Format("{0:0.##} MB", megabytes);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
