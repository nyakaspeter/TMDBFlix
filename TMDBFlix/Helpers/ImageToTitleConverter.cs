﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDBFlix.Core.Models;
using Windows.UI.Xaml.Data;

namespace TMDBFlix.Helpers
{
    class ImageToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var media = value as Media;
            if (media.name != null) return media.name;
            if (media.title != null) return media.title;
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
