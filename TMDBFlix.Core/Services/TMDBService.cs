using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TMDBFlix.Core.Models;

namespace TMDBFlix.Core.Services
{
    class TMDBService
    {
        private static string key = "c82568d86ba0dafa5ecef39bee96f011";
        private static RestClient client = new RestClient("https://api.themoviedb.org/");

        public static ObservableCollection<Movie> GetPopularMovies()
        {
            var request = new RestRequest("movie/popular");
            request.AddParameter("api_key", key);

            var result = client.Execute<List<Movie>>(request).Data;

            return new ObservableCollection<Movie>(result);
        }
    }
}
