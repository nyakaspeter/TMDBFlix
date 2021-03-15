using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDBFlix.Models;
using TMDBFlix.Services;
using TMDBFlix.Helpers;
using TMDBFlix.Views;
using Windows.ApplicationModel.Resources;

namespace TMDBFlix.ViewModels
{
    /// <summary>
    /// Contains movie list data and loading tasks
    /// </summary>
    public class MoviesGridViewModel : ClickableViewModel
    {
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        public int LoadedPages = 0;

        public delegate void loadedMore();
        public event loadedMore LoadedMore;

        public string ListName = new ResourceLoader().GetString("Shell_Movies/Content");
        public string Path = "/discover/movie";
        public Dictionary<string, string> Query = new Dictionary<string, string>();

        public async Task LoadData()
        {
            LoadedPages++;
            var results = await Task.Run(() => TMDBService.GetMovieList(Path, Query, LoadedPages, 1));
            if (results.Count != 0) LoadedMore();
            foreach (var v in results)
            {
                Movies.Add(v);
            }   
        }

        public MoviesGridViewModel()
        {
        }
    }
}
