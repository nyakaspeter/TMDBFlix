using System;

using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;

namespace TMDBFlix.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
