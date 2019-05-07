using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDBFlix.Core.Models;
using TMDBFlix.Core.Services;
using TMDBFlix.Helpers;
using TMDBFlix.Services;
using TMDBFlix.Views;
using Windows.ApplicationModel.Resources;
using System.Linq;

namespace TMDBFlix.ViewModels
{
    /// <summary>
    /// Contains search item list data and loading tasks
    /// </summary>
    public class SearchGridViewModel : ClickableViewModel
    {
        public ObservableCollection<MultiSearchItem> Results { get; set; } = new ObservableCollection<MultiSearchItem>();
        public ObservableCollection<MultiSearchItem> Movies { get; set; } = new ObservableCollection<MultiSearchItem>();
        public ObservableCollection<MultiSearchItem> Shows { get; set; } = new ObservableCollection<MultiSearchItem>();
        public ObservableCollection<MultiSearchItem> People { get; set; } = new ObservableCollection<MultiSearchItem>();

        public int LoadedPages = 0;

        public delegate void loadedMore();
        public event loadedMore LoadedMore;

        public delegate void noMore();
        public event noMore NoMore;

        public Dictionary<string, string> Query = new Dictionary<string, string>();

        public async Task LoadData()
        {
            LoadedPages++;
            var results = await Task.Run(() => TMDBService.Search(Query["query"], LoadedPages, 1));

            foreach (var v in results.OnlyWithImages())
            {
                Results.Add(v);
                if (v.media_type.Equals("movie")) Movies.Add(v);
                if (v.media_type.Equals("tv")) Shows.Add(v);
                if (v.media_type.Equals("person")) People.Add(v);
            }

            if (results.Count != 0) LoadedMore();
            else NoMore();

        }

        public SearchGridViewModel()
        {
        }
    }
}
