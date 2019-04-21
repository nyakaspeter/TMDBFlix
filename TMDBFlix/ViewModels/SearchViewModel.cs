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
    public class SearchViewModel : Observable
    {
        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<SampleOrder>(OnItemClick));

        public static string searchstring;
        public static string SearchString { get { return searchstring; } set { searchstring = value; LoadSearchResults(); } }

        public ObservableCollection<Person> NonStaticPeopleResults { get { return PeopleResults; } }
        public ObservableCollection<Movie> NonStaticMovieResults { get { return MovieResults; } }
        public ObservableCollection<Show> NonStaticShowResults { get { return ShowResults; } }

        static public ObservableCollection<Person> PeopleResults { get; set; }
        static public ObservableCollection<Movie> MovieResults { get; set; }
        static public ObservableCollection<Show> ShowResults { get; set; }

        public static async Task LoadSearchResults()
        {
            var searchresults = await Task.Run(() => TMDBService.Search(SearchString).ImagesFirst());

            PeopleResults.Clear();
            MovieResults.Clear();
            ShowResults.Clear();

            foreach (var v in searchresults)
            {
                if (v.media_type.Equals("movie"))
                {
                    MovieResults.Add(new Movie() { id = v.id, title = v.title, poster_path = v.poster_path, release_date = v.release_date });
                }
                if (v.media_type.Equals("tv"))
                {
                    ShowResults.Add(new Show() { id = v.id, name = v.name, poster_path = v.poster_path, first_air_date = v.first_air_date });
                }
                if (v.media_type.Equals("person"))
                {
                    PeopleResults.Add(new Person() { id = v.id, name = v.name, profile_path = v.profile_path, known_for = v.known_for });
                }
            }
        }

        public SearchViewModel()
        {
            PeopleResults = new ObservableCollection<Person>();
            MovieResults = new ObservableCollection<Movie>();
            ShowResults = new ObservableCollection<Show>();
        }

        private void OnItemClick(SampleOrder clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<SearchDetailPage>(clickedItem.OrderId);
            }
        }
    }
}
