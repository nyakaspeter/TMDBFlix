using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMDBFlix.Models;

namespace TMDBFlix.Services
{
    /// <summary>
    /// Static class for communicating with Jackett API
    /// </summary>
    public static class JackettService
    {
        private static string url;
        public static string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["jackett_url"] = value;
            }
        }

        private static string key;
        public static string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["jackett_key"] = value;
            }
        }
        
        private static List<string> indexers;
        public static List<string> Indexers
        {
            get { return indexers; }
            set
            {
                indexers = value;
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["torrent_indexers"] = String.Join(",",indexers);
            }
        }

        private static List<string> moviecategories;
        public static List<string> MovieCategories
        {
            get { return moviecategories; }
            set
            {
                moviecategories = value;
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["torrent_moviecategories"] = String.Join(",",moviecategories);
            }
        }

        private static List<string> tvcategories;
        public static List<string> TVCategories
        {
            get { return tvcategories; }
            set
            {
                tvcategories = value;
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["torrent_tvcategories"] = String.Join(",", tvcategories);
            }
        }

        /// <summary>
        /// Gets a list of torrents
        /// </summary>
        /// <param name="query">The search query</param>
        /// <returns></returns>
        public static List<Torrent> SearchTorrents(string query, string indexer, List<string> categories)
        {
            try
            {
                var normalizedUrl = url;
                if (!normalizedUrl.StartsWith("http")) normalizedUrl = "http://" + normalizedUrl;
                if (normalizedUrl.EndsWith('/')) normalizedUrl = normalizedUrl.Trim('/');

                var client = new RestClient($"{normalizedUrl}/torznab/{indexer}");
                var request = new RestRequest();
                request.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
                request.RequestFormat = DataFormat.Xml;

                request.AddParameter("apikey", key);
                request.AddParameter("t", "search");
                request.AddParameter("q", query);
                request.AddParameter("cat", String.Join(",", categories));

                var response = client.Execute<List<Torrent>>(request);
                if (response.IsSuccessful) return response.Data;
                else return new List<Torrent>();
            }
            catch (Exception)
            {
                return new List<Torrent>();
            }
        }

        public static List<Indexer> GetConfiguredIndexers()
        {
            try
            {
                var normalizedUrl = url;
                if (!normalizedUrl.StartsWith("http")) normalizedUrl = "http://" + normalizedUrl;
                if (normalizedUrl.EndsWith('/')) normalizedUrl = normalizedUrl.Trim('/');

                var client = new RestClient($"{normalizedUrl}/torznab/all");
                var request = new RestRequest();
                request.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
                request.RequestFormat = DataFormat.Xml;

                request.AddParameter("apikey", key);
                request.AddParameter("t", "indexers");
                request.AddParameter("configured", "true");

                var response = client.Execute<List<Indexer>>(request);
                if (response.IsSuccessful) return response.Data;
                else return new List<Indexer>();
                } 
            catch (Exception) 
            {
                return new List<Indexer>();
            }
        }

        public static void Init()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Values["jackett_url"] as string == null) localSettings.Values["jackett_url"] = "";
            if (localSettings.Values["jackett_key"] as string == null) localSettings.Values["jackett_key"] = "";
            if (localSettings.Values["torrent_moviecategories"] as string == null) localSettings.Values["torrent_moviecategories"] = "2000";
            if (localSettings.Values["torrent_tvcategories"] as string == null) localSettings.Values["torrent_tvcategories"] = "5000";
            if (localSettings.Values["torrent_indexers"] as string == null) localSettings.Values["torrent_indexers"] = "";

            url = localSettings.Values["jackett_url"] as string;
            key = localSettings.Values["jackett_key"] as string;

            var indexers_str = localSettings.Values["torrent_indexers"] as string;
            var moviecategories_str = localSettings.Values["torrent_moviecategories"] as string;
            var tvcategories_str = localSettings.Values["torrent_tvcategories"] as string;
            indexers = indexers_str.Split(',').ToList();
            moviecategories = moviecategories_str.Split(',').ToList();
            tvcategories = tvcategories_str.Split(',').ToList();
        }
    }
}
