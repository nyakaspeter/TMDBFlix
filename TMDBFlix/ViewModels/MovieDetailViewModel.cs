using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TMDBFlix.Models;
using TMDBFlix.Services;
using TMDBFlix.Helpers;

namespace TMDBFlix.ViewModels
{
    /// <summary>
    /// Contains movie data and loading tasks
    /// </summary>
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

        public ObservableCollection<Torrent> Torrents = new ObservableCollection<Torrent>();

        public ImagesResponse Images = new ImagesResponse();

        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        async Task LoadInfo()
        {
            Movie = await Task.Run(() => TMDBService.GetMovie(Id));
            foreach (var v in Movie.credits.cast.OrderBy(x => x.order).ToList().ImagesFirst())
            {
                Cast.Add(v);
            }
            foreach (var v in Movie.credits.crew.ImagesFirst())
            {
                if (!Crew.Any(x => x.id == v.id)) Crew.Add(v);
                else Crew.Single(x => x.id == v.id).job += $", {v.job}";
                if (v.job.Equals("Director")) Directors.Add(v);
            }

        }

        async Task LoadImages()
        {
            Images = await Task.Run(() => TMDBService.GetImages($"/movie/{Id}/images"));
            foreach (var v in Images.backdrops)
            {
                Backdrops.Add(v);
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
            foreach (var v in recommendations.ImagesFirst())
            {
                Recommendations.Add(v);
            }
        }

        async Task LoadSimilar()
        {
            var similar = await Task.Run(() => TMDBService.GetMovieList($"/movie/{Id}/similar", new Dictionary<string, string>()));
            foreach (var v in similar.ImagesFirst())
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

            Posters.Add(new Image() { file_path = Movie.poster_path});
            foreach (var v in Images.posters)
            {
                if(!v.file_path.Equals(Movie.poster_path)) Posters.Add(v);
            }

            LoadCompleted();
        }

        public MovieDetailViewModel()
        {
        }

    }
}
