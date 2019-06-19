using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMDBFlix.Core.Models;
using TMDBFlix.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TMDBFlix.Views
{
    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {
        
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            InitializeComponent();
           
            Window.Current.SetTitleBar(AppTitleBar);

            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);

            ViewModel.SearchResultNames.CollectionChanged += SearchResultNames_CollectionChanged;
        }

        private void SearchResultNames_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Multisearch.ItemsSource = ViewModel.SearchResultNames;
        }

        private void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if(!sender.Text.Equals(ShellViewModel.searchtext))ShellViewModel.searched = false;
            if (sender.Text.Equals(""))
            {
                ViewModel.SearchResultNames.Clear();
            }
            else
            {
                ViewModel.LoadSearchResultNames(sender.Text);
            }
        }

        private void Search_Find(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (!sender.Text.Equals(""))
            {
                ShellViewModel.searched = true;
                ShellViewModel.searchtext = sender.Text;
                shellFrame.Navigate(typeof(SearchGridPage), new Dictionary<string, string>()
                {
                    {"query", sender.Text }
                });

                var isTabStop = Multisearch.IsTabStop;
                Multisearch.IsTabStop = false;
                Multisearch.IsEnabled = false;
                Multisearch.IsEnabled = true;
                Multisearch.IsTabStop = isTabStop;
            }
        }

        private void Search_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            Multisearch.Text = args.SelectedItem.ToString();
        }

        private void Multisearch_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ShellViewModel.searched = false;
            if (Multisearch.Text.Equals(""))
            {
                ViewModel.SearchResultNames.Clear();
            }
            else
            {
                ViewModel.LoadSearchResultNames(Multisearch.Text);
            }
        }
    }
}
