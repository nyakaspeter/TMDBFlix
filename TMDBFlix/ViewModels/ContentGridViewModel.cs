using System;
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
        
        public ObservableCollection<Movie> PopularMovies { get; set; }
        public ObservableCollection<Show> PopularShows { get; set; }
        public ObservableCollection<Person> PopularPeople { get; set; }
        public ObservableCollection<Movie> NowPlayingMovies { get; set; }
        public ObservableCollection<Movie> NowStreamingMovies { get; set; }


        public async Task LoadData()
        {
            var popularmovies = await Task.Run(() => TMDBService.GetPopularMovies());
            foreach (var v in popularmovies)
            {
                PopularMovies.Add(v);
            }
            
            
            var popularshows = await Task.Run(() => TMDBService.GetPopularShows());
            foreach (var v in popularshows)
            {
                PopularShows.Add(v);
            }
            
            
            var popularpeople = await Task.Run(() => TMDBService.GetPopularPeople());
            foreach (var v in popularpeople)
            {
                PopularPeople.Add(v);
            }

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
        }

        public ContentGridViewModel()
        {
            PopularMovies = new ObservableCollection<Movie>();
            PopularShows = new ObservableCollection<Show>();
            PopularPeople = new ObservableCollection<Person>();
            NowPlayingMovies = new ObservableCollection<Movie>();
            NowStreamingMovies = new ObservableCollection<Movie>();
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
