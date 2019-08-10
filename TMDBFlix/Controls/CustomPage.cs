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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

namespace TMDBFlix.Controls
{
    public class CustomPage : Page
    {
        public const int ONEROWGRID_DESIREDWIDTH = 178;
        public const int ONEROWGRID_TEXTHEIGHT = 35;
        public const double POSTERASPECTRATIO = 1.58;

        /// <summary>
        /// Gets the first ScrollViewer child of an element
        /// </summary>
        /// <param name="element">The parent element</param>
        /// <returns></returns>
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

        /// <summary>
        /// Scrolls forward a one row grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScrollForward(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            ScrollGrid((DependencyObject)this.FindName(btn.Tag.ToString()), true);
        }

        /// <summary>
        /// Scrolls backward a one row grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScrollBackward(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            ScrollGrid((DependencyObject)this.FindName(btn.Tag.ToString()), false);
        }

        /// <summary>
        /// Scrolls a grid one screen width
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="forward"></param>
        public void ScrollGrid(DependencyObject obj, bool forward)
        {
            ScrollViewer scrollViewer = GetScrollViewer(obj);
            scrollViewer.HorizontalScrollMode = ScrollMode.Enabled;
            if (forward) scrollViewer.ChangeView(scrollViewer.HorizontalOffset + scrollViewer.ViewportWidth-2, scrollViewer.VerticalOffset, null, false);
            else scrollViewer.ChangeView(scrollViewer.HorizontalOffset - scrollViewer.ViewportWidth+2, scrollViewer.VerticalOffset, null, false);
            scrollViewer.HorizontalScrollMode = ScrollMode.Disabled;
        }

        /// <summary>
        /// Loads placeholder image if image is failed to load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Image img = sender as Image;
            BitmapImage fallbackImage = new BitmapImage(new Uri("ms-appx:///Assets/Placeholder.jpg"));
            img.Source = fallbackImage;
        }

        /// <summary>
        /// Fixes bugs of one row grid
        /// </summary>
        /// <param name="element">The one row grid</param>
        /// <param name="width">Desired width of one grid element</param>
        /// <param name="height">Fixed height of one grid element</param>
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

        /// <summary>
        /// Sends app to full screen mode when YouTube video enters full screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
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
