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

        public async Task LoadData()
        {
            var pm = await Task.Run(() => TMDBService.GetPopularMovies());
            foreach (var v in pm)
            {
                PopularMovies.Add(v);
            }
            
            
            var ps = await Task.Run(() => TMDBService.GetPopularShows());
            foreach (var v in ps)
            {
                PopularShows.Add(v);
            }
            
            
            var pp = await Task.Run(() => TMDBService.GetPopularPeople());
            foreach (var v in pp)
            {
                PopularPeople.Add(v);
            }
        }

        public ContentGridViewModel()
        {
            PopularMovies = new ObservableCollection<Movie>();
            PopularShows = new ObservableCollection<Show>();
            PopularPeople = new ObservableCollection<Person>();
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
