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
    /// Contains person list data and loading tasks
    /// </summary>
    public class PeopleGridViewModel : ClickableViewModel
    {
        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();

        public int LoadedPages = 0;

        public delegate void loadedMore();
        public event loadedMore LoadedMore;

        public string ListName = new ResourceLoader().GetString("Shell_People/Content");
        public string Path = "/person/popular";
        public Dictionary<string, string> Query = new Dictionary<string, string>();

        public async Task LoadData()
        {
            LoadedPages++;
            var results = await Task.Run(() => TMDBService.GetPersonList(Path, Query, LoadedPages, 1));
            if (results.Count != 0) LoadedMore();
            foreach (var v in results)
            {
                People.Add(v);
            }   
        }

        public PeopleGridViewModel()
        {
        }
    }
}
