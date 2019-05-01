﻿using System;
using Windows.UI.Xaml;
using TMDBFlix.Controls;
using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;
using System.Collections.Generic;
using TMDBFlix.Core.Models;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Navigation;
using TMDBFlix.Core.Services;

namespace TMDBFlix.Views
{
    public sealed partial class MoviesGridPage : CustomPage
    {

        public MoviesGridViewModel ViewModel { get; } = new MoviesGridViewModel();

        public MoviesGridPage()
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ViewModel.Query = e.Parameter as Dictionary<string, string>;
                ViewModel.Path = ViewModel.Query["path"];
                ViewModel.ListName = ViewModel.Query["listname"];
            }

            ListName.Text = ViewModel.ListName;
            
            ViewModel.LoadData();
        }

        private void Scroller_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (Scroller.VerticalOffset == Scroller.ScrollableHeight) ViewModel.LoadData();
        }
    }
}
