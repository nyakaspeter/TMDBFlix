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
    public class HighRatedMoviesGridViewModel : Observable
    {
        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<SampleOrder>(OnItemClick));

        public ObservableCollection<Movie> HighRatedMovies { get; set; }

        private int loadedPages = 0;

        public static bool loading = false;

        public async Task LoadData()
        {
            if (!loading)
            {
                loading = true;
                var HighRatedmovies = await Task.Run(() => TMDBService.GetHighRatedMovies(loadedPages + 1));
                foreach (var v in HighRatedmovies)
                {
                    HighRatedMovies.Add(v);
                }
                loadedPages += 2;
                loading = false;
            }
        }

        public HighRatedMoviesGridViewModel()
        {
            HighRatedMovies = new ObservableCollection<Movie>();
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
