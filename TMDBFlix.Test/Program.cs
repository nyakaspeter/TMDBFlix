using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDBFlix.Core.Models;
using TMDBFlix.Core.Services;
using Windows.Storage;

namespace TMDBFlix.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string link = ApplicationData.Current.LocalSettings.Values["link"] as string;
            string mode = ApplicationData.Current.LocalSettings.Values["mode"] as string;

            //Console.WriteLine("asdasd");

            var temp = ApplicationData.Current.TemporaryFolder;
            var downloads = temp.CreateFolderAsync("Downloads", CreationCollisionOption.OpenIfExists);

            var p = new ProcessStartInfo
            {
                WorkingDirectory = temp.Path,
                FileName = "cmd",
                RedirectStandardInput = true,
                UseShellExecute = false,
            };
            var cmd = Process.Start(p);

            if(mode.Equals("filelist")) cmd.StandardInput.WriteLine($"peerflix \"{link}\" -l > filelist.txt");
            if(mode.Equals("download")) cmd.StandardInput.WriteLine($"peerflix \"{link}\" -f Downloads");
            /*
            var key = "c82568d86ba0dafa5ecef39bee96f011";
            var client = new RestClient("https://api.themoviedb.org/3");

            var request = new RestRequest("/movie/popular");
            request.AddParameter("api_key", key);

            var result = client.Execute<MoviesResponse>(request).Data;

            foreach (var r in result.results)
            {
                Console.WriteLine("Poster: " + r.poster_path);
                Console.WriteLine("Adult: " + r.adult);
                Console.WriteLine("Overview: " + r.overview);
                Console.WriteLine("Release date: " + r.release_date);
                Console.WriteLine("Genre ids: " + String.Join(", ", r.genre_ids));
                Console.WriteLine("Id: " + r.id);
                Console.WriteLine("Original title: " + r.original_title);
                Console.WriteLine("Original language: " + r.original_language);
                Console.WriteLine("Title: " + r.title);
                Console.WriteLine("Backdrop: " + r.backdrop_path);
                Console.WriteLine("Popularity: " + r.popularity);
                Console.WriteLine("Vote count: " + r.vote_count);
                Console.WriteLine("Video: " + r.video);
                Console.WriteLine("Vote average: " + r.vote_average);
                Console.WriteLine();
            }

            request = new RestRequest("/movie/287947-shazam");
            request.AddParameter("api_key", key);

            var m = client.Execute<Movie>(request).Data;

            Console.WriteLine("Title: " + m.title);
            Console.WriteLine("Tagline: " + m.tagline);
            Console.WriteLine("Overview: " + m.overview);
            Console.WriteLine("Genres: " + String.Join(", ",m.genres));
            Console.WriteLine("Homepage: " + m.homepage);
            Console.WriteLine("Id: " + m.id);
            Console.WriteLine("Imdb id: " + m.imdb_id);
            Console.WriteLine("Budget: " + m.budget);
            Console.WriteLine("Revenue: " + m.revenue);
            Console.WriteLine("Release date: " + m.release_date);
            Console.WriteLine("Runtime: " + m.runtime);
            Console.WriteLine("Status: " + m.status);
            Console.WriteLine("Vote count: " + m.vote_count);
            Console.WriteLine("Vote average: " + m.vote_average);
            Console.WriteLine("Original title: " + m.original_title);
            Console.WriteLine("Original language: " + m.original_language);
            Console.WriteLine("Spoken languages: " + String.Join(",", m.spoken_languages));
            Console.WriteLine("Companies: " + String.Join(", ", m.production_companies));
            Console.WriteLine("Countries: " + String.Join(", ", m.production_countries));
            Console.WriteLine("Popularity: " + m.popularity);
            Console.WriteLine("Adult: " + m.adult);
            Console.WriteLine("Video: " + m.video);
            Console.WriteLine("Collection: " + m.belongs_to_collection);
            Console.WriteLine("Poster: " + m.poster_path);
            Console.WriteLine("Backdrop: " + m.backdrop_path);
            
            request = new RestRequest("/person/popular");
            request.AddParameter("api_key", key);

            var t = client.Execute<PeopleResponse>(request);
            var d = t.Data;

            foreach (var s in d.results)
            {
                Console.WriteLine(s.name);
                Console.WriteLine(s.known_for);
            }
            */
        }
    }

}
