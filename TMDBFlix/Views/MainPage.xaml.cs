using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TMDBFlix.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace TMDBFlix.Views
{
    public sealed partial class MainPage : Page
    {




        public MainPage()
        {


            this.InitializeComponent();

        }

        public MainViewModel ViewModel { get; } = new MainViewModel();
    }
 

}

