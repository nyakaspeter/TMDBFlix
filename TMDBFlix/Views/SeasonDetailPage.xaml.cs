using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.UI.Animations;
using TMDBFlix.Controls;
using TMDBFlix.Core.Models;
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
    public sealed partial class SeasonDetailPage : CustomPage
    {
        public SeasonDetailViewModel ViewModel { get; } = new SeasonDetailViewModel();

        public SeasonDetailPage()
        {
            InitializeComponent();

            ViewModel.LoadCompleted += ViewModel_LoadCompleted;

            pivot.SelectionChanged += Pivot_SelectionChanged;

            FadeOutContent.Begin();
        }

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pivot.SelectedItem == torrentspivot)
            {
                TorrentsGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                NoTorrentResults.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                TorrentsLoadRing.IsActive = true;
                ViewModel.Torrents.Clear();

                var torrents = new List<Torrent>();
                foreach (var indexer in JackettService.Indexers)
                {
                    torrents.AddRange(await Task.Run(() => JackettService.SearchTorrents(ViewModel.Show.name + " S" + ViewModel.Season.season_number.ToString("00"), indexer, JackettService.TVCategories)));
                    torrents.AddRange(await Task.Run(() => JackettService.SearchTorrents(ViewModel.Show.original_name + " S" + ViewModel.Season.season_number.ToString("00"), indexer, JackettService.TVCategories)));
                    torrents.AddRange(await Task.Run(() => JackettService.SearchTorrents(ViewModel.Show.name + " season " + ViewModel.Season.season_number.ToString(), indexer, JackettService.TVCategories)));
                    torrents.AddRange(await Task.Run(() => JackettService.SearchTorrents(ViewModel.Show.original_name + " season " + ViewModel.Season.season_number.ToString(), indexer, JackettService.TVCategories)));
                }

                TorrentsLoadRing.IsActive = false;
                if (torrents.Count == 0) NoTorrentResults.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else TorrentsGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;

                torrents = torrents.OrderByDescending(x => int.Parse(x.Attributes.Where(y => y.Name.Equals("seeders")).ToList()[0].Value)).GroupBy(z => z.Title).Select(w => w.First()).ToList();
                torrents.ForEach(x => ViewModel.Torrents.Add(x));
            }
        }

        private void ViewModel_LoadCompleted()
        {
            var iso = TMDBService.region;
            foreach (var v in ViewModel.Show.content_ratings.results)
            {
                if (v.iso_3166_1.Equals(iso))
                {
                    certification.Text = v.rating;
                    break;
                }
            }

            if (ViewModel.Show.episode_run_time.Count != 0) runtime.Text = ViewModel.Show.episode_run_time[0].ToString() + " " + new ResourceLoader().GetString("Minute");
            episodes.Text = ViewModel.Season.episodes.Count + " " + new ResourceLoader().GetString("Episodenumber");

            ViewModel.Season.episodes.ForEach(x => ViewModel.Season.vote_count += x.vote_count);
            ViewModel.Season.episodes.ForEach(x => ViewModel.Season.vote_average += x.vote_average);
            var notrated = 0;
            ViewModel.Season.episodes.ForEach(x => { if (x.vote_count == 0) notrated++; });

            ViewModel.Season.vote_average /= (ViewModel.Season.episodes.Count - notrated);

            var vote = Math.Round((double)ViewModel.Season.vote_average / 2, 2);
            voteaverage.PlaceholderValue = vote;
            votecount.Text = "(" + ViewModel.Season.vote_count + ")";

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

            if (ViewModel.Season.air_date != null) firstairdate.Text = DateTime.Parse(ViewModel.Season.air_date).ToString("yyyy. MM. dd.");
            if (ViewModel.Season.episodes[ViewModel.Season.episodes.Count - 1].air_date != null) lastairdate.Text = DateTime.Parse(ViewModel.Season.episodes[ViewModel.Season.episodes.Count - 1].air_date).ToString("yyyy. MM. dd.");

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

            if (ViewModel.Season.overview is null || ViewModel.Show.overview.Equals("")) overview.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Show.homepage is null || ViewModel.Show.homepage.Equals("")) homepageicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Show.external_ids.imdb_id is null || ViewModel.Show.external_ids.imdb_id.Equals("")) imdbicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Show.external_ids.facebook_id is null || ViewModel.Show.external_ids.facebook_id.Equals("")) facebookicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Show.external_ids.twitter_id is null || ViewModel.Show.external_ids.twitter_id.Equals("")) twittericon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Show.external_ids.instagram_id is null || ViewModel.Show.external_ids.instagram_id.Equals("")) instagramicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (ViewModel.Season.episodes.Count == 0) pivot.Items.Remove(episodespivot);
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
            if (e.Parameter is Season season)
            {
                ViewModel.Id = season.showid;
                ViewModel.SeasonNumber = season.season_number;
                ViewModel.LoadData();
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.Season);
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
            contentdialogtext.Text = ViewModel.Season.overview;
            contentdialog.Title = new ResourceLoader().GetString("Overview/Text");
            contentdialog.ShowAsync();
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
    }
}
