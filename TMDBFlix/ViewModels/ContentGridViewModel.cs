using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using Microsoft.Toolkit.Uwp.UI.Animations;

using TMDBFlix.Core.Models;
using TMDBFlix.Core.Services;
using TMDBFlix.Helpers;
using TMDBFlix.Services;
using TMDBFlix.Views;

namespace TMDBFlix.ViewModels
{
    public class ContentGridViewModel : Observable
    {
        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<SampleOrder>(OnItemClick));

        public ObservableCollection<int> NonStaticSection { get { return Section; } }
        public static ObservableCollection<int> Section { set; get; }

        public ObservableCollection<Movie> PopularMovies { get; set; }
        public ObservableCollection<Show> PopularShows { get; set; }
        public ObservableCollection<Person> PopularPeople { get; set; }
        public ObservableCollection<Movie> NowPlayingMovies { get; set; }
        public ObservableCollection<Movie> NowStreamingMovies { get; set; }
        public ObservableCollection<Movie> HighRatedMovies { get; set; }
        public ObservableCollection<Movie> UpcomingMovies { get; set; }
        public ObservableCollection<Show> OnTvShows { get; set; }
        public ObservableCollection<Show> AiringTodayShows { get; set; }
        public ObservableCollection<Show> HighRatedShows { get; set; }

        public async Task LoadData()
        {
            var nowplayingmovies = await Task.Run(() => TMDBService.GetNowPlayingMovies());
            foreach (var v in nowplayingmovies)
            {
                NowPlayingMovies.Add(v);
            }

            var nowstreamingmovies = await Task.Run(() => TMDBService.GetNowStreamingMovies());
            foreach (var v in nowstreamingmovies)
            {
                NowStreamingMovies.Add(v);
            }

            var upcomingmovies = await Task.Run(() => TMDBService.GetUpcomingMovies());
            foreach (var v in upcomingmovies)
            {
                UpcomingMovies.Add(v);
            }

            var popularmovies = await Task.Run(() => TMDBService.GetPopularMovies());
            foreach (var v in popularmovies)
            {
                PopularMovies.Add(v);
            }

            var highratedmovies = await Task.Run(() => TMDBService.GetHighRatedMovies());
            foreach (var v in highratedmovies)
            {
                HighRatedMovies.Add(v);
            }

            var ontvshows = await Task.Run(() => TMDBService.GetOnTvShows());
            foreach (var v in ontvshows)
            {
                OnTvShows.Add(v);
            }

            var airingtodayshows = await Task.Run(() => TMDBService.GetAiringTodayShows());
            foreach (var v in airingtodayshows)
            {
                AiringTodayShows.Add(v);
            }

            var popularshows = await Task.Run(() => TMDBService.GetPopularShows());
            foreach (var v in popularshows)
            {
                PopularShows.Add(v);
            }

            var highratedshows = await Task.Run(() => TMDBService.GetHighRatedShows());
            foreach (var v in highratedshows)
            {
                HighRatedShows.Add(v);
            }

            var popularpeople = await Task.Run(() => TMDBService.GetPopularPeople());
            foreach (var v in popularpeople)
            {
                PopularPeople.Add(v);
            }
        }

        public ContentGridViewModel()
        {
            Section = new ObservableCollection<int>();

            PopularMovies = new ObservableCollection<Movie>();
            PopularShows = new ObservableCollection<Show>();
            PopularPeople = new ObservableCollection<Person>();
            NowPlayingMovies = new ObservableCollection<Movie>();
            NowStreamingMovies = new ObservableCollection<Movie>();
            HighRatedMovies = new ObservableCollection<Movie>();
            HighRatedShows = new ObservableCollection<Show>();
            UpcomingMovies = new ObservableCollection<Movie>();
            OnTvShows = new ObservableCollection<Show>();
            AiringTodayShows = new ObservableCollection<Show>();

            LoadData();
        }

        private void OnItemClick(SampleOrder clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ContentGridDetailPage>(clickedItem.OrderId);
            }
        }
    }
}
