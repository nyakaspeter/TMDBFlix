using System;

using TMDBFlix.ViewModels;

using Windows.UI.Xaml.Controls;

namespace TMDBFlix.Views
{
    public sealed partial class ImageGalleryPage : Page
    {
        public ImageGalleryViewModel ViewModel { get; } = new ImageGalleryViewModel();

        public ImageGalleryPage()
        {
            InitializeComponent();
        }
    }
}
