using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMDBFlix.Core.Models;
using TMDBFlix.Core.Services;
using TMDBFlix.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using TMDBFlix.Helpers;
using TMDBFlix.Controls;
using Windows.UI;
using Point = Windows.Foundation.Point;

namespace TMDBFlix.Views
{
    public sealed partial class ContentGridPage : CustomPage
    {
        public ContentGridViewModel ViewModel { get; } = new ContentGridViewModel();

        public ContentGridPage()
        {
            InitializeComponent();

            Loaded += ContentGridPage_Loaded;
            Scroller.ViewChanged += Scroller_ViewChanged;

            ViewModel.PopularMovies.CollectionChanged += PopularMovies_CollectionChanged;
            ViewModel.PopularShows.CollectionChanged += PopularShows_CollectionChanged;
            ViewModel.NowPlayingMovies.CollectionChanged += NowPlayingMovies_CollectionChanged;
            ViewModel.NowStreamingMovies.CollectionChanged += NowStreamingMovies_CollectionChanged;
            ViewModel.PopularPeople.CollectionChanged += PopularPeople_CollectionChanged;
            ViewModel.UpcomingMovies.CollectionChanged += UpcomingMovies_CollectionChanged;
            ViewModel.HighRatedMovies.CollectionChanged += HighRatedMovies_CollectionChanged;
            ViewModel.OnTvShows.CollectionChanged += OnTvShows_CollectionChanged;
            ViewModel.AiringTodayShows.CollectionChanged += AiringTodayShows_CollectionChanged;
            ViewModel.HighRatedShows.CollectionChanged += HighRatedShows_CollectionChanged;
            ViewModel.NonStaticSection.CollectionChanged += NonStaticSection_CollectionChanged;
        }

        private void NonStaticSection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ContentGridViewModel.Section.Count != 0) {
                if ((int)e.NewItems[0] == 0) Scroller.ScrollToElement(NowPlayingMovies);
                if ((int)e.NewItems[0] == 1) Scroller.ScrollToElement(OnTvShows);
                if ((int)e.NewItems[0] == 2) Scroller.ScrollToElement(PopularPeople);
            }
        }

        private void Scroller_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {

            var shows = OnTvShows.TransformToVisual((UIElement)Scroller.Content);
            var showspos = shows.TransformPoint(new Point(0, 0));

            var people = PopularPeople.TransformToVisual((UIElement)Scroller.Content);
            var peoplepos = people.TransformPoint(new Point(0, 0));

            if (Scroller.VerticalOffset == Scroller.ScrollableHeight) ShellPage.SetSection(2);
            else if (Scroller.VerticalOffset >= showspos.Y) ShellPage.SetSection(1);
            else ShellPage.SetSection(0);

        }

        private void HighRatedShows_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.HighRatedShows.Count == 0) { FadeOutHighRatedShows.Begin(); }
            else { FadeOutHighRatedShows.Stop(); HighRatedShows.Visibility = Visibility.Visible; FadeInHighRatedShows.Begin(); }
        }

        private void AiringTodayShows_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.AiringTodayShows.Count == 0) { FadeOutAiringTodayShows.Begin(); }
            else { FadeOutAiringTodayShows.Stop(); AiringTodayShows.Visibility = Visibility.Visible; FadeInAiringTodayShows.Begin(); }
        }

        private void OnTvShows_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.OnTvShows.Count == 0) { FadeOutOnTvShows.Begin(); }
            else { FadeOutOnTvShows.Stop(); OnTvShows.Visibility = Visibility.Visible; FadeInOnTvShows.Begin(); }
        }

        private void HighRatedMovies_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.HighRatedMovies.Count == 0) { FadeOutHighRatedMovies.Begin(); }
            else { FadeOutHighRatedMovies.Stop(); HighRatedMovies.Visibility = Visibility.Visible; FadeInHighRatedMovies.Begin(); }
        }

        private void UpcomingMovies_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.UpcomingMovies.Count == 0) { FadeOutUpcomingMovies.Begin(); }
            else { FadeOutUpcomingMovies.Stop(); UpcomingMovies.Visibility = Visibility.Visible; FadeInUpcomingMovies.Begin(); }
        }

        private void PopularPeople_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.PopularPeople.Count == 0) { FadeOutPopularPeople.Begin(); }
            else { FadeOutPopularPeople.Stop(); PopularPeople.Visibility = Visibility.Visible; FadeInPopularPeople.Begin(); }
        }

        private void NowStreamingMovies_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.NowStreamingMovies.Count == 0) { FadeOutNowStreamingMovies.Begin(); }
            else { FadeOutNowStreamingMovies.Stop(); NowStreamingMovies.Visibility = Visibility.Visible; FadeInNowStreamingMovies.Begin(); }
        }

        private void NowPlayingMovies_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.NowPlayingMovies.Count == 0) { FadeOutNowPlayingMovies.Begin(); }
            else { FadeOutNowPlayingMovies.Stop(); NowPlayingMovies.Visibility = Visibility.Visible; FadeInNowPlayingMovies.Begin(); }
        }

        private void PopularShows_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.PopularShows.Count == 0) { FadeOutPopularShows.Begin(); }
            else { FadeOutPopularShows.Stop(); PopularShows.Visibility = Visibility.Visible; FadeInPopularShows.Begin(); }
        }

        private void PopularMovies_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.PopularMovies.Count == 0) { FadeOutPopularMovies.Begin(); }
            else { FadeOutPopularMovies.Stop(); PopularMovies.Visibility = Visibility.Visible; FadeInPopularMovies.Begin(); }
        }

        private void ContentGridPage_Loaded(object sender, RoutedEventArgs e)
        {
            GetScrollViewer(PopularMovies).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(PopularShows).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(PopularPeople).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(NowPlayingMovies).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(NowStreamingMovies).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(UpcomingMovies).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(HighRatedMovies).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(OnTvShows).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(AiringTodayShows).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(HighRatedShows).HorizontalScrollMode = ScrollMode.Disabled;

            GetScrollViewer(PopularMovies).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(PopularShows).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(PopularPeople).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(NowPlayingMovies).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(NowStreamingMovies).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(UpcomingMovies).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(HighRatedMovies).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(OnTvShows).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(AiringTodayShows).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(HighRatedShows).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;

            FadeOutPopularMovies.Begin();
            FadeOutPopularShows.Begin();
            FadeOutNowPlayingMovies.Begin();
            FadeOutNowStreamingMovies.Begin();
            FadeOutPopularPeople.Begin();
            FadeOutUpcomingMovies.Begin();
            FadeOutHighRatedMovies.Begin();
            FadeOutOnTvShows.Begin();
            FadeOutAiringTodayShows.Begin();
            FadeOutHighRatedShows.Begin();
        }

        private void NowPlayingMoviesBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NowPlayingMoviesGridPage));
        }

        private void NowStreamingMoviesBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NowStreamingMoviesGridPage));
        }

        private void UpcomingMoviesBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpcomingMoviesGridPage));
        }

        private void PopularMoviesBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PopularMoviesGridPage));
        }

        private void HighRatedMoviesBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HighRatedMoviesGridPage));
        }

        private void OnTvShowsBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(OnTvShowsGridPage));
        }

        private void AiringTodayShowsBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AiringTodayShowsGridPage));
        }

        private void PopularShowsBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PopularShowsGridPage));
        }

        private void HighRatedShowsBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HighRatedShowsGridPage));
        }

        private void PopularPeopleBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PopularPeopleGridPage));
        }
    }
}
