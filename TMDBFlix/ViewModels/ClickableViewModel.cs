using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDBFlix.Core.Models;
using TMDBFlix.Helpers;
using TMDBFlix.Services;
using TMDBFlix.Views;
using Windows.ApplicationModel.Resources;

namespace TMDBFlix.ViewModels
{
    public class ClickableViewModel : Observable
    {
        private ICommand _movieClickCommand;
        public ICommand MovieClickCommand => _movieClickCommand ?? (_movieClickCommand = new RelayCommand<Movie>(OnMovieClick));
        private ICommand _showClickCommand;
        public ICommand ShowClickCommand => _showClickCommand ?? (_showClickCommand = new RelayCommand<Show>(OnShowClick));
        private ICommand _personClickCommand;
        public ICommand PersonClickCommand => _personClickCommand ?? (_personClickCommand = new RelayCommand<Person>(OnPersonClick));
        private ICommand _videoClickCommand;
        public ICommand VideoClickCommand => _videoClickCommand ?? (_videoClickCommand = new RelayCommand<Video>(OnVideoClick));
        private ICommand _imageClickCommand;
        public ICommand ImageClickCommand => _imageClickCommand ?? (_imageClickCommand = new RelayCommand<Image>(OnImageClick));
        private ICommand _genreClickCommand;
        public ICommand GenreClickCommand => _genreClickCommand ?? (_genreClickCommand = new RelayCommand<Genre>(OnGenreClick));
        private ICommand _tvgenreClickCommand;
        public ICommand TvGenreClickCommand => _tvgenreClickCommand ?? (_tvgenreClickCommand = new RelayCommand<Genre>(OnTvGenreClick));
        private ICommand _keywordClickCommand;
        public ICommand KeywordClickCommand => _keywordClickCommand ?? (_keywordClickCommand = new RelayCommand<Keyword>(OnKeywordClick));
        private ICommand _tvkeywordClickCommand;
        public ICommand TvKeywordClickCommand => _tvkeywordClickCommand ?? (_tvkeywordClickCommand = new RelayCommand<Keyword>(OnTvKeywordClick));
        private ICommand _networkClickCommand;
        public ICommand NetworkClickCommand => _networkClickCommand ?? (_networkClickCommand = new RelayCommand<Network>(OnNetworkClick));
        private ICommand _searchItemClickCommand;
        public ICommand SearchItemClickCommand => _searchItemClickCommand ?? (_searchItemClickCommand = new RelayCommand<MultiSearchItem>(OnSearchItemClick));

        public void OnMovieClick(Movie clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<MovieDetailPage>(clickedItem.id);
            }
        }

        public void OnShowClick(Show clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ShowDetailPage>(clickedItem.id);
            }
        }

        public void OnPersonClick(Person clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<PersonDetailPage>(clickedItem.id);
            }
        }

        public void OnSearchItemClick(MultiSearchItem clickedItem)
        {
            if (clickedItem.media_type.Equals("movie"))
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<MovieDetailPage>(clickedItem.id);
            }
            if (clickedItem.media_type.Equals("tv"))
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ShowDetailPage>(clickedItem.id);
            }
            if (clickedItem.media_type.Equals("person"))
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<PersonDetailPage>(clickedItem.id);
            }
        }

        public void OnVideoClick(Video clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<WebPage>(clickedItem);
            }
        }

        public void OnImageClick(Image clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ImagePage>(clickedItem);
            }
        }

        public void OnGenreClick(Genre clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<MoviesGridPage>(new Dictionary<string, string>(){
                {"listname", clickedItem.name + " " + new ResourceLoader().GetString("Shell_Movies/Content") },
                { "path", "/discover/movie"},
                {"with_genres",clickedItem.id.ToString() }
            });
            }
        }

        public void OnTvGenreClick(Genre clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ShowsGridPage>(new Dictionary<string, string>(){
                {"listname", clickedItem.name + " " + new ResourceLoader().GetString("Shell_Shows/Content") },
                { "path", "/discover/tv"},
                {"with_genres",clickedItem.id.ToString() }
            });
            }
        }

        public void OnKeywordClick(Keyword clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<MoviesGridPage>(new Dictionary<string, string>(){
                {"listname", clickedItem.name},
                { "path", "/discover/movie"},
                {"with_keywords",clickedItem.id.ToString() }
            });
            }
        }

        public void OnTvKeywordClick(Keyword clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ShowsGridPage>(new Dictionary<string, string>(){
                {"listname", clickedItem.name},
                { "path", "/discover/tv"},
                {"with_keywords",clickedItem.id.ToString() }
            });
            }
        }

        public void OnNetworkClick(Network clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ShowsGridPage>(new Dictionary<string, string>(){
                {"listname", clickedItem.name + " " + new ResourceLoader().GetString("Shell_Shows/Content") },
                { "path", "/discover/tv"},
                {"with_networks",clickedItem.id.ToString() }
            });
            }
        }
    }
}
