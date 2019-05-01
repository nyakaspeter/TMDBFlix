using System;

using Microsoft.Toolkit.Uwp.UI.Animations;
using TMDBFlix.Controls;
using TMDBFlix.Core.Models;
using TMDBFlix.Services;
using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace TMDBFlix.Views
{
    public sealed partial class WebPage : CustomPage
    {

        public WebPage()
        {
            InitializeComponent();

            WebView.ContainsFullScreenElementChanged += WebView_ContainsFullScreenElementChanged;


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Video video)
            {
                WebView.Navigate(new Uri("https://www.youtube.com/watch_popup?v=" + video.key));
            }
            if (e.Parameter is Core.Models.Image image)
            {
                WebView.Navigate(new Uri("http://image.tmdb.org/t/p/original/" + image.file_path));
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            WebView.Navigate(new Uri("about:blank"));
        }

    }
}
