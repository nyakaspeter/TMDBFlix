using System;

using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using TMDBFlix.Helpers;
using TMDBFlix.Controls;

namespace TMDBFlix.Views
{
    public sealed partial class SearchPage : CustomPage
    {
        public SearchViewModel ViewModel { get; } = new SearchViewModel();

        public SearchPage()
        {
            InitializeComponent();

            Loaded += SearchPage_Loaded;

            ViewModel.NonStaticPeopleResults.CollectionChanged += PeopleResults_CollectionChanged;
            ViewModel.NonStaticMovieResults.CollectionChanged += MovieResults_CollectionChanged;
            ViewModel.NonStaticShowResults.CollectionChanged += ShowResults_CollectionChanged;

            FadeOutMovieResults.Completed += FadeOutMovieResults_Completed;
            FadeOutShowResults.Completed += FadeOutShowResults_Completed;
            FadeOutPeopleResults.Completed += FadeOutPeopleResults_Completed;
        }

        private void SearchPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GetScrollViewer(MovieResults).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(ShowResults).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(PeopleResults).HorizontalScrollMode = ScrollMode.Disabled;

            GetScrollViewer(MovieResults).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(ShowResults).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(PeopleResults).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;

            FadeOutPeopleResults.Begin();
            FadeOutMovieResults.Begin();
            FadeOutShowResults.Begin();
        }
        
        private void ShowResults_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (SearchViewModel.ShowResults.Count == 0) { FadeOutShowResults.Begin(); }
            else { FadeOutShowResults.Stop(); ShowResults.Visibility = Visibility.Visible; FadeInShowResults.Begin(); }
        }

        private void MovieResults_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (SearchViewModel.MovieResults.Count == 0) { FadeOutMovieResults.Begin(); }
            else { FadeOutMovieResults.Stop(); MovieResults.Visibility = Visibility.Visible; FadeInMovieResults.Begin(); }
        }

        private void PeopleResults_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (SearchViewModel.PeopleResults.Count == 0) { FadeOutPeopleResults.Begin(); }
            else { FadeOutPeopleResults.Stop(); PeopleResults.Visibility = Visibility.Visible; FadeInPeopleResults.Begin(); }
        }
        
        private void FadeOutPeopleResults_Completed(object sender, object e)
        {
            PeopleResults.Visibility = Visibility.Collapsed;
        }

        private void FadeOutShowResults_Completed(object sender, object e)
        {
            ShowResults.Visibility = Visibility.Collapsed;
        }

        private void FadeOutMovieResults_Completed(object sender, object e)
        {
            MovieResults.Visibility = Visibility.Collapsed;
        }

        private void Search_Find(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (!sender.Text.Equals(""))
            {
            }
        }

        private void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (sender.Text.Equals(""))
            {
            }
            else
            {
            }
        }
    }
}
