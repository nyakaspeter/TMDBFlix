using System;

using Microsoft.Toolkit.Uwp.UI.Animations;
using TMDBFlix.Controls;
using TMDBFlix.Services;
using TMDBFlix.Helpers;
using TMDBFlix.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace TMDBFlix.Views
{
    public sealed partial class CollectionDetailPage : CustomPage
    {
        public CollectionDetailViewModel ViewModel { get; } = new CollectionDetailViewModel();

        public CollectionDetailPage()
        {
            InitializeComponent();

            ViewModel.LoadCompleted += ViewModel_LoadCompleted;

            FadeOutContent.Begin();
        }

        private void ViewModel_LoadCompleted()
        {
            if (ViewModel.Collection.overview is null || ViewModel.Collection.overview.Equals("")) overview.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Posters.Count == 0) pivot.Items.Remove(posterspivot);

            if (ViewModel.Backdrops.Count == 0) pivot.Items.Remove(backdropspivot);

            LoadRing.IsActive = false;
            FadeInContent.Begin();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is int Id)
            {
                ViewModel.Id = Id;
                ViewModel.LoadData();
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.Collection);
            }
        }

    }
}
