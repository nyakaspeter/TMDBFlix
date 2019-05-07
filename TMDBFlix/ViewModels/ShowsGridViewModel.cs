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

namespace TMDBFlix.ViewModels
{
    /// <summary>
    /// Contains show list data and loading tasks
    /// </summary>
    public class ShowsGridViewModel : ClickableViewModel
    {

        public ObservableCollection<Show> Shows { get; set; } = new ObservableCollection<Show>();

        public int LoadedPages = 0;

        public delegate void loadedMore();
        public event loadedMore LoadedMore;

        public string ListName = new ResourceLoader().GetString("Shell_Shows/Content");
        public string Path = "/discover/tv";
        public Dictionary<string, string> Query = new Dictionary<string, string>();

        public async Task LoadData()
        {
            LoadedPages++;
            var results = await Task.Run(() => TMDBService.GetShowList(Path, Query, LoadedPages, 1));
            if (results.Count != 0) LoadedMore();
            foreach (var v in results)
            {
                Shows.Add(v);
            }   
        }

        public ShowsGridViewModel()
        {
        }
    }
}
