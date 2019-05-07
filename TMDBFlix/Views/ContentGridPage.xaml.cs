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
using Windows.ApplicationModel.Resources;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace TMDBFlix.Views
{
    public sealed partial class ContentGridPage : CustomPage
    {
        public ContentGridViewModel ViewModel { get; } = new ContentGridViewModel();

        public ContentGridPage()
        {
            InitializeComponent();
            
            Loaded += ContentGridPage_Loaded;
            ViewModel.LoadCompleted += ViewModel_LoadCompleted;

            FadeOutContent.Begin();
        }

        private void ViewModel_LoadCompleted()
        {
            LoadRing.IsActive = false;

            FadeInContent.Begin();

            Scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private async void ContentGridPage_Loaded(object sender, RoutedEventArgs e)
        {
            FixOneRowGrid(NowPlayingMoviesGrid);
            FixOneRowGrid(NowStreamingMoviesGrid);
            FixOneRowGrid(UpcomingMoviesGrid);
            FixOneRowGrid(PopularMoviesGrid);
            FixOneRowGrid(HighRatedMoviesGrid);
            FixOneRowGrid(OnTvShowsGrid);
            FixOneRowGrid(AiringTodayShowsGrid);
            FixOneRowGrid(PopularShowsGrid);
            FixOneRowGrid(HighRatedShowsGrid);
            FixOneRowGrid(PopularPeopleGrid);
        }

        private void NowPlayingMoviesBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MoviesGridPage), new Dictionary<string,string>(){
                {"listname", new ResourceLoader().GetString("NowPlayingMovies/Text") },
                { "path", "/discover/movie"},
                {"region","us"},
                {"with_release_type", "3"},
                {"primary_release_date.gte", DateTime.Today.AddMonths(-3).ToString("yyyy-MM-dd")},
                {"primary_release_date.lte", DateTime.Today.ToString("yyyy-MM-dd")}
            }, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void NowStreamingMoviesBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MoviesGridPage), new Dictionary<string, string>(){
                {"listname", new ResourceLoader().GetString("NowStreamingMovies/Text") },
                { "path", "/discover/movie"},
                {"region","us"},
                {"with_release_type", "4"},
                {"primary_release_date.gte", DateTime.Today.AddMonths(-8).ToString("yyyy-MM-dd")}
            }, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void UpcomingMoviesBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MoviesGridPage), new Dictionary<string, string>(){
                {"listname", new ResourceLoader().GetString("UpcomingMovies/Text") },
                { "path", "/discover/movie"},
                {"region","us" },
                {"primary_release_date.gte", DateTime.Today.AddDays(7).ToString("yyyy-MM-dd")},
                {"primary_release_date.lte", DateTime.Today.AddMonths(6).ToString("yyyy-MM-dd")}
            }, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void PopularMoviesBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MoviesGridPage), new Dictionary<string, string>(){
                {"listname", new ResourceLoader().GetString("PopularMovies/Text") },
                { "path", "/discover/movie"},
                {"region","us"}
            }, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void HighRatedMoviesBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MoviesGridPage), new Dictionary<string, string>(){
                {"listname", new ResourceLoader().GetString("HighRatedMovies/Text") },
                { "path", "/discover/movie"},
                {"region","us"},
                {"sort_by","vote_average.desc" },
                {"primary_release_date.gte", DateTime.Today.AddYears(-20).ToString("yyyy-MM-dd")},
                {"vote_count.gte", "1000"},
                {"vote_average.gte", "7"}
            }, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void OnTvShowsBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShowsGridPage), new Dictionary<string, string>(){
                {"listname", new ResourceLoader().GetString("OnTvShows/Text") },
                { "path", "/discover/tv"},
                {"air_date.gte", DateTime.Today.AddDays(-14).ToString("yyyy-MM-dd") },
                {"air_date.lte", DateTime.Today.AddDays(7).ToString("yyyy-MM-dd") }
            }, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void AiringTodayShowsBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShowsGridPage), new Dictionary<string, string>(){
                {"listname", new ResourceLoader().GetString("AiringTodayShows/Text") },
                { "path", "/discover/tv"},
                {"air_date.gte", DateTime.Today.ToString("yyyy-MM-dd") },
                {"air_date.lte", DateTime.Today.ToString("yyyy-MM-dd") }
            }, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void PopularShowsBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShowsGridPage), new Dictionary<string, string>(){
                {"listname", new ResourceLoader().GetString("PopularShows/Text") },
                { "path", "/discover/tv"}
            }, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void HighRatedShowsBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShowsGridPage), new Dictionary<string, string>(){
                {"listname", new ResourceLoader().GetString("HighRatedShows/Text") },
                { "path", "/discover/tv"},
                {"sort_by", "vote_average.desc" },
                {"vote_average.gte","7" },
                {"vote_count.gte","400" }
            }, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void PopularPeopleBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PeopleGridPage), new Dictionary<string, string>(){
                {"listname", new ResourceLoader().GetString("PopularPeople/Text") },
                { "path", "person/popular"}
            }, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        
    }
}
