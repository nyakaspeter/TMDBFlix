using System;

using TMDBFlix.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TMDBFlix.Views
{
    public sealed partial class ContentGridPage : Page
    {
        static double WindowWidth;

        public ContentGridViewModel ViewModel { get; } = new ContentGridViewModel();

        public ContentGridPage()
        {
            InitializeComponent();
            
            Window.Current.SizeChanged += OnWindowSizeChanged;
            Loaded += ContentGridPage_Loaded;
        }

        private void ContentGridPage_Loaded(object sender, RoutedEventArgs e)
        {
            WindowWidth = Window.Current.Bounds.Width;
            GetScrollViewer(PopularMovies).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(PopularShows).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(PopularPeople).HorizontalScrollMode = ScrollMode.Disabled;
            GetScrollViewer(PopularMovies).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(PopularShows).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            GetScrollViewer(PopularPeople).HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }

        private void OnWindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            WindowWidth = e.Size.Width;
        }

        public static ScrollViewer GetScrollViewer(DependencyObject element)
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

        public static void ScrollGrid(DependencyObject obj, bool forward)
        {
            ScrollViewer scrollViewer = GetScrollViewer(obj);
            scrollViewer.HorizontalScrollMode = ScrollMode.Enabled;
            if (forward) scrollViewer.ChangeView(scrollViewer.HorizontalOffset + (WindowWidth - 54), scrollViewer.VerticalOffset, null, false);
            else scrollViewer.ChangeView(scrollViewer.HorizontalOffset - (WindowWidth - 54), scrollViewer.VerticalOffset, null, false);
            scrollViewer.HorizontalScrollMode = ScrollMode.Disabled;
        }

        private void ScrollForward(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            ScrollGrid((DependencyObject)this.FindName(btn.Tag.ToString()), true);
        }

        private void ScrollBackward(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            ScrollGrid((DependencyObject)this.FindName(btn.Tag.ToString()), false);
        }

    }
}
