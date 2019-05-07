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
        private static readonly string key = "379uar6t4vk5hgblx3czb3cpb28xx0kg";
        private static RestClient client = new RestClient("http://127.0.0.1:9117/torznab/all");
        public static List<int> MovieCategories = new List<int>() { 2030, 2040, 2070 };
        public static List<int> ShowCategories = new List<int>() { 5030, 5040 };

        /// <summary>
        /// Gets a list of movie torrents
        /// </summary>
        /// <param name="query">The search query</param>
        /// <returns></returns>
        public static List<Torrent> SearchMovieTorrents(string query)
        {
            var request = new RestRequest();
            request.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
            request.RequestFormat = DataFormat.Xml;

            request.AddParameter("apikey", key);
            request.AddParameter("t", "search");
            request.AddParameter("q", query);
            request.AddParameter("cat", String.Join(",", MovieCategories));

            var response = client.Execute<List<Torrent>>(request);
            if (response.IsSuccessful) return response.Data;
            else return new List<Torrent>();
        }
    }
}