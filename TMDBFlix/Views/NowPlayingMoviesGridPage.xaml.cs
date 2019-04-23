using System;
using Windows.UI.Xaml;
using TMDBFlix.Controls;
using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;

namespace TMDBFlix.Views
{
    public sealed partial class NowPlayingMoviesGridPage : CustomPage
    {
        public NowPlayingMoviesGridViewModel ViewModel { get; } = new NowPlayingMoviesGridViewModel();

        public NowPlayingMoviesGridPage()
        {
            InitializeComponent();
            Scroller.ViewChanged += Scroller_ViewChanged;
            Scroller.Loaded += Scroller_Loaded;
        }

        private void Scroller_Loaded(object sender, RoutedEventArgs e)
        {
            if (Scroller.ScrollableHeight == 0) ViewModel.LoadData();
        }

        private void Scroller_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (Scroller.VerticalOffset == Scroller.ScrollableHeight) ViewModel.LoadData();
        }
    }
}
