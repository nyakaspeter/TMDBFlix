using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDBFlix.Core.Models;
using TMDBFlix.Core.Services;
using TMDBFlix.Helpers;
using TMDBFlix.Services;
using TMDBFlix.Views;

namespace TMDBFlix.ViewModels
{
    public class PopularPeopleGridViewModel : Observable
    {
        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<SampleOrder>(OnItemClick));

        public ObservableCollection<Person> PopularPeople { get; set; }

        private int loadedPages = 0;

        public static bool loading = false;

        public async Task LoadData()
        {
            if (!loading)
            {
                loading = true;
                var Popularpeople = await Task.Run(() => TMDBService.GetPopularPeople(loadedPages + 1));
                foreach (var v in Popularpeople)
                {
                    PopularPeople.Add(v);
                }
                loadedPages += 2;
                loading = false;
            }
        }

        public PopularPeopleGridViewModel()
        {
            PopularPeople = new ObservableCollection<Person>();
            LoadData();
        }

        private void OnItemClick(SampleOrder clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ContentGridDetailPage>(clickedItem.OrderId);
            }
        }
    }
}
