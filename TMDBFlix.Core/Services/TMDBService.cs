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

        public static List<MultiSearchItem> Search(string Query, int StartPage = 1, int PageCount = 2)
        {
            var request = new RestRequest("/search/multi");
            request.AddParameter("api_key", key);
            request.AddParameter("query", Query);

            var results = new List<MultiSearchItem>();

            for (int page = StartPage; page < StartPage + PageCount; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<MultiSearchResponse>(request).Data;

                foreach (var v in response.results)
                {
                    results.Add(v);
                }
            }
            return results;
        }

        public static List<Movie> GetPopularMovies(int StartPage = 1, int PageCount = 1)
        {
            var request = new RestRequest("/movie/popular");
            request.AddParameter("api_key", key);

            var results = new List<Movie>();

            for (int page = StartPage; page < StartPage + PageCount; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<MoviesResponse>(request).Data;

                foreach (var v in response.results)
                {
                    results.Add(v);
                }
            }

            results.Reverse();
            return results;
        }

        public static List<Movie> GetNowPlayingMovies(int StartPage = 1, int PageCount = 1)
        {
            var request = new RestRequest("/movie/now_playing");
            request.AddParameter("api_key", key);

            var results = new List<Movie>();

            for (int page = StartPage; page < StartPage + PageCount; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<MoviesResponse>(request).Data;

                foreach (var v in response.results)
                {
                    results.Add(v);
                }
            }

            var nowstreaming = GetNowStreamingMovies();
            for (int i=0; i<results.Count; i++)
            {
                for (int j = 0; j < nowstreaming.Count; j++)
                {
                    if(results[i].id == nowstreaming[j].id)
                    {
                        var item = results[i];
                        results.Remove(results[i]);
                        results.Add(item);
                    }
                }

            }
            return results;
        }

        public static List<Movie> GetNowStreamingMovies(int MonthsAgo = 12, int StartPage = 1, int PageCount = 1)
        {
            var monthsago = DateTime.Today.AddMonths(MonthsAgo * -1);

            var request = new RestRequest("/discover/movie");
            request.AddParameter("api_key", key);
            request.AddParameter("with_release_type", 4);
            request.AddParameter("primary_release_date.gte", monthsago.ToString("yyyy-MM-dd"));

            var results = new List<Movie>();

            for (int page = StartPage; page < StartPage + PageCount; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<MoviesResponse>(request).Data;

                foreach (var v in response.results)
                {
                    results.Add(v);
                }
            }

            return results;
        }

        public static List<Show> GetPopularShows(int StartPage = 1, int PageCount = 1)
        {
            var request = new RestRequest("/tv/popular");
            request.AddParameter("api_key", key);

            var results = new List<Show>();

            for (int page = StartPage; page < StartPage + PageCount; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<ShowsResponse>(request).Data;

                foreach (var v in response.results)
                {
                    results.Add(v);
                }
            }

            results.Reverse();
            return results;
        }

        public static List<Person> GetPopularPeople(int StartPage = 1, int PageCount = 1)
        {
            var request = new RestRequest("/person/popular");
            request.AddParameter("api_key", key);
            
            var results = new List<Person>();

            for (int page = StartPage; page < StartPage + PageCount; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<PeopleResponse>(request).Data;

                foreach (var v in response.results)
                {
                    results.Add(v);
                }
            }
            return results;
        }

        public static List<Movie> GetHighRatedMovies(int MonthsAgo = 24, int StartPage = 1, int PageCount = 1)
        {
            var monthsago = DateTime.Today.AddMonths(MonthsAgo * -1);

            var request = new RestRequest("/discover/movie");
            request.AddParameter("api_key", key);
            request.AddParameter("sort_by", "vote_average.desc");
            request.AddParameter("primary_release_date.gte", monthsago.ToString("yyyy-MM-dd"));
            request.AddParameter("vote_count.gte", 300);

            var results = new List<Movie>();

            for (int page = StartPage; page < StartPage + PageCount; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<MoviesResponse>(request).Data;

                foreach (var v in response.results)
                {
                    results.Add(v);
                }
            }
            return results;
        }

        public static List<Movie> GetUpcomingMovies(int InMonths = 3, int StartPage = 1, int PageCount = 1)
        {
            var inmonths = DateTime.Today.AddMonths(InMonths);
            var tomorrow = DateTime.Today.AddDays(1);

            var request = new RestRequest("/discover/movie");
            request.AddParameter("api_key", key);
            request.AddParameter("region", "us");
            request.AddParameter("primary_release_date.gte", tomorrow.ToString("yyyy-MM-dd"));
            request.AddParameter("primary_release_date.lte", inmonths.ToString("yyyy-MM-dd"));

            var results = new List<Movie>();

            for (int page = StartPage; page < StartPage + PageCount; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<MoviesResponse>(request).Data;

                foreach (var v in response.results)
                {
                    results.Add(v);
                }
            }
            return results;
        }

        public static List<Show> GetAiringTodayShows(int StartPage = 1, int PageCount = 1)
        {
            var request = new RestRequest("/tv/airing_today");
            request.AddParameter("api_key", key);

            var results = new List<Show>();

            for (int page = StartPage; page < StartPage + PageCount; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<ShowsResponse>(request).Data;

                foreach (var v in response.results)
                {
                    results.Add(v);
                }
            }
            return results;
        }

        public static List<Show> GetOnTvShows(int StartPage = 1, int PageCount = 1)
        {
            var request = new RestRequest("/tv/on_the_air");
            request.AddParameter("api_key", key);

            var results = new List<Show>();

            for (int page = StartPage; page < StartPage + PageCount; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<ShowsResponse>(request).Data;

                foreach (var v in response.results)
                {
                    results.Add(v);
                }
            }

            var airingtoday = GetAiringTodayShows();
            for (int i = 0; i < results.Count; i++)
            {
                for (int j = 0; j < airingtoday.Count; j++)
                {
                    if (results[i].id == airingtoday[j].id)
                    {
                        var item = results[i];
                        results.Remove(results[i]);
                        results.Add(item);
                    }
                }

            }
            return results;
        }

        public static List<Show> GetHighRatedShows(int MonthsAgo = 24, int StartPage = 1, int PageCount = 1)
        {
            var monthsago = DateTime.Today.AddMonths(MonthsAgo * -1);

            var request = new RestRequest("/discover/tv");
            request.AddParameter("api_key", key);
            request.AddParameter("sort_by", "vote_average.desc");
            request.AddParameter("air_date.gte", monthsago.ToString("yyyy-MM-dd"));
            request.AddParameter("vote_count.gte", 300);

            var results = new List<Show>();

            for (int page=StartPage; page<StartPage+PageCount; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<ShowsResponse>(request).Data;

                foreach (var v in response.results)
                {
                    results.Add(v);
                }
            }
            return results;
        }

    }
}
