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
    public sealed partial class PeopleGridPage : CustomPage
    {

        public PeopleGridViewModel ViewModel { get; } = new PeopleGridViewModel();

        public PeopleGridPage()
        {
            InitializeComponent();
            Scroller.ViewChanged += Scroller_ViewChanged;
            ViewModel.LoadedMore += ViewModel_LoadedMore;
        }

        private async void ViewModel_LoadedMore()
        {
            LoadRing.IsActive = false;
            if (Scroller.ScrollableHeight == 0) await ViewModel.LoadData();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ViewModel.Query = e.Parameter as Dictionary<string, string>;
                ViewModel.Path = ViewModel.Query["path"];
                ViewModel.ListName = ViewModel.Query["listname"];
            }

            ListName.Text = ViewModel.ListName;

            await ViewModel.LoadData();
        }

        private async void Scroller_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (Scroller.VerticalOffset == Scroller.ScrollableHeight) await ViewModel.LoadData();
        }
    }
}
