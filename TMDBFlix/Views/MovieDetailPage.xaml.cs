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
    public sealed partial class MovieDetailPage : CustomPage
    {
        public MovieDetailViewModel ViewModel { get; } = new MovieDetailViewModel();

        public MovieDetailPage()
        {
            InitializeComponent();

            ViewModel.LoadCompleted += ViewModel_LoadCompleted;

            pivot.SelectionChanged += Pivot_SelectionChanged;

            FadeOutContent.Begin();
        }

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pivot.SelectedItem == torrentspivot)
            {
                TorrentsGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                NoTorrentResults.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                TorrentsLoadRing.IsActive = true;
                ViewModel.Torrents.Clear();

                var torrents = new List<Torrent>();
                foreach (var indexer in JackettService.Indexers)
                {
                    torrents.AddRange(await Task.Run(() => JackettService.SearchTorrents(ViewModel.Movie.title, indexer, JackettService.MovieCategories)));
                    torrents.AddRange(await Task.Run(() => JackettService.SearchTorrents(ViewModel.Movie.original_title, indexer, JackettService.MovieCategories)));
                }

                TorrentsLoadRing.IsActive = false;
                if (torrents.Count == 0) NoTorrentResults.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else TorrentsGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                
                torrents = torrents.OrderByDescending(x => int.Parse(x.Attributes.Where(y => y.Name.Equals("seeders")).ToList()[0].Value)).GroupBy(z => z.Title).Select(w => w.First()).ToList();
                var betterresults = torrents.Where(x => x.Title.Contains($"{ViewModel.Movie.release_date.Split('-')[0]}")).ToList();
                betterresults.ForEach(x => torrents.Remove(x));
                torrents.InsertRange(0, betterresults);
                torrents.ForEach(x => ViewModel.Torrents.Add(x));
            }
        }

        private void ViewModel_LoadCompleted()
        {

            year.Text = ViewModel.Movie.release_date.Split('-')[0];

            var iso = TMDBService.region;
            foreach (var v in ViewModel.Movie.release_dates.results)
            {
                if (v.iso_3166_1.Equals(iso))
                {
                    if (!v.release_dates[v.release_dates.Count - 1].certification.Equals(""))
                        certification.Text = v.release_dates[v.release_dates.Count - 1].certification;
                    break;
                }
            }

            if(ViewModel.Movie.runtime != 0)
            {
                if (ViewModel.Movie.runtime > 60)
                {
                    runtime.Text = $"{ViewModel.Movie.runtime / 60} {new ResourceLoader().GetString("Hour")} {ViewModel.Movie.runtime % 60} {new ResourceLoader().GetString("Minute")}";
                }
                else runtime.Text = $"{ViewModel.Movie.runtime} {new ResourceLoader().GetString("Minute")}";
            }

            var vote = Math.Round((double)ViewModel.Movie.vote_average / 2, 2);
            voteaverage.PlaceholderValue = vote;
            votecount.Text = "(" + ViewModel.Movie.vote_count + ")";

            if (!ViewModel.Movie.release_date.Equals(""))
                releasedate.Text = DateTime.Parse(ViewModel.Movie.release_date).ToString("yyyy. MM. dd.");

            director.ItemTemplateSelector = new MyTemplateSelector()
            {
                CommonTemplate = CommonDataTemplate,
                LastTemplate = LastDataTemplate
            };

            if (ViewModel.Movie.belongs_to_collection is null) collectioninfo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            else collection.Text = ViewModel.Movie.belongs_to_collection.name;
            
            var prodcompanies = "";
            foreach (var v in ViewModel.Movie.production_companies)
            {
                if (!v.name.Equals("")) prodcompanies += v.name + ", ";
            }
            if (!prodcompanies.Equals("")) companies.Text = prodcompanies.Substring(0, prodcompanies.Length - 2);
            
            var prodcountries = "";
            foreach (var v in ViewModel.Movie.production_countries)
            {
                if (!v.name.Equals("")) prodcountries += v.name + ", ";
            }
            if (!prodcountries.Equals("")) countries.Text = prodcountries.Substring(0, prodcountries.Length - 2);

            var spokenlangs = "";
            foreach (var v in ViewModel.Movie.spoken_languages)
            {
                if (!v.name.Equals("")) spokenlangs += v.name + ", ";
            }
            if (!spokenlangs.Equals("")) languages.Text = spokenlangs.Substring(0, spokenlangs.Length - 2);

            if (ViewModel.Movie.budget != 0) budget.Text = "$" + ViewModel.Movie.budget.ToString("N0");
            if (ViewModel.Movie.revenue != 0) revenue.Text = "$" + ViewModel.Movie.revenue.ToString("N0");
            if (ViewModel.Movie.tagline is null || ViewModel.Movie.tagline.Equals("")) tagline.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Movie.overview is null || ViewModel.Movie.overview.Equals("")) overview.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Movie.homepage is null || ViewModel.Movie.homepage.Equals("")) homepageicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Movie.imdb_id is null || ViewModel.Movie.imdb_id.Equals("")) imdbicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Movie.external_ids.facebook_id is null || ViewModel.Movie.external_ids.facebook_id.Equals("")) facebookicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Movie.external_ids.twitter_id is null || ViewModel.Movie.external_ids.twitter_id.Equals("")) twittericon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (ViewModel.Movie.external_ids.instagram_id is null || ViewModel.Movie.external_ids.instagram_id.Equals("")) instagramicon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (ViewModel.Cast.Count == 0) pivot.Items.Remove(castpivot);
            if (ViewModel.Crew.Count == 0) pivot.Items.Remove(crewpivot);
            if (ViewModel.Videos.Count == 0) pivot.Items.Remove(videospivot);
            if (ViewModel.Backdrops.Count == 0) pivot.Items.Remove(backdropspivot);
            if (ViewModel.Recommendations.Count == 0) pivot.Items.Remove(recommendationspivot);
            if (ViewModel.Similar.Count == 0) pivot.Items.Remove(similarpivot);
            //if (ViewModel.Torrents.Count == 0) pivot.Items.Remove(torrentspivot);
            if (ViewModel.Movie.keywords.keywords.Count == 0) keywordsinfo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

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
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.Movie);
            }
        }

        private void ImdbIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://imdb.com/title/" + ViewModel.Movie.imdb_id));
        }

        private void FacebookIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.facebook.com/" + ViewModel.Movie.external_ids.facebook_id));
        }

        private void InstagramIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.instagram.com/" + ViewModel.Movie.external_ids.instagram_id));
        }

        private void TwitterIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://twitter.com/" + ViewModel.Movie.external_ids.twitter_id));
        }

        private void HomepageIcon_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(ViewModel.Movie.homepage));
        }

        private void Title_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentdialogtext.Text = "";
            foreach (var v in ViewModel.Movie.alternative_titles.titles)
            {
                contentdialogtext.Text += v.title + "\n";
            }
            contentdialog.Title = new ResourceLoader().GetString("AlternativeTitles/Text");
            contentdialog.ShowAsync();
        }

        private void Overview_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentdialogtext.Text = ViewModel.Movie.tagline + "\n\n" + ViewModel.Movie.overview;
            contentdialog.Title = new ResourceLoader().GetString("Overview/Text");
            contentdialog.ShowAsync();
        }

        private void Languages_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentdialogtext.Text = "";
            foreach (var v in ViewModel.Movie.spoken_languages)
            {
                contentdialogtext.Text += v.name + "\n";
            }
            contentdialog.Title = new ResourceLoader().GetString("Languages/Text");
            contentdialog.ShowAsync();
        }

        private void Companies_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentdialogtext.Text = "";
            foreach (var v in ViewModel.Movie.production_companies)
            {
                contentdialogtext.Text += v.name + "\n";
            }
            contentdialog.Title = new ResourceLoader().GetString("Companies/Text");
            contentdialog.ShowAsync();
        }

        private void Countries_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentdialogtext.Text = "";
            foreach (var v in ViewModel.Movie.production_countries)
            {
                contentdialogtext.Text += v.name + "\n";
            }
            contentdialog.Title = new ResourceLoader().GetString("Countries/Text");
            contentdialog.ShowAsync();
        }

        private void Collection_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            NavigationService.Navigate<CollectionDetailPage>(ViewModel.Movie.belongs_to_collection.id);
        }
    }
}
