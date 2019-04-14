using System;

using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;

namespace TMDBFlix.Views
{
    public sealed partial class DiscoverPage : Page
    {
        public DiscoverViewModel ViewModel { get; } = new DiscoverViewModel();

        public DiscoverPage()
        {
            InitializeComponent();
        }
    }
}
