using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TMDBFlix.Helpers
{
    public static class UIHelpers
    {
        /// <summary>
        /// Scrolls a ScrollViewer to an element
        /// </summary>
        /// <param name="scrollViewer">The ScrollViewer</param>
        /// <param name="element">The element to scroll to</param>
        /// <param name="isVerticalScrolling"></param>
        /// <param name="smoothScrolling"></param>
        /// <param name="zoomFactor"></param>
        public static void ScrollToElement(this ScrollViewer scrollViewer, UIElement element,
        bool isVerticalScrolling = true, bool smoothScrolling = true, float? zoomFactor = null)
        {
            var transform = element.TransformToVisual((UIElement)scrollViewer.Content);
            var position = transform.TransformPoint(new Point(0, 0));

            if (isVerticalScrolling)
            {
                scrollViewer.ChangeView(null, position.Y + 2, zoomFactor, !smoothScrolling);
            }
            else
            {
                scrollViewer.ChangeView(position.X, null, zoomFactor, !smoothScrolling);
            }
        }
    }
}
