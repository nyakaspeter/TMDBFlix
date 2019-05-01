using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMDBFlix.Core.Models;

namespace TMDBFlix.Core.Services
{
    public static class TMDBService
    {
        private static readonly string key = "c82568d86ba0dafa5ecef39bee96f011";
        private static RestClient client = new RestClient("https://api.themoviedb.org/3");
        public static string language = "en";
        public static string region = "US";

        public static Person GetPerson(int Id)
        {
            var request = new RestRequest($"/person/{Id}");
            request.AddParameter("api_key", key);
            request.AddParameter("language", language);
            request.AddParameter("append_to_response", "movie_credits,tv_credits,external_ids,images");

            var response = client.Execute<Person>(request);
            if (response.IsSuccessful) return response.Data;
            else if ((int)response.StatusCode == 429)
            {
                while ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(3000);
                    response = client.Execute<Person>(request);
                }
                return response.Data;
            }
            else return new Person();
        }

        public static List<Movie> GetMovieList(string Path, Dictionary<string, string> Query, int StartingPage = 1, int NumberOfPages = 2)
        {
            var request = new RestRequest(Path);
            request.AddParameter("api_key", key);
            request.AddParameter("language", language);
            foreach (var param in Query)
            {
                request.AddParameter(param.Key, param.Value);
            }

            var results = new List<Movie>();
            for (int page = StartingPage; page < StartingPage + NumberOfPages; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<MoviesResponse>(request);
                if (response.IsSuccessful) results.AddRange(response.Data.results);
                else if ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(3000);
                    page--;
                }
            }

            return results;
        }

        public static Movie GetMovie(int Id)
        {
            var request = new RestRequest($"/movie/{Id}");
            request.AddParameter("api_key", key);
            request.AddParameter("language", language);
            request.AddParameter("append_to_response", "credits,external_ids,alternative_titles,keywords,release_dates");

            var response = client.Execute<Movie>(request);
            if (response.IsSuccessful) return response.Data;
            else if ((int)response.StatusCode == 429)
            {
                while ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(3000);
                    response = client.Execute<Movie>(request);
                }
                return response.Data;
            }
            else return new Movie();
        }

        public static ImagesResponse GetImages(string Path)
        {
            var request = new RestRequest(Path);
            request.AddParameter("api_key", key);

            var response = client.Execute<ImagesResponse>(request);
            if (response.IsSuccessful) return response.Data;
            else if ((int)response.StatusCode == 429)
            {
                while ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(3000);
                    response = client.Execute<ImagesResponse>(request);
                }
                return response.Data;
            }
            else return new ImagesResponse();
        }

        public static List<Video> GetVideos(string Path)
        {
            var videos = new List<Video>();

            var request = new RestRequest($"{Path}/videos");
            request.AddParameter("api_key", key);

            var response = client.Execute<VideosResponse>(request);
            if (response.IsSuccessful)
            {
                videos = response.Data.results;
            }
            else if ((int)response.StatusCode == 429)
            {
                while ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(3000);
                    response = client.Execute<VideosResponse>(request);
                }
                videos = response.Data.results;
            }

            if (!language.Equals("en"))
            {
                request.AddParameter("language", language);
                response = client.Execute<VideosResponse>(request);
                if (response.IsSuccessful)
                {
                    videos.InsertRange(0,response.Data.results);
                }
                else if ((int)response.StatusCode == 429)
                {
                    while ((int)response.StatusCode == 429)
                    {
                        Thread.Sleep(3000);
                        response = client.Execute<VideosResponse>(request);
                    }
                    videos.InsertRange(0,response.Data.results);
                }
            }
            return videos;
        }

        public static List<Show> GetShowList(string Path, Dictionary<string, string> Query, int StartingPage = 1, int NumberOfPages = 2)
        {
            var request = new RestRequest(Path);
            request.AddParameter("api_key", key);
            request.AddParameter("language", language);
            foreach (var param in Query)
            {
                request.AddParameter(param.Key, param.Value);
            }

            var results = new List<Show>();
            for (int page = StartingPage; page < StartingPage + NumberOfPages; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<ShowsResponse>(request);
                if (response.IsSuccessful) results.AddRange(response.Data.results);
                else if ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(3000);
                    page--;
                }
            }

            return results;
        }

        public static List<Person> GetPersonList(string Path, Dictionary<string, string> Query, int StartingPage = 1, int NumberOfPages = 2)
        {
            var request = new RestRequest(Path);
            request.AddParameter("api_key", key);
            request.AddParameter("language", language);
            foreach (var param in Query)
            {
                request.AddParameter(param.Key, param.Value);
            }

            var results = new List<Person>();
            for (int page = StartingPage; page < StartingPage + NumberOfPages; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<PeopleResponse>(request);
                if (response.IsSuccessful) results.AddRange(response.Data.results);
                else if ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(3000);
                    page--;
                }
            }

            return results;
        }

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

                if(response.results != null)
                foreach (var v in response.results)
                {
                    results.Add(v);
                }
            }
            return results;
        }

        public static List<MultiSearchItem> ImagesFirst(this List<MultiSearchItem> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].profile_path == null && list[i].poster_path == null)
                {
                    var v = list[i];
                    list.RemoveAt(i);
                    list.Add(v);
                }
            }
            return list;
        }
    }
}
