using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TMDBFlix.Helpers
{
    /// <summary>
    /// Data template selector for displaying last items in a list differently
    /// </summary>
    public class MyTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CommonTemplate { get; set; }
        public DataTemplate LastTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var wg = GetWrapGrid(container);
            if (wg != null)
            {
                var i = wg.Items.IndexOf(item);
                if (i == wg.Items.Count - 1)
                {
                    return LastTemplate;
                }
            }
            return CommonTemplate;

        }

        public static ItemsControl GetWrapGrid(DependencyObject element)
        {
            var parent = VisualTreeHelper.GetParent(element);
            if (parent == null)
            {
                return null;
            }
            var parentWrapGrid = parent as ItemsControl;
            return parentWrapGrid ?? GetWrapGrid(parent);
        }
    }
}
