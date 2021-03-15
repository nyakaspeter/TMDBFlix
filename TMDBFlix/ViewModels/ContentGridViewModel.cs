using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using Microsoft.Toolkit.Uwp.UI.Animations;

using TMDBFlix.Models;
using TMDBFlix.Services;
using TMDBFlix.Helpers;
using TMDBFlix.Views;

namespace TMDBFlix.ViewModels
{
    /// <summary>
    /// Contains main page data and loading tasks
    /// </summary>
    public class ContentGridViewModel : ClickableViewModel
    {
        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        public ObservableCollection<Movie> NowPlayingMovies { get; set; } = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> NowStreamingMovies { get; set; } = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> UpcomingMovies { get; set; } = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> PopularMovies { get; set; } = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> HighRatedMovies { get; set; } = new ObservableCollection<Movie>();
        public ObservableCollection<Show> OnTvShows { get; set; } = new ObservableCollection<Show>();
        public ObservableCollection<Show> AiringTodayShows { get; set; } = new ObservableCollection<Show>();
        public ObservableCollection<Show> PopularShows { get; set; } = new ObservableCollection<Show>();
        public ObservableCollection<Show> HighRatedShows { get; set; } = new ObservableCollection<Show>();
        public ObservableCollection<Person> PopularPeople { get; set; } = new ObservableCollection<Person>();

        async Task LoadMovieList(ObservableCollection<Movie> List, string Path, Dictionary<string,string> Query)
        {
            var results = await Task.Run(() => TMDBService.GetMovieList(Path, Query, 1, 1));
            foreach (var v in results)
            {
                List.Add(v);
            }
        }

        async Task LoadShowList(ObservableCollection<Show> List, string Path, Dictionary<string, string> Query)
        {
            var results = await Task.Run(() => TMDBService.GetShowList(Path, Query, 1, 1));
            foreach (var v in results)
            {
                List.Add(v);
            }
        }

        async Task LoadPersonList(ObservableCollection<Person> List, string Path, Dictionary<string, string> Query)
        {
            var results = await Task.Run(() => TMDBService.GetPersonList(Path, Query, 1, 1));
            foreach (var v in results)
            {
                List.Add(v);
            }
        }

        public async void LoadData()
        {
            var tasks = new List<Task>()
            {
                LoadMovieList(NowPlayingMovies, "/discover/movie", new Dictionary<string, string>()
                {
                    {"region","US"},
                    {"with_release_type", "3"},
                    {"primary_release_date.gte", DateTime.Today.AddMonths(-3).ToString("yyyy-MM-dd")},
                    {"primary_release_date.lte", DateTime.Today.ToString("yyyy-MM-dd")}
                }),
                LoadMovieList(NowStreamingMovies, "/discover/movie", new Dictionary<string, string>()
                {
                    {"region","US"},
                    {"with_release_type", "4"},
                    {"primary_release_date.gte", DateTime.Today.AddMonths(-8).ToString("yyyy-MM-dd")}
                }),
                LoadMovieList(UpcomingMovies, "/discover/movie", new Dictionary<string, string>()
                {
                    {"region","US" },
                    {"primary_release_date.gte", DateTime.Today.AddDays(7).ToString("yyyy-MM-dd")},
                    {"primary_release_date.lte", DateTime.Today.AddMonths(6).ToString("yyyy-MM-dd")}
                }),
                LoadMovieList(PopularMovies, "/discover/movie", new Dictionary<string, string>()
                {
                    {"region","US"}
                }),
                LoadMovieList(HighRatedMovies, "/discover/movie", new Dictionary<string, string>()
                {
                    {"region","US"},
                    {"sort_by","vote_average.desc" },
                    {"primary_release_date.gte", DateTime.Today.AddYears(-20).ToString("yyyy-MM-dd")},
                    {"vote_count.gte", "1000"},
                    {"vote_average.gte", "7"}
                }),
                LoadShowList(OnTvShows, "/discover/tv", new Dictionary<string, string>()
                {
                    {"air_date.gte", DateTime.Today.AddDays(-14).ToString("yyyy-MM-dd") },
                    {"air_date.lte", DateTime.Today.AddDays(7).ToString("yyyy-MM-dd") }
                }),
                LoadShowList(AiringTodayShows, "/discover/tv", new Dictionary<string, string>()
                {
                    {"air_date.gte", DateTime.Today.ToString("yyyy-MM-dd") },
                    {"air_date.lte", DateTime.Today.ToString("yyyy-MM-dd") }
                }),
                LoadShowList(PopularShows, "/discover/tv", new Dictionary<string, string>()),
                LoadShowList(HighRatedShows, "/discover/tv", new Dictionary<string, string>(){
                    {"sort_by", "vote_average.desc" },
                    {"vote_average.gte","7" },
                    {"vote_count.gte","400" }
                }),
                LoadPersonList(PopularPeople, "/person/popular", new Dictionary<string, string>())
            };

            await Task.WhenAll(tasks);
            LoadCompleted();
        }

        public ContentGridViewModel()
        {
            LoadData();
        }
    }
}
