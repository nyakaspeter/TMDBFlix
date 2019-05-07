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
    /// <summary>
    /// Contains season data and loading tasks
    /// </summary>
    public class SeasonDetailViewModel : ClickableViewModel
    {
        private Season season = new Season();

        public Season Season
        {
            get { return season; }
            set { Set(ref season, value); }
        }

        private Show show = new Show();

        public Show Show
        {
            get { return show; }
            set { Set(ref show, value); }
        }

        public int Id;
        public int SeasonNumber;
        public ObservableCollection<Video> Videos = new ObservableCollection<Video>();
        public ObservableCollection<Image> Backdrops = new ObservableCollection<Image>();
        public ObservableCollection<Image> Posters = new ObservableCollection<Image>();
        public ObservableCollection<Show> Recommendations = new ObservableCollection<Show>();
        public ObservableCollection<Show> Similar = new ObservableCollection<Show>();
        public ObservableCollection<Person> Cast = new ObservableCollection<Person>();
        public ObservableCollection<Person> Crew = new ObservableCollection<Person>();
        public ObservableCollection<Person> Creator = new ObservableCollection<Person>();

        public ImagesResponse Images = new ImagesResponse();

        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        async Task LoadInfo()
        {
            Show = await Task.Run(() => TMDBService.GetShow(Id));
            Season = await Task.Run(() => TMDBService.GetSeason(Id, SeasonNumber));
            foreach (var v in Season.credits.cast.OrderBy(x => x.order).ToList().ImagesFirst())
            {
                Cast.Add(v);
            }
            foreach (var v in Season.credits.crew.ImagesFirst())
            {
                Crew.Add(v);
            }
            foreach(var v in Season.episodes)
            {
                if(v.still_path != null) Backdrops.Add(new Image() { file_path = v.still_path });
            }
            Console.WriteLine();
        }

        async Task LoadImages()
        {
            Images = await Task.Run(() => TMDBService.GetImages($"/tv/{Id}/season/{SeasonNumber}/images"));
        }

        async Task LoadVideos()
        {
            var videos = await Task.Run(() => TMDBService.GetVideos($"/tv/{Id}/season/{SeasonNumber}"));
            foreach (var v in videos)
            {
                if(v.site.Equals("YouTube"))
                    Videos.Add(v);
            }
        }

        async Task LoadRecommendations()
        {
            var recommendations = await Task.Run(() => TMDBService.GetShowList($"/tv/{Id}/recommendations", new Dictionary<string, string>()));
            foreach (var v in recommendations.ImagesFirst())
            {
                Recommendations.Add(v);
            }
        }

        async Task LoadSimilar()
        {
            var similar = await Task.Run(() => TMDBService.GetShowList($"/tv/{Id}/similar", new Dictionary<string, string>()));
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
                LoadImages(),
                LoadVideos(),
                LoadRecommendations(),
                LoadSimilar()
            };

            await Task.WhenAll(tasks);

            Posters.Add(new Image() { file_path = Season.poster_path });
            foreach (var v in Images.posters)
            {
                if (!v.file_path.Equals(Season.poster_path)) Posters.Add(v);
            }

            LoadCompleted();
        }

        public SeasonDetailViewModel()
        {
        }

    }
}
