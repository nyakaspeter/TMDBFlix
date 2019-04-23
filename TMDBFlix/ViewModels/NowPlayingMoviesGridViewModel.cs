using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDBFlix.Core.Models;
using TMDBFlix.Core.Services;
using TMDBFlix.Helpers;
using TMDBFlix.Services;
using TMDBFlix.Views;

namespace TMDBFlix.ViewModels
{
    public class NowPlayingMoviesGridViewModel : Observable
    {
        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<SampleOrder>(OnItemClick));

        public ObservableCollection<Movie> NowPlayingMovies { get; set; }

        private int loadedPages = 0;

        public async Task LoadData()
        {
            var nowplayingmovies = await Task.Run(() => TMDBService.GetNowPlayingMovies(loadedPages+1));
            foreach (var v in nowplayingmovies)
            {
                NowPlayingMovies.Add(v);
            }
            loadedPages+=2;
        }

        public NowPlayingMoviesGridViewModel()
        {
            NowPlayingMovies = new ObservableCollection<Movie>();
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
