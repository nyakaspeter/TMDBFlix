using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TMDBFlix.Core.Models;

namespace TMDBFlix.Core.Services
{
    public class TMDBService
    {
        private static string key = "c82568d86ba0dafa5ecef39bee96f011";
        private static RestClient client = new RestClient("https://api.themoviedb.org/3");

        public static ObservableCollection<Movie> GetPopularMovies()
        {
            var request = new RestRequest("/movie/popular");
            request.AddParameter("api_key", key);
            
            var result = client.Execute<MoviesResponse>(request).Data;

            return new ObservableCollection<Movie>(result.results);
        }

        public static ObservableCollection<Movie> GetNowPlayingMovies()
        {
            var request = new RestRequest("/movie/now_playing");
            request.AddParameter("api_key", key);

            var result = client.Execute<MoviesResponse>(request).Data;

            return new ObservableCollection<Movie>(result.results);
        }

        public static ObservableCollection<Show> GetPopularShows()
        {
            var request = new RestRequest("/tv/popular");
            request.AddParameter("api_key", key);

            var result = client.Execute<ShowsResponse>(request).Data;

            return new ObservableCollection<Show>(result.results);
        }

        public static ObservableCollection<Person> GetPopularPeople()
        {
            var request = new RestRequest("/person/popular");
            request.AddParameter("api_key", key);

            var result = client.Execute<PeopleResponse>(request).Data;

            return new ObservableCollection<Person>(result.results);
        }
    }
}
