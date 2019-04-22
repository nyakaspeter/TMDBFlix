using System;
using System.Collections.ObjectModel;
using TMDBFlix.Core.Models;
using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;

namespace TMDBFlix.Views
{
    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {
        
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public static void SetSection(int s)
        {
            ShellViewModel.Section.Clear();
            ShellViewModel.Section.Add(s);
        }

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);

            ViewModel.SearchResultNames.CollectionChanged += SearchResultNames_CollectionChanged;
            ViewModel.NonStaticSection.CollectionChanged += Section_CollectionChanged;
            navigationView.ItemInvoked += NavigationView_ItemInvoked;
        }

        private void NavigationView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            ContentGridViewModel.Section.Clear();
            ContentGridViewModel.Section.Add(navigationView.MenuItems.IndexOf(navigationView.SelectedItem));
        }

        private void Section_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(ViewModel.NonStaticSection.Count != 0) navigationView.SelectedItem = navigationView.MenuItems[(int)e.NewItems[0]];
        }

        private void SearchResultNames_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Multisearch.ItemsSource = sender;
        }

        private void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
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
                SearchViewModel.SearchString = sender.Text;
                if(shellFrame.CurrentSourcePageType != typeof(SearchPage)) shellFrame.Navigate(typeof(SearchPage));
            }
        }
    }
}
