using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDBFlix.Core.Models;
using TMDBFlix.Core.Services;
using TMDBFlix.Helpers;
using TMDBFlix.Services;

using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace TMDBFlix.ViewModels
{
    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings.md
    public class SettingsViewModel : Observable
    {
        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        public ObservableCollection<LanguageConfiguration> Languages = new ObservableCollection<LanguageConfiguration>();
        public ObservableCollection<CountryConfiguration> Countries = new ObservableCollection<CountryConfiguration>();
        public ObservableCollection<Indexer> Indexers = new ObservableCollection<Indexer>();

        public ObservableCollection<Category> MovieCategories = new ObservableCollection<Category>()
        {
            new Category("2000","All"),
            new Category("2030","SD"),
            new Category("2040","HD"),
            new Category("2080","WEB-DL"),
            new Category("2060","BluRay"),
            new Category("2050","3D"),
            new Category("2070","DVD"),
            new Category("2010","Foreign"),
            new Category("2020","Other")
        };

        public ObservableCollection<Category> TVCategories = new ObservableCollection<Category>()
        {
            new Category("5000","All"),
            new Category("5030","SD"),
            new Category("5040","HD"),
            new Category("5010","WEB-DL"),
            new Category("5060","Sport"),
            new Category("5070","Anime"),
            new Category("5080","Documentary"),
            new Category("5020","Foreign"),
            new Category("5999","Other")
        };

        private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { Set(ref _elementTheme, value); }
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get { return _versionDescription; }

            set { Set(ref _versionDescription, value); }
        }

        private ICommand _switchThemeCommand;

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            ElementTheme = param;
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return _switchThemeCommand;
            }
        }

        public SettingsViewModel()
        {
            LoadData();
        }

        async Task LoadLanguages()
        {
            var languages = await Task.Run(() => TMDBService.GetLanguages());
            languages.ForEach(x => Languages.Add(x));
        }

        async Task LoadCountries()
        {
            var countries = await Task.Run(() => TMDBService.GetCountries());
            countries.ForEach(x => Countries.Add(x));
        }

        async Task LoadIndexers()
        {
            var indexers = await Task.Run(() => JackettService.GetConfiguredIndexers());
            indexers.ForEach(x => Indexers.Add(x));
        }

        public async void LoadData()
        {
            var tasks = new List<Task>()
            {
                LoadLanguages(),
                LoadCountries(),
                LoadIndexers()
            };

            await Task.WhenAll(tasks);

            foreach(var i in Indexers)
            {
                if (JackettService.Indexers.Contains(i.Id)) i.Enabled = true;
            }

            foreach(var c in MovieCategories)
            {
                if (JackettService.MovieCategories.Contains(c.Id)) c.Enabled = true;
            }

            foreach (var c in TVCategories)
            {
                if (JackettService.TVCategories.Contains(c.Id)) c.Enabled = true;
            }

            LoadCompleted();
        }

        public async Task InitializeAsync()
        {
            VersionDescription = GetVersionDescription();
            await Task.CompletedTask;
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
