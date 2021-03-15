using System;

using Microsoft.Toolkit.Uwp.UI.Animations;
using TMDBFlix.Controls;
using TMDBFlix.Models;
using TMDBFlix.Services;
using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace TMDBFlix.Views
{
    public sealed partial class ImagePage : CustomPage
    {

        public ImagePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Models.Image image)
            {
                Img.Source = new BitmapImage(new Uri("http://image.tmdb.org/t/p/original/" + image.file_path));
                Scroller.ZoomToFactor(0.95f);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
        }
    }
}
