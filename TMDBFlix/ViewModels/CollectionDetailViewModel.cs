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
    /// Contains collection data and loading tasks
    /// </summary>
    public class CollectionDetailViewModel : ClickableViewModel
    {
        private Collection collection = new Collection();

        public Collection Collection
        {
            get { return collection; }
            set { Set(ref collection, value); }
        }

        public int Id;
        public ObservableCollection<Image> Backdrops = new ObservableCollection<Image>();
        public ObservableCollection<Image> Posters = new ObservableCollection<Image>();
        public ObservableCollection<Movie> Parts = new ObservableCollection<Movie>();

        public ImagesResponse Images = new ImagesResponse();

        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        async Task LoadInfo()
        {
            Collection = await Task.Run(() => TMDBService.GetCollection(Id));
            foreach (var v in Collection.parts.OrderBy(x => x.release_date).ToList())
            {
                Parts.Add(v);
            }
        }

        async Task LoadImages()
        {
            Images = await Task.Run(() => TMDBService.GetImages($"/collection/{Id}/images"));
            foreach (var v in Images.backdrops)
            {
                Backdrops.Add(v);
            }
        }

        public async void LoadData()
        {
            var tasks = new List<Task>()
            {
                LoadInfo(),
                LoadImages()
            };

            await Task.WhenAll(tasks);

            Posters.Add(new Image() { file_path = Collection.poster_path});
            foreach (var v in Images.posters)
            {
                if(!v.file_path.Equals(Collection.poster_path)) Posters.Add(v);
            }

            LoadCompleted();
        }

        public CollectionDetailViewModel()
        {
        }

    }
}
