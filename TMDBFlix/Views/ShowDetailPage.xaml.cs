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
    public sealed partial class ShowDetailPage : CustomPage
    {
        public ShowDetailViewModel ViewModel { get; } = new ShowDetailViewModel();

        public ShowDetailPage()
        {
            InitializeComponent();

            ViewModel.LoadCompleted += ViewModel_LoadCompleted;

            FadeOutContent.Begin();
        }

        private void ViewModel_LoadCompleted()
        {

            if(ViewModel.Show.first_air_date != null)
            {
                firstairdate.Text = DateTime.Parse(ViewModel.Show.first_air_date).ToString("yyyy. MM. dd.");
                year.Text = ViewModel.Show.first_air_date.Split('-')[0];
                if (ViewModel.Show.last_air_date != null)
                {
                    lastairdate.Text = DateTime.Parse(ViewModel.Show.last_air_date).ToString("yyyy. MM. dd.");
                    if (!ViewModel.Show.in_production)
                        year.Text += "-" + ViewModel.Show.last_air_date.Split('-')[0];
                }
                
            }

            if(ViewModel.Show.episode_run_time.Count != 0) runtime.Text = ViewModel.Show.episode_run_time[0].ToString() + " " + new ResourceLoader().GetString("Minute");
            seasons.Text = ViewModel.Show.number_of_seasons + " " + new ResourceLoader().GetString("Seasonnumber");
            episodes.Text = ViewModel.Show.number_of_episodes + " " + new ResourceLoader().GetString("Episodenumber");

            var iso = TMDBService.region;
            foreach (var v in ViewModel.Show.content_ratings.results)
            {
                if (v.iso_3166_1.Equals(iso))
                {
                    certification.Text = v.rating;
                    break;
                }
            }

            var vote = Math.Round((double)ViewModel.Show.vote_average / 2, 2);
            voteaverage.PlaceholderValue = vote;
            votecount.Text = "(" + ViewModel.Show.vote_count + ")";

            creator.ItemTemplateSelector = new MyTemplateSelector()
            {
                CommonTemplate = CommonDataTemplate,
                LastTemplate = LastDataTemplate
            };

            network.ItemTemplateSelector = new MyTemplateSelector()
            {
                CommonTemplate = CommonNetworkDataTemplate,
                LastTemplate = LastNetworkDataTemplate
            };

            var prodcompanies = "";
            foreach (var v in ViewModel.Show.production_companies)
            {
                if (!v.name.Equals("")) prodcompanies += v.name + ", ";
            }
            if (!prodcompanies.Equals("")) companies.Text = prodcompanies.Substring(0, prodcompanies.Length - 2);

            var origincountries = "";
            foreach (var v in ViewModel.Show.origin_country)
            {
                origincountries += v + ", ";
            }
            if (!origincountries.Equals("")) countries.Text = origincountries.Substring(0, origincountries.Length - 2);

            if (ViewModel.Show.overview is null || ViewModel.Show.overview.Equals("")) overview.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Show.homepage is null || ViewModel.Show.homepage.Equals("")) homepageicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Show.external_ids.imdb_id is null || ViewModel.Show.external_ids.imdb_id.Equals("")) imdbicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Show.external_ids.facebook_id is null || ViewModel.Show.external_ids.facebook_id.Equals("")) facebookicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Show.external_ids.twitter_id is null || ViewModel.Show.external_ids.twitter_id.Equals("")) twittericon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Show.external_ids.instagram_id is null || ViewModel.Show.external_ids.instagram_id.Equals("")) instagramicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (ViewModel.Show.seasons.Count == 0) pivot.Items.Remove(seasonspivot);
            if (ViewModel.Cast.Count == 0) pivot.Items.Remove(castpivot);
            if (ViewModel.Crew.Count == 0) pivot.Items.Remove(crewpivot);
            if (ViewModel.Videos.Count == 0) pivot.Items.Remove(videospivot);
            if (ViewModel.Backdrops.Count == 0) pivot.Items.Remove(backdropspivot);
            if (ViewModel.Recommendations.Count == 0) pivot.Items.Remove(recommendationspivot);
            if (ViewModel.Similar.Count == 0) pivot.Items.Remove(similarpivot);
            if (ViewModel.Show.keywords.results.Count == 0) keywordsinfo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

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
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.Show);
            }
        }

        private void ImdbIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://imdb.com/title/" + ViewModel.Show.external_ids.imdb_id));
        }

        private void FacebookIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.facebook.com/" + ViewModel.Show.external_ids.facebook_id));
        }

        private void InstagramIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.instagram.com/" + ViewModel.Show.external_ids.instagram_id));
        }

        private void TwitterIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://twitter.com/" + ViewModel.Show.external_ids.twitter_id));
        }

        private void HomepageIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(ViewModel.Show.homepage));
        }

        private void Title_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentdialogtext.Text = "";
            foreach (var v in ViewModel.Show.alternative_titles.results)
            {
                contentdialogtext.Text += v.title + "\n";
            }
            contentdialog.Title = new ResourceLoader().GetString("AlternativeTitles/Text");
            contentdialog.ShowAsync();
        }

        private void Overview_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentdialogtext.Text = ViewModel.Show.overview;
            contentdialog.Title = new ResourceLoader().GetString("Overview/Text");
            contentdialog.ShowAsync();
        }

        private void Languages_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }

        private void Companies_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentdialogtext.Text = "";
            foreach (var v in ViewModel.Show.production_companies)
            {
                contentdialogtext.Text += v.name + "\n";
            }
            contentdialog.Title = new ResourceLoader().GetString("Companies/Text");
            contentdialog.ShowAsync();
        }

        private void Countries_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }

        private void Collection_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            /*
            NavigationService.Navigate<MoviesGridPage>(new Dictionary<string, string>(){
                {"listname", ViewModel.Movie.belongs_to_collection.name },
                { "path", "/discover/movie"},
                {"with_genres",clickedItem.id.ToString() }
            */
        }
    }
}
