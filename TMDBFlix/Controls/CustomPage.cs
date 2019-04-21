using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDBFlix.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TMDBFlix.Controls
{
    public class CustomPage : Page
    {
        public ScrollViewer GetScrollViewer(DependencyObject element)
        {
            if (element is ScrollViewer)
            {
                return (ScrollViewer)element;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);

                var result = GetScrollViewer(child);
                if (result == null)
                {
                    continue;
                }
                else
                {
                    return result;
                }
            }
            return null;
        }

        public void ScrollForward(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            ScrollGrid((DependencyObject)this.FindName(btn.Tag.ToString()), true);
        }

        public void ScrollBackward(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            ScrollGrid((DependencyObject)this.FindName(btn.Tag.ToString()), false);
        }

        public void ScrollGrid(DependencyObject obj, bool forward)
        {
            ScrollViewer scrollViewer = GetScrollViewer(obj);
            scrollViewer.HorizontalScrollMode = ScrollMode.Enabled;
            if (forward) scrollViewer.ChangeView(scrollViewer.HorizontalOffset + Math.Floor((Window.Current.Bounds.Width - 52) / 168) * 172, scrollViewer.VerticalOffset, null, false);
            else scrollViewer.ChangeView(scrollViewer.HorizontalOffset - Math.Floor((Window.Current.Bounds.Width - 52) / 168) * 172, scrollViewer.VerticalOffset, null, false);
            scrollViewer.HorizontalScrollMode = ScrollMode.Disabled;
        }

        
    }
}
