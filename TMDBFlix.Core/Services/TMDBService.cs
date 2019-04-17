using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TMDBFlix.Core.Models;

namespace TMDBFlix.Core.Services
{
    public class TMDBService
    {
        private static string key = "c82568d86ba0dafa5ecef39bee96f011";
        private static RestClient client = new RestClient("https://api.themoviedb.org/3");

        public static List<MultiSearchItem> Search(string query, int page = 1)
        {
            var request = new RestRequest("/search/multi");
            request.AddParameter("api_key", key);
            request.AddParameter("query", query);

            var result = client.Execute<MultiSearchResponse>(request).Data;

            return result.results;
        }

        public static List<Movie> GetPopularMovies(int page = 1)
        {
            var request = new RestRequest("/movie/popular");
            request.AddParameter("api_key", key);
            request.AddParameter("page", page);
            
            var result = client.Execute<MoviesResponse>(request).Data;

            return result.results;
        }

        public static List<Movie> GetNowPlayingMovies(int page = 1)
        {
            var request = new RestRequest("/movie/now_playing");
            request.AddParameter("api_key", key);
            request.AddParameter("page", page);

            var result = client.Execute<MoviesResponse>(request).Data;

            return result.results;
        }

        public static List<Movie> GetNowStreamingMovies(int page = 1)
        {
            var halfyearago = DateTime.Today.AddMonths(-6);

            var request = new RestRequest("/discover/movie");
            request.AddParameter("api_key", key);
            request.AddParameter("with_release_type", 4);
            request.AddParameter("primary_release_date.gte", halfyearago.ToString("yyyy-MM-dd"));
            request.AddParameter("page", page);

            var result = client.Execute<MoviesResponse>(request).Data;

            return result.results;
        }

        public static List<Show> GetPopularShows(int page = 1)
        {
            var request = new RestRequest("/tv/popular");
            request.AddParameter("api_key", key);
            request.AddParameter("page", page);

            var result = client.Execute<ShowsResponse>(request).Data;

            return result.results;
        }

        public static List<Person> GetPopularPeople(int page = 1)
        {
            var request = new RestRequest("/person/popular");
            request.AddParameter("api_key", key);
            request.AddParameter("page", page);

            var result = client.Execute<PeopleResponse>(request).Data;

            return result.results;
        }

    }
}
