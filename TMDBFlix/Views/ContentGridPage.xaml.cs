using System;

using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;

namespace TMDBFlix.Views
{
    public sealed partial class ContentGridPage : Page
    {
        public ContentGridViewModel ViewModel { get; } = new ContentGridViewModel();

        public ContentGridPage()
        {
            InitializeComponent();
        }
    }
}
