using System;

using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;

namespace TMDBFlix.Views
{
    public sealed partial class TabbedPage : Page
    {
        public TabbedViewModel ViewModel { get; } = new TabbedViewModel();

        public TabbedPage()
        {
            InitializeComponent();
        }
    }
}
