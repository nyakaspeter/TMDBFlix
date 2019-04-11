using System;
using System.Collections.ObjectModel;
using System.Linq;

using TMDBFlix.Core.Models;
using TMDBFlix.Core.Services;
using TMDBFlix.Helpers;

using Windows.UI.Xaml.Navigation;

namespace TMDBFlix.ViewModels
{
    public class ImageGalleryDetailViewModel : Observable
    {
        private object _selectedImage;
        private ObservableCollection<SampleImage> _source;

        public object SelectedImage
        {
            get => _selectedImage;
            set
            {
                Set(ref _selectedImage, value);
                ImagesNavigationHelper.UpdateImageId(ImageGalleryViewModel.ImageGallerySelectedIdKey, ((SampleImage)SelectedImage).ID);
            }
        }

        public ObservableCollection<SampleImage> Source
        {
            get => _source;
            set => Set(ref _source, value);
        }

        public ImageGalleryDetailViewModel()
        {
            // TODO WTS: Replace this with your actual data
            Source = SampleDataService.GetGallerySampleData();
        }

        public void Initialize(string selectedImageID, NavigationMode navigationMode)
        {
            if (!string.IsNullOrEmpty(selectedImageID) && navigationMode == NavigationMode.New)
            {
                SelectedImage = Source.FirstOrDefault(i => i.ID == selectedImageID);
            }
            else
            {
                selectedImageID = ImagesNavigationHelper.GetImageId(ImageGalleryViewModel.ImageGallerySelectedIdKey);
                if (!string.IsNullOrEmpty(selectedImageID))
                {
                    SelectedImage = Source.FirstOrDefault(i => i.ID == selectedImageID);
                }
            }
        }
    }
}
