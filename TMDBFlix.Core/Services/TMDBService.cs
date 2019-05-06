using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public static Show GetShow(int Id)
        {
            var request = new RestRequest($"/tv/{Id}");
            request.AddParameter("api_key", key);
            request.AddParameter("language", language);
            request.AddParameter("append_to_response", "alternative_titles,content_ratings,credits,external_ids,keywords");

            var response = client.Execute<Show>(request);
            if (response.IsSuccessful) return response.Data;
            else if ((int)response.StatusCode == 429)
            {
                while ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(1000);
                    response = client.Execute<Show>(request);
                }
                return response.Data;
            }
            else return new Show();
        }

        public static Season GetSeason(int Id, int Season)
        {
            var request = new RestRequest($"/tv/{Id}/season/{Season}");
            request.AddParameter("api_key", key);
            request.AddParameter("language", language);
            request.AddParameter("append_to_response", "credits,external_ids");

            var response = client.Execute<Season>(request);
            if (response.IsSuccessful)
            {
                response.Data.showid = Id;
                return response.Data;
            }
            else if ((int)response.StatusCode == 429)
            {
                while ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(1000);
                    response = client.Execute<Season>(request);
                }
                response.Data.showid = Id;
                return response.Data;
            }
            else return new Season();
        }

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
                    Thread.Sleep(1000);
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
                    Thread.Sleep(1000);
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
                    Thread.Sleep(1000);
                    response = client.Execute<Movie>(request);
                }
                return response.Data;
            }
            else return new Movie();
        }

        public static Collection GetCollection(int Id)
        {
            var request = new RestRequest($"/collection/{Id}");
            request.AddParameter("api_key", key);
            request.AddParameter("language", language);

            var response = client.Execute<Collection>(request);
            if (response.IsSuccessful) return response.Data;
            else if ((int)response.StatusCode == 429)
            {
                while ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(1000);
                    response = client.Execute<Collection>(request);
                }
                return response.Data;
            }
            else return new Collection();
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
                    Thread.Sleep(1000);
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
                    Thread.Sleep(1000);
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
                        Thread.Sleep(1000);
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
                    Thread.Sleep(1000);
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
                    Thread.Sleep(1000);
                    page--;
                }
            }

            return results;
        }

        public static List<MultiSearchItem> Search(string Query, int StartingPage = 1, int NumberOfPages = 1)
        {
            var results = new List<MultiSearchItem>();

            var request = new RestRequest("/search/movie");
            request.AddParameter("api_key", key);
            request.AddParameter("language", language);
            request.AddParameter("query", Query);

            for (int page = StartingPage; page < StartingPage + NumberOfPages; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<MultiSearchResponse>(request);
                if (response.IsSuccessful)
                {
                    response.Data.results.ForEach(x => x.media_type = "movie");
                    results.AddRange(response.Data.results);
                }
                else if ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(1000);
                    page--;
                }
            }

            request = new RestRequest("/search/tv");
            request.AddParameter("api_key", key);
            request.AddParameter("language", language);
            request.AddParameter("query", Query);

            for (int page = StartingPage; page < StartingPage + NumberOfPages; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<MultiSearchResponse>(request);
                if (response.IsSuccessful)
                {
                    response.Data.results.ForEach(x => x.media_type = "tv");
                    results.AddRange(response.Data.results);
                }
                else if ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(1000);
                    page--;
                }
            }

            request = new RestRequest("/search/person");
            request.AddParameter("api_key", key);
            request.AddParameter("language", language);
            request.AddParameter("query", Query);

            for (int page = StartingPage; page < StartingPage + NumberOfPages; page++)
            {
                request.AddParameter("page", page);
                var response = client.Execute<MultiSearchResponse>(request);
                if (response.IsSuccessful)
                {
                    response.Data.results.ForEach(x => x.media_type = "person");
                    results.AddRange(response.Data.results);
                }
                else if ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(1000);
                    page--;
                }
            }

            return results.OrderByDescending(x => x.popularity).ToList();
        }

        public static List<MultiSearchItem> ImagesFirst(this List<MultiSearchItem> list)
        {
            var noimage = new List<MultiSearchItem>();
            foreach (var v in list)
            {
                if (v.poster_path is null && v.profile_path is null) noimage.Add(v);
            }
            foreach (var v in noimage)
            {
                list.Remove(v);
                list.Add(v);
            }
            
            return list;
        }

        public static List<Movie> ImagesFirst(this List<Movie> list)
        {
            var noimage = new List<Movie>();
            foreach (var v in list)
            {
                if (v.poster_path is null) noimage.Add(v);
            }
            foreach (var v in noimage)
            {
                list.Remove(v);
                list.Add(v);
            }

            return list;
        }

        public static List<Show> ImagesFirst(this List<Show> list)
        {
            var noimage = new List<Show>();
            foreach (var v in list)
            {
                if (v.poster_path is null) noimage.Add(v);
            }
            foreach (var v in noimage)
            {
                list.Remove(v);
                list.Add(v);
            }

            return list;
        }

        public static List<MovieCredit> ImagesFirst(this List<MovieCredit> list)
        {
            var noimage = new List<MovieCredit>();
            foreach (var v in list)
            {
                if (v.poster_path is null) noimage.Add(v);
            }
            foreach (var v in noimage)
            {
                list.Remove(v);
                list.Add(v);
            }

            return list;
        }

        public static List<ShowCredit> ImagesFirst(this List<ShowCredit> list)
        {
            var noimage = new List<ShowCredit>();
            foreach (var v in list)
            {
                if (v.poster_path is null) noimage.Add(v);
            }
            foreach (var v in noimage)
            {
                list.Remove(v);
                list.Add(v);
            }

            return list;
        }

        public static List<Person> ImagesFirst(this List<Person> list)
        {
            var noimage = new List<Person>();
            foreach (var v in list)
            {
                if (v.profile_path is null) noimage.Add(v);
            }
            foreach (var v in noimage)
            {
                list.Remove(v);
                list.Add(v);
            }

            return list;
        }

        public static List<MultiSearchItem> OnlyWithImages(this List<MultiSearchItem> list)
        {
            var noimage = new List<MultiSearchItem>();
            foreach (var v in list)
            {
                if (v.poster_path is null && v.profile_path is null) noimage.Add(v);
            }
            foreach (var v in noimage)
            {
                list.Remove(v);
            }

            return list;
        }

        public static List<MultiSearchItem> OnlyWithoutImages(this List<MultiSearchItem> list)
        {
            var hasimage = new List<MultiSearchItem>();
            foreach (var v in list)
            {
                if (!(v.poster_path is null && v.profile_path is null)) hasimage.Add(v);
            }
            foreach (var v in hasimage)
            {
                list.Remove(v);
            }

            return list;
        }
    }
}
