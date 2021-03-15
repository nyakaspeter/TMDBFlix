using System;
using Windows.UI.Xaml;
using TMDBFlix.Controls;
using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;
using System.Collections.Generic;
using TMDBFlix.Models;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Navigation;
using TMDBFlix.Services;

namespace TMDBFlix.Views
{
    public sealed partial class SearchGridPage : CustomPage
    {

        public SearchGridViewModel ViewModel { get; } = new SearchGridViewModel();

        public SearchGridPage()
        {
            InitializeComponent();
            Scroller.ViewChanged += Scroller_ViewChanged;
            ViewModel.LoadedMore += ViewModel_LoadedMore;
            ViewModel.NoMore += ViewModel_NoMore;
            pivot.SelectionChanged += Pivot_SelectionChanged;

            FadeOutContent.Begin();
        }

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pivot.SelectedItem == allpivot) showall.Visibility = Visibility.Collapsed;
            else showall.Visibility = Visibility.Visible;

            if (Scroller.ScrollableHeight == 0) await ViewModel.LoadData();
        }

        private void ViewModel_NoMore()
        {
            if(ViewModel.Results.Count == 0)
            {
                pivot.Visibility = Visibility.Collapsed;
                NoResults.Visibility = Visibility.Visible;
                LoadRing.IsActive = false;
            }
        }

        private async void ViewModel_LoadedMore()
        {
            if (ViewModel.Movies.Count == 0) pivot.Items.Remove(moviespivot);
            if (ViewModel.Shows.Count == 0) pivot.Items.Remove(showspivot);
            if (ViewModel.People.Count == 0) pivot.Items.Remove(peoplepivot);

            LoadRing.IsActive = false;
            FadeInContent.Begin();
            if (Scroller.ScrollableHeight == 0) await ViewModel.LoadData();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ViewModel.Query = e.Parameter as Dictionary<string, string>;
            }

            ListName.Text = ViewModel.Query["query"];
            
            //ViewModel.LoadData();
        }

        private async void Scroller_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (Scroller.VerticalOffset == Scroller.ScrollableHeight) await ViewModel.LoadData();
        }

        private void ShowAll_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if(pivot.SelectedItem == moviespivot)
            {
                Frame.Navigate(typeof(MoviesGridPage), new Dictionary<string, string>(){
                {"listname", ViewModel.Query["query"] },
                { "path", "/search/movie"},
                {"query", ViewModel.Query["query"] }
                });
            }
            if (pivot.SelectedItem == showspivot)
            {
                Frame.Navigate(typeof(ShowsGridPage), new Dictionary<string, string>(){
                {"listname", ViewModel.Query["query"] },
                { "path", "/search/tv"},
                {"query", ViewModel.Query["query"] }
                });
            }
            if (pivot.SelectedItem == peoplepivot)
            {
                Frame.Navigate(typeof(PeopleGridPage), new Dictionary<string, string>(){
                {"listname", ViewModel.Query["query"] },
                { "path", "/search/person"},
                {"query", ViewModel.Query["query"] }
                });
            }
        }
    }
}
