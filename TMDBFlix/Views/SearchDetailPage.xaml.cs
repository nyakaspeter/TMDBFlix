﻿using System;

using Microsoft.Toolkit.Uwp.UI.Animations;

using TMDBFlix.Services;
using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TMDBFlix.Views
{
    public sealed partial class SearchDetailPage : Page
    {
        public SearchDetailViewModel ViewModel { get; } = new SearchDetailViewModel();

        public SearchDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is long orderId)
            {
                ViewModel.Initialize(orderId);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }
}