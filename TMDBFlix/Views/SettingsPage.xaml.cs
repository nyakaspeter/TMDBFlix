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

            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["autoplay"] is null) Windows.Storage.ApplicationData.Current.LocalSettings.Values["autoplay"] = "-";
            var autoplay = Windows.Storage.ApplicationData.Current.LocalSettings.Values["autoplay"] as string;
            if (autoplay.Equals("-")) Settings_Streaming_AutoplayBox.SelectedItem = Settings_Streaming_NoAutoPlay;
            if (autoplay.Equals("vlc")) Settings_Streaming_AutoplayBox.SelectedItem = Settings_Streaming_Autoplay_VLC;
            if (autoplay.Equals("mpc-hc")) Settings_Streaming_AutoplayBox.SelectedItem = Settings_Streaming_Autoplay_MPCHC;
            if (autoplay.Equals("potplayer")) Settings_Streaming_AutoplayBox.SelectedItem = Settings_Streaming_Autoplay_PotPlayer;

            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["folderpath"] is null) Windows.Storage.ApplicationData.Current.LocalSettings.Values["folderpath"] = "";
            StreamingFilePath.Text = Windows.Storage.ApplicationData.Current.LocalSettings.Values["folderpath"] as string;

            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["showfiles"] is null) Windows.Storage.ApplicationData.Current.LocalSettings.Values["showfiles"] = true;
            StreamingShowFileList.IsChecked = Windows.Storage.ApplicationData.Current.LocalSettings.Values["showfiles"] as bool?;

            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["keepfiles"] is null) Windows.Storage.ApplicationData.Current.LocalSettings.Values["keepfiles"] = true;
            StreamingKeepFiles.IsChecked = Windows.Storage.ApplicationData.Current.LocalSettings.Values["keepfiles"] as bool?;

        }

        private void ViewModel_LoadCompleted()
        {
            TMDBLanguage.SelectedItem = ViewModel.Languages.Where(x => x.iso_639_1.Equals(TMDBService.language)).ToList()[0];
            TMDBRegion.SelectedItem = ViewModel.Countries.Where(x => x.iso_3166_1.Equals(TMDBService.region)).ToList()[0];
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

        private void StreamingFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["folderpath"] = StreamingFilePath.Text;
        }

        private void Autoplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Settings_Streaming_AutoplayBox.SelectedItem == Settings_Streaming_NoAutoPlay) Windows.Storage.ApplicationData.Current.LocalSettings.Values["autoplay"] = "-";
            if(Settings_Streaming_AutoplayBox.SelectedItem == Settings_Streaming_Autoplay_VLC) Windows.Storage.ApplicationData.Current.LocalSettings.Values["autoplay"] = "vlc";
            if(Settings_Streaming_AutoplayBox.SelectedItem == Settings_Streaming_Autoplay_MPCHC) Windows.Storage.ApplicationData.Current.LocalSettings.Values["autoplay"] = "mpc-hc";
            if(Settings_Streaming_AutoplayBox.SelectedItem == Settings_Streaming_Autoplay_PotPlayer) Windows.Storage.ApplicationData.Current.LocalSettings.Values["autoplay"] = "potplayer";
        }

        private async void Folderpicker_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.FileTypeFilter.Add("*");

            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null) StreamingFilePath.Text = folder.Path;
        }

        private void StreamingShowFileList_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["showfiles"] = true;
        }

        private void StreamingShowFileList_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["showfiles"] = false;
        }

        private void StreamingKeepFiles_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["keepfiles"] = true;
        }

        private void StreamingKeepFiles_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["keepfiles"] = false;
        }
    }
}
