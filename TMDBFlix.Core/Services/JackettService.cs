using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMDBFlix.Core.Models;

namespace TMDBFlix.Core.Services
{
    /// <summary>
    /// Static class for communicating with Jackett API
    /// </summary>
    public static class JackettService
    {
        private static readonly string key = "4ix0cjg1siw9ol050f4a27ktg1vc53ei";
        
        public static List<int> MovieCategories = new List<int>() { 2030, 2040 };
        public static List<int> ShowCategories = new List<int>() { 5030, 5040 };
        public static List<string> Indexers = new List<string>() { "ncore", "1337x" };

        /// <summary>
        /// Gets a list of movie torrents
        /// </summary>
        /// <param name="query">The search query</param>
        /// <returns></returns>
        public static List<Torrent> SearchMovieTorrents(string query, string indexer, List<int> categories)
        {
            var client = new RestClient($"http://127.0.0.1:9117/torznab/{indexer}");
            var request = new RestRequest();
            request.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
            request.RequestFormat = DataFormat.Xml;

            request.AddParameter("apikey", key);
            request.AddParameter("t", "search");
            request.AddParameter("q", query);
            request.AddParameter("cat", String.Join(",", MovieCategories));

            var response = client.Execute<List<Torrent>>(request);
            if (response.IsSuccessful) return response.Data;
            
            return new List<Torrent>();
        }

        public static List<Indexer> GetConfiguredIndexers()
        {
            var client = new RestClient($"http://127.0.0.1:9117/torznab/all");
            var request = new RestRequest();
            request.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
            request.RequestFormat = DataFormat.Xml;

            request.AddParameter("apikey", key);
            request.AddParameter("t", "indexers");
            request.AddParameter("configured", "true");

            var response = client.Execute<List<Indexer>>(request);
            if (response.IsSuccessful) return response.Data;

            return new List<Indexer>();
        }
    }
}