using System;
using System.Collections.Generic;
using System.Linq;
using TMDBFlix.Core.Models;
using TMDBFlix.Core.Services;
using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TMDBFlix.Views
{
    // TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere
    public sealed partial class SettingsPage : Page
    {
        public SettingsViewModel ViewModel { get; } = new SettingsViewModel();

        public SettingsPage()
        {
            InitializeComponent();

            ViewModel.LoadCompleted += ViewModel_LoadCompleted;

            JackettApiUrl.Text = JackettService.Url;
            JackettApiKey.Text = JackettService.Key;
        }

        private void ViewModel_LoadCompleted()
        {
            TMDBLanguage.SelectedItem = ViewModel.Languages.Where(x => x.iso_639_1.Equals(TMDBService.language)).ToList()[0];
            TMDBRegion.SelectedItem = ViewModel.Countries.Where(x => x.iso_3166_1.Equals(TMDBService.region)).ToList()[0];
            Indexer_Checkbox.InvalidateArrange();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeAsync();
        }

        private void TMDBLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = TMDBLanguage.SelectedItem as LanguageConfiguration;
            TMDBService.Language = selected.iso_639_1;
            
        }

        private void TMDBRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = TMDBRegion.SelectedItem as CountryConfiguration;
            TMDBService.Region = selected.iso_3166_1;
        }

        private void JackettApiUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            JackettService.Url = JackettApiUrl.Text;
        }

        private void JackettApiKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            JackettService.Key = JackettApiKey.Text;
        }

        private void CheckBox_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<string> enabled = new List<string>();
            foreach (var i in ViewModel.Indexers)
            {
                if (i.Enabled) enabled.Add(i.Id);
            }
            JackettService.Indexers = enabled;
        }

        private void MovieCategory_CheckBox_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<string> enabled = new List<string>();
            foreach (var c in ViewModel.MovieCategories)
            {
                if (c.Enabled) enabled.Add(c.Id);
            }
            JackettService.MovieCategories = enabled;
        }

        private void TVCategory_CheckBox_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<string> enabled = new List<string>();
            foreach (var c in ViewModel.TVCategories)
            {
                if (c.Enabled) enabled.Add(c.Id);
            }
            JackettService.TVCategories = enabled;
        }
    }
}
