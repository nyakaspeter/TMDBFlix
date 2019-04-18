using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMDBFlix.Core.Models;
using TMDBFlix.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TMDBFlix.Views
{
    public sealed partial class ContentGridPage : Page
    {
        static double WindowWidth;

        public ContentGridViewModel ViewModel { get; } = new ContentGridViewModel();

        public ContentGridPage()
        {
            InitializeComponent();
            
            Window.Current.SizeChanged += OnWindowSizeChanged;

            Loaded += ContentGridPage_Loaded;

            ViewModel.PeopleResuts.CollectionChanged += PeopleResuts_CollectionChanged;
            ViewModel.MovieResults.CollectionChanged += MovieResults_CollectionChanged;
            ViewModel.ShowResults.CollectionChanged += ShowResults_CollectionChanged;
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

            FadeOutMovieResults.Completed += FadeOutMovieResults_Completed;
            FadeOutShowResults.Completed += FadeOutShowResults_Completed;
            FadeOutPeopleResults.Completed += FadeOutPeopleResults_Completed;
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

        private void ShowResults_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.ShowResults.Count == 0) { FadeOutShowResults.Begin(); }
            else { FadeOutShowResults.Stop(); ShowResults.Visibility = Visibility.Visible; FadeInShowResults.Begin(); }
        }

        private void MovieResults_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.MovieResults.Count == 0) { FadeOutMovieResults.Begin(); }
            else { FadeOutMovieResults.Stop(); MovieResults.Visibility = Visibility.Visible; FadeInMovieResults.Begin(); }
        }

        private void PeopleResuts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ViewModel.PeopleResuts.Count == 0) { FadeOutPeopleResults.Begin(); }
            else { FadeOutPeopleResults.Stop(); PeopleResults.Visibility = Visibility.Visible; FadeInPeopleResults.Begin(); }
        }

        private void FadeOutPeopleResults_Completed(object sender, object e)
        {
            PeopleResults.Visibility = Visibility.Collapsed;
        }

        private void FadeOutShowResults_Completed(object sender, object e)
        {
            ShowResults.Visibility = Visibility.Collapsed;
        }

        private void FadeOutMovieResults_Completed(object sender, object e)
        {
            MovieResults.Visibility = Visibility.Collapsed;
        }

        private void ContentGridPage_Loaded(object sender, RoutedEventArgs e)
        {
            WindowWidth = Window.Current.Bounds.Width;

            GetScrollViewer(PopularMovies).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(PopularShows).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(PopularPeople).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(NowPlayingMovies).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(NowStreamingMovies).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(MovieResults).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(ShowResults).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(PeopleResults).HorizontalScrollMode = ScrollMode.Disabled;
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
            GetScrollViewer(MovieResults).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(ShowResults).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(PeopleResults).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(UpcomingMovies).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(HighRatedMovies).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(OnTvShows).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(AiringTodayShows).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(HighRatedShows).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;

            FadeOutPeopleResults.Begin();
            FadeOutMovieResults.Begin();
            FadeOutShowResults.Begin();
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

        private void OnWindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            WindowWidth = e.Size.Width;
        }

        public static ScrollViewer GetScrollViewer(DependencyObject element)
        {
            if (element is ScrollViewer)
            {
                return (ScrollViewer)element;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);

                var result = GetScrollViewer(child);
                if (result == null)
                {
                    continue;
                }
                else
                {
                    return result;
                }
            }
            return null;
        }

        public static void ScrollGrid(DependencyObject obj, bool forward)
        {
            ScrollViewer scrollViewer = GetScrollViewer(obj);
            scrollViewer.HorizontalScrollMode = ScrollMode.Enabled;
            if (forward) scrollViewer.ChangeView(scrollViewer.HorizontalOffset + Math.Floor((WindowWidth - 52) / 168) * 172, scrollViewer.VerticalOffset, null, false);
            else scrollViewer.ChangeView(scrollViewer.HorizontalOffset - Math.Floor((WindowWidth - 52) / 168) * 172, scrollViewer.VerticalOffset, null, false);
            scrollViewer.HorizontalScrollMode = ScrollMode.Disabled;
        }

        private void ScrollForward(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            ScrollGrid((DependencyObject)this.FindName(btn.Tag.ToString()), true);
        }

        private void ScrollBackward(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            ScrollGrid((DependencyObject)this.FindName(btn.Tag.ToString()), false);
        }

        private void Search_Find(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ViewModel.LoadSearchResults(args.QueryText);   
        }

        private void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if(sender.Text.Equals(""))
            {
                FadeInPopularMovies.Begin();
                FadeInPopularShows.Begin();
                FadeInNowPlayingMovies.Begin();
                FadeInNowStreamingMovies.Begin();
                FadeInPopularPeople.Begin();
                FadeInUpcomingMovies.Begin();
                FadeInHighRatedMovies.Begin();
                FadeInOnTvShows.Begin();
                FadeInAiringTodayShows.Begin();
                FadeInHighRatedShows.Begin();

                FadeOutPeopleResults.Begin();
                FadeOutMovieResults.Begin();
                FadeOutShowResults.Begin();
            }
            else
            {
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

                ViewModel.LoadSearchResults(sender.Text);
            }
        }
    }
}
