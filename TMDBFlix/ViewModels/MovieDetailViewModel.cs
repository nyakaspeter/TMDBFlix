using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TMDBFlix.Core.Models;
using TMDBFlix.Core.Services;
using TMDBFlix.Helpers;

namespace TMDBFlix.ViewModels
{
    public class MovieDetailViewModel : ClickableViewModel
    {
        private Movie movie = new Movie();

        public Movie Movie
        {
            get { return movie; }
            set { Set(ref movie, value); }
        }

        public int Id;
        public ObservableCollection<Video> Videos = new ObservableCollection<Video>();
        public ObservableCollection<Image> Backdrops = new ObservableCollection<Image>();
        public ObservableCollection<Image> Posters = new ObservableCollection<Image>();
        public ObservableCollection<Movie> Recommendations = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> Similar = new ObservableCollection<Movie>();
        public ObservableCollection<Person> Cast = new ObservableCollection<Person>();
        public ObservableCollection<Person> Crew = new ObservableCollection<Person>();
        public ObservableCollection<Person> Directors = new ObservableCollection<Person>();

        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        async Task LoadInfo()
        {
            Movie = await Task.Run(() => TMDBService.GetMovie(Id));
            foreach (var v in Movie.credits.cast)
            {
                Cast.Add(v);
            }
            foreach (var v in Movie.credits.crew)
            {
                Crew.Add(v);
                if (v.job.Equals("Director")) Directors.Add(v);
            }
        }

        async Task LoadImages()
        {
            var images = await Task.Run(() => TMDBService.GetImages($"/movie/{Id}/images"));
            foreach (var v in images.backdrops)
            {
                Backdrops.Add(v);
            }
            foreach (var v in images.posters)
            {
                Posters.Add(v);
            }
        }

        async Task LoadVideos()
        {
            var videos = await Task.Run(() => TMDBService.GetVideos($"/movie/{Id}"));
            foreach (var v in videos)
            {
                if(v.site.Equals("YouTube"))
                    Videos.Add(v);
            }
        }

        async Task LoadRecommendations()
        {
            var recommendations = await Task.Run(() => TMDBService.GetMovieList($"/movie/{Id}/recommendations", new Dictionary<string, string>()));
            foreach (var v in recommendations)
            {
                Recommendations.Add(v);
            }
        }

        async Task LoadSimilar()
        {
            var similar = await Task.Run(() => TMDBService.GetMovieList($"/movie/{Id}/similar", new Dictionary<string, string>()));
            foreach (var v in similar)
            {
                Similar.Add(v);
            }
        }

        public async void LoadData()
        {
            var tasks = new List<Task>()
            {
                LoadInfo(),
                LoadRecommendations(),
                LoadSimilar(),
                LoadImages(),
                LoadVideos()
            };

            await Task.WhenAll(tasks);
            LoadCompleted();
        }

        public MovieDetailViewModel()
        {
        }

    }
}
