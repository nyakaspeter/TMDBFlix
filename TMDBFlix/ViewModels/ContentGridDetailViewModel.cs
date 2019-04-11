using System;
using System.Linq;

using TMDBFlix.Core.Models;
using TMDBFlix.Core.Services;
using TMDBFlix.Helpers;

namespace TMDBFlix.ViewModels
{
    public class ContentGridDetailViewModel : Observable
    {
        private SampleOrder _item;

        public SampleOrder Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public ContentGridDetailViewModel()
        {
        }

        public void Initialize(long orderId)
        {
            var data = SampleDataService.GetContentGridData();
            Item = data.First(i => i.OrderId == orderId);
        }
    }
}
