using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDBFlix.Core.Models;
using TMDBFlix.Helpers;
using TMDBFlix.Services;
using TMDBFlix.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;
using Windows.Foundation.Metadata;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace TMDBFlix.ViewModels
{
    public class ClickableViewModel : Observable
    {
        private ICommand _movieClickCommand;
        public ICommand MovieClickCommand => _movieClickCommand ?? (_movieClickCommand = new RelayCommand<Movie>(OnMovieClick));
        private ICommand _showClickCommand;
        public ICommand ShowClickCommand => _showClickCommand ?? (_showClickCommand = new RelayCommand<Show>(OnShowClick));
        private ICommand _personClickCommand;
        public ICommand PersonClickCommand => _personClickCommand ?? (_personClickCommand = new RelayCommand<Person>(OnPersonClick));
        private ICommand _videoClickCommand;
        public ICommand VideoClickCommand => _videoClickCommand ?? (_videoClickCommand = new RelayCommand<Video>(OnVideoClick));
        private ICommand _imageClickCommand;
        public ICommand ImageClickCommand => _imageClickCommand ?? (_imageClickCommand = new RelayCommand<Image>(OnImageClick));
        private ICommand _genreClickCommand;
        public ICommand GenreClickCommand => _genreClickCommand ?? (_genreClickCommand = new RelayCommand<Genre>(OnGenreClick));
        private ICommand _tvgenreClickCommand;
        public ICommand TvGenreClickCommand => _tvgenreClickCommand ?? (_tvgenreClickCommand = new RelayCommand<Genre>(OnTvGenreClick));
        private ICommand _keywordClickCommand;
        public ICommand KeywordClickCommand => _keywordClickCommand ?? (_keywordClickCommand = new RelayCommand<Keyword>(OnKeywordClick));
        private ICommand _tvkeywordClickCommand;
        public ICommand TvKeywordClickCommand => _tvkeywordClickCommand ?? (_tvkeywordClickCommand = new RelayCommand<Keyword>(OnTvKeywordClick));
        private ICommand _networkClickCommand;
        public ICommand NetworkClickCommand => _networkClickCommand ?? (_networkClickCommand = new RelayCommand<Network>(OnNetworkClick));
        private ICommand _searchItemClickCommand;
        public ICommand SearchItemClickCommand => _searchItemClickCommand ?? (_searchItemClickCommand = new RelayCommand<MultiSearchItem>(OnSearchItemClick));
        private ICommand _torrentDownloadClickCommand;
        public ICommand TorrentDownloadClickCommand => _torrentDownloadClickCommand ?? (_torrentDownloadClickCommand = new RelayCommand<Torrent>(OnTorrentDownloadClick));
        private ICommand _torrentInfoClickCommand;
        public ICommand TorrentInfoClickCommand => _torrentInfoClickCommand ?? (_torrentInfoClickCommand = new RelayCommand<Torrent>(OnTorrentInfoClick));
        private ICommand _torrentStreamClickCommand;
        public ICommand TorrentStreamClickCommand => _torrentStreamClickCommand ?? (_torrentStreamClickCommand = new RelayCommand<Torrent>(OnTorrentStreamClick));
        private ICommand _seasonClickCommand;
        public ICommand SeasonClickCommand => _seasonClickCommand ?? (_seasonClickCommand = new RelayCommand<Season>(OnSeasonClick));

        public void OnMovieClick(Movie clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<MovieDetailPage>(clickedItem.id);
            }
        }

        public void OnShowClick(Show clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ShowDetailPage>(clickedItem.id);
            }
        }

        public void OnSeasonClick(Season clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<SeasonDetailPage>(clickedItem);
            }
        }

        public void OnPersonClick(Person clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<PersonDetailPage>(clickedItem.id);
            }
        }

        public void OnSearchItemClick(MultiSearchItem clickedItem)
        {
            if (clickedItem.media_type.Equals("movie"))
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<MovieDetailPage>(clickedItem.id);
            }
            if (clickedItem.media_type.Equals("tv"))
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ShowDetailPage>(clickedItem.id);
            }
            if (clickedItem.media_type.Equals("person"))
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<PersonDetailPage>(clickedItem.id);
            }
        }

        public void OnVideoClick(Video clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<WebPage>(clickedItem);
            }
        }

        public void OnImageClick(Image clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ImagePage>(clickedItem);
            }
        }

        public void OnGenreClick(Genre clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<MoviesGridPage>(new Dictionary<string, string>(){
                {"listname", clickedItem.name + " " + new ResourceLoader().GetString("Shell_Movies/Content") },
                { "path", "/discover/movie"},
                {"with_genres",clickedItem.id.ToString() }
            });
            }
        }

        public void OnTvGenreClick(Genre clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ShowsGridPage>(new Dictionary<string, string>(){
                {"listname", clickedItem.name + " " + new ResourceLoader().GetString("Shell_Shows/Content") },
                { "path", "/discover/tv"},
                {"with_genres",clickedItem.id.ToString() }
            });
            }
        }

        public void OnKeywordClick(Keyword clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<MoviesGridPage>(new Dictionary<string, string>(){
                {"listname", clickedItem.name},
                { "path", "/discover/movie"},
                {"with_keywords",clickedItem.id.ToString() }
            });
            }
        }

        public void OnTvKeywordClick(Keyword clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ShowsGridPage>(new Dictionary<string, string>(){
                {"listname", clickedItem.name},
                { "path", "/discover/tv"},
                {"with_keywords",clickedItem.id.ToString() }
            });
            }
        }

        public void OnNetworkClick(Network clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<ShowsGridPage>(new Dictionary<string, string>(){
                {"listname", clickedItem.name + " " + new ResourceLoader().GetString("Shell_Shows/Content") },
                { "path", "/discover/tv"},
                {"with_networks",clickedItem.id.ToString() }
            });
            }
        }

        public void OnTorrentInfoClick(Torrent clickedItem)
        {
            
            Windows.System.Launcher.LaunchUriAsync(new Uri(clickedItem.Comments));
        }

        public async void OnTorrentStreamClick(Torrent clickedItem)
        {
            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                ApplicationData.Current.LocalSettings.Values["mode"] = "filelist";

                if (clickedItem.Link.StartsWith("magnet:"))
                {
                    ApplicationData.Current.LocalSettings.Values["link"] = clickedItem.Link;
                    await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync("Parameters");
                    StartStreaming(clickedItem.Title);
                }
                else
                {
                    Uri address = new Uri(clickedItem.Link, UriKind.Absolute);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
                    WebResponse response;
                    try
                    {
                        response = await request.GetResponseAsync();

                        Stream stream = response.GetResponseStream();
                        var torrentsfolder = await ApplicationData.Current.TemporaryFolder.CreateFolderAsync("Torrents", CreationCollisionOption.OpenIfExists);
                        StorageFile torrentfile = await torrentsfolder.CreateFileAsync(String.Join("", clickedItem.Title.Split(Path.GetInvalidFileNameChars())) + ".torrent", CreationCollisionOption.ReplaceExisting);
                        await Windows.Storage.FileIO.WriteBytesAsync(torrentfile, ReadStream(stream));

                        ApplicationData.Current.LocalSettings.Values["link"] = torrentfile.Path;
                        await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync("Parameters");
                        StartStreaming(clickedItem.Title);
                    }
                    catch (WebException e)
                    {
                        if (e.Message.Contains("302"))
                        {
                            var link = e.Response.Headers["Location"];
                            if (link.StartsWith("magnet:"))
                            {
                                ApplicationData.Current.LocalSettings.Values["link"] = link;
                                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync("Parameters");
                                StartStreaming(clickedItem.Title);
                            }
                        }
                    }

                }
            }
        }

        public async void StartStreaming(string title)
        {
            List<string> files = new List<string>();
            string line;

            System.IO.StreamReader file = new StreamReader(ApplicationData.Current.TemporaryFolder.Path + "\\filelist.txt");
            while ((line = file.ReadLine()) != null)
            {
                files.Add(line.Split("35m")[1].Split('\u001b')[0]);
            }
            file.Close();
            ApplicationData.Current.LocalSettings.Values["mode"] = "download";
            FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync("Parameters");
            Thread.Sleep(5000);

            var downloads = await ApplicationData.Current.TemporaryFolder.GetFolderAsync("Downloads");
            var folder = await downloads.GetFolderAsync(title);
            var vid = await folder.GetFileAsync(files[0]);
            Windows.System.Launcher.LaunchFileAsync(vid);
        }

        public async void OnTorrentDownloadClick(Torrent clickedItem)
        {
           
            if (clickedItem.Link.StartsWith("magnet:"))
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri(clickedItem.Link));
            }
            else
            {
                Uri address = new Uri(clickedItem.Link, UriKind.Absolute);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
                WebResponse response;
                try
                {
                    response = await request.GetResponseAsync();

                    Stream stream = response.GetResponseStream();
                    var torrentsfolder = await ApplicationData.Current.TemporaryFolder.CreateFolderAsync("Torrents", CreationCollisionOption.OpenIfExists);
                    StorageFile torrentfile = await torrentsfolder.CreateFileAsync(clickedItem.Title + ".torrent", CreationCollisionOption.ReplaceExisting);
                    await Windows.Storage.FileIO.WriteBytesAsync(torrentfile, ReadStream(stream));
                    await Windows.System.Launcher.LaunchFileAsync(torrentfile);
                }
                catch (WebException e) {
                    if (e.Message.Contains("302"))
                    {
                        var link = e.Response.Headers["Location"];
                        if (link.StartsWith("magnet:")) await Windows.System.Launcher.LaunchUriAsync(new Uri(link));
                    }
                }
                
            }
            
        }

        private byte[] ReadStream(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }

        }

    }
}
