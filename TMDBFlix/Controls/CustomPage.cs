using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDBFlix.Helpers;
using TMDBFlix.ViewModels;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TMDBFlix.Controls
{
    public class CustomPage : Page
    {
        public const int ONEROWGRID_DESIREDWIDTH = 178;
        public const int ONEROWGRID_TEXTHEIGHT = 35;
        public const double POSTERASPECTRATIO = 1.58;

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
            if (forward) scrollViewer.ChangeView(scrollViewer.HorizontalOffset + scrollViewer.ViewportWidth-2, scrollViewer.VerticalOffset, null, false);
            else scrollViewer.ChangeView(scrollViewer.HorizontalOffset - scrollViewer.ViewportWidth+2, scrollViewer.VerticalOffset, null, false);
            scrollViewer.HorizontalScrollMode = ScrollMode.Disabled;
        }

        public void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Image img = sender as Image;
            BitmapImage fallbackImage = new BitmapImage(new Uri("ms-appx:///Assets/Placeholder.jpg"));
            img.Source = fallbackImage;
        }

        public void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var grid = sender as AdaptiveGridView;
            if (grid.ActualWidth != 0)
                grid.ItemHeight = grid.ActualWidth / Math.Round(grid.ActualWidth / ONEROWGRID_DESIREDWIDTH) * POSTERASPECTRATIO + ONEROWGRID_TEXTHEIGHT;
        }

        public void FixOneRowGrid(DependencyObject element, double width = ONEROWGRID_DESIREDWIDTH, double height = ONEROWGRID_DESIREDWIDTH * POSTERASPECTRATIO + ONEROWGRID_TEXTHEIGHT)
        {
            if (element is AdaptiveGridView)
            {
                var grid = element as AdaptiveGridView;
                grid.DesiredWidth = width;
                grid.ItemHeight = height;

                var scrollviewer = GetScrollViewer(grid);
                if(scrollviewer != null)
                {
                    scrollviewer.HorizontalScrollMode = ScrollMode.Disabled;
                    scrollviewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                }
                
            }
        }

        public void WebView_ContainsFullScreenElementChanged(WebView sender, object args)
        {
            var applicationView = ApplicationView.GetForCurrentView();
            
            if (sender.ContainsFullScreenElement)
            {
                applicationView.TryEnterFullScreenMode();
                ShellViewModel._navigationView.IsPaneVisible = false;
                ShellViewModel._navigationView.IsBackButtonVisible = Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Collapsed;
            }
            else if (applicationView.IsFullScreenMode)
            {
                applicationView.ExitFullScreenMode();
                ShellViewModel._navigationView.IsPaneVisible = true;
                ShellViewModel._navigationView.IsBackButtonVisible = Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Auto;
            }
        }
    }
}
