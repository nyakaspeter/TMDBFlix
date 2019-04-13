using System;
using System.Collections.ObjectModel;
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

        public ObservableCollection<Movie> PopularMovies
        {
            get
            {
                return TMDBService.GetPopularMovies();
            }
        }

        public ObservableCollection<Movie> NowPlayingMovies
        {
            get
            {
                return TMDBService.GetNowPlayingMovies();
            }
        }

        public ObservableCollection<Show> PopularShows
        {
            get
            {
                return TMDBService.GetPopularShows();
            }
        }

        public ObservableCollection<Person> PopularPeople
        {
            get
            {
                return TMDBService.GetPopularPeople();
            }
        }

        public ContentGridViewModel()
        {
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
