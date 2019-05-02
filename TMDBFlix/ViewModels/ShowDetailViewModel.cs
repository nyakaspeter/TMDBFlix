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
    public class ShowDetailViewModel : ClickableViewModel
    {
        private Show show = new Show();

        public Show Show
        {
            get { return show; }
            set { Set(ref show, value); }
        }

        public int Id;
        public ObservableCollection<Video> Videos = new ObservableCollection<Video>();
        public ObservableCollection<Image> Backdrops = new ObservableCollection<Image>();
        public ObservableCollection<Image> Posters = new ObservableCollection<Image>();
        public ObservableCollection<Show> Recommendations = new ObservableCollection<Show>();
        public ObservableCollection<Show> Similar = new ObservableCollection<Show>();
        public ObservableCollection<Person> Cast = new ObservableCollection<Person>();
        public ObservableCollection<Person> Crew = new ObservableCollection<Person>();
        public ObservableCollection<Person> Creator = new ObservableCollection<Person>();

        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        async Task LoadInfo()
        {
            Show = await Task.Run(() => TMDBService.GetShow(Id));
            foreach (var v in Show.credits.cast.OrderBy(x => x.order).ToList().ImagesFirst())
            {
                Cast.Add(v);
            }
            foreach (var v in Show.credits.crew.ImagesFirst())
            {
                Crew.Add(v);
            }
            foreach (var v in Show.seasons)
            {
                if (v.season_number == 0)
                {
                    Show.seasons.Add(v);
                    Show.seasons.Remove(v);
                    break;
                }
            }
        }

        async Task LoadImages()
        {
            var images = await Task.Run(() => TMDBService.GetImages($"/tv/{Id}/images"));
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
            var videos = await Task.Run(() => TMDBService.GetVideos($"/tv/{Id}"));
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
                LoadRecommendations(),
                LoadSimilar(),
                LoadImages(),
                LoadVideos()
            };

            await Task.WhenAll(tasks);
            LoadCompleted();
        }

        public ShowDetailViewModel()
        {
        }

    }
}
