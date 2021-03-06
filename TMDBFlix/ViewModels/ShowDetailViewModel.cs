﻿using System;
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
    /// Contains show data and loading tasks
    /// </summary>
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

        public ObservableCollection<Torrent> Torrents = new ObservableCollection<Torrent>();

        public ImagesResponse Images = new ImagesResponse();

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
                if (!Crew.Any(x => x.id == v.id)) Crew.Add(v);
                else Crew.Single(x => x.id == v.id).job += $", {v.job}";
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
            Images = await Task.Run(() => TMDBService.GetImages($"/tv/{Id}/images"));
            foreach (var v in Images.backdrops)
            {
                Backdrops.Add(v);
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

            Posters.Add(new Image() { file_path = Show.poster_path });
            foreach (var v in Images.posters)
            {
                if (!v.file_path.Equals(Show.poster_path)) Posters.Add(v);
            }

            foreach (var v in Show.seasons)
            {
                v.showid = Id;
            }

            LoadCompleted();
        }

        public ShowDetailViewModel()
        {
        }

    }
}
