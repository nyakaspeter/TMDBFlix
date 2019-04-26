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
    public class NowStreamingMoviesGridViewModel : Observable
    {
        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<SampleOrder>(OnItemClick));

        public ObservableCollection<Movie> NowStreamingMovies { get; set; }

        private int loadedPages = 0;

        public static bool loading = false;

        public async Task LoadData()
        {
            if (!loading)
            {
                loading = true;
                var nowstreamingmovies = await Task.Run(() => TMDBService.GetNowStreamingMovies(loadedPages + 1));
                foreach (var v in nowstreamingmovies)
                {
                    NowStreamingMovies.Add(v);
                }
                loadedPages += 2;
                loading = false;
            }
        }

        public NowStreamingMoviesGridViewModel()
        {
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
