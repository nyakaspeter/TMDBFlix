using System;

using Microsoft.Toolkit.Uwp.UI.Animations;
using TMDBFlix.Controls;
using TMDBFlix.Core.Services;
using TMDBFlix.Helpers;
using TMDBFlix.Services;
using TMDBFlix.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace TMDBFlix.Views
{
    public sealed partial class PersonDetailPage : CustomPage
    {
        public PersonDetailViewModel ViewModel { get; } = new PersonDetailViewModel();

        public PersonDetailPage()
        {
            InitializeComponent();

            ViewModel.LoadCompleted += ViewModel_LoadCompleted;

            FadeOutContent.Begin();
        }

        private void ViewModel_LoadCompleted()
        {

            if(ViewModel.Person.known_for_department != null)department.Text = ViewModel.Person.known_for_department;

            if (ViewModel.Person.gender == 1) gender.Text = new ResourceLoader().GetString("Female");
            else if (ViewModel.Person.gender == 2) gender.Text = new ResourceLoader().GetString("Male");

            if (ViewModel.Person.birthday != null)
            {
                birthday.Text = DateTime.Parse(ViewModel.Person.birthday).ToString("yyyy. MM. dd.");
                age.Text = Math.Floor((DateTime.Now - DateTime.Parse(ViewModel.Person.birthday)).Days / 365.2425) + " " + new ResourceLoader().GetString("Years");
                
            }

            if (ViewModel.Person.deathday != null)
            {
                deathday.Text = DateTime.Parse(ViewModel.Person.deathday).ToString("yyyy. MM. dd.");
                ageinfo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                deathdayinfo.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

            if (ViewModel.Person.place_of_birth != null) birthplace.Text = ViewModel.Person.place_of_birth;

            if (ViewModel.Person.biography is null || ViewModel.Person.biography.Equals("")) biography.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Person.homepage is null || ViewModel.Person.homepage.Equals("")) homepageicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Person.imdb_id is null || ViewModel.Person.imdb_id.Equals("")) imdbicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Person.external_ids.facebook_id is null || ViewModel.Person.external_ids.facebook_id.Equals("")) facebookicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Person.external_ids.twitter_id is null || ViewModel.Person.external_ids.twitter_id.Equals("")) twittericon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Person.external_ids.instagram_id is null || ViewModel.Person.external_ids.instagram_id.Equals("")) instagramicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (ViewModel.Movies.Count == 0) pivot.Items.Remove(moviespivot);
            if (ViewModel.Shows.Count == 0) pivot.Items.Remove(showspivot);
            if (ViewModel.Images.Count == 0) pivot.Items.Remove(imagespivot);

            LoadRing.IsActive = false;
            FadeInContent.Begin();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is int Id)
            {
                ViewModel.Id = Id;
                ViewModel.LoadData();
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.Person);
            }
        }

        private void ImdbIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://imdb.com/name/" + ViewModel.Person.imdb_id));
        }

        private void FacebookIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.facebook.com/" + ViewModel.Person.external_ids.facebook_id));
        }

        private void InstagramIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.instagram.com/" + ViewModel.Person.external_ids.instagram_id));
        }

        private void TwitterIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://twitter.com/" + ViewModel.Person.external_ids.twitter_id));
        }

        private void HomepageIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(ViewModel.Person.homepage));
        }

        private void Title_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentdialogtext.Text = "";
            foreach (var v in ViewModel.Person.also_known_as)
            {
                contentdialogtext.Text += v + "\n";
            }
            contentdialog.Title = new ResourceLoader().GetString("AlsoKnownAs/Text");
            contentdialog.ShowAsync();
        }

        private void Overview_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentdialogtext.Text = ViewModel.Person.biography;
            contentdialog.Title = new ResourceLoader().GetString("Biography/Text");
            contentdialog.ShowAsync();
        }

        private void Languages_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }

        private void Companies_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            
        }

        private void Countries_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            
        }

        private void Collection_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }
    }
}
