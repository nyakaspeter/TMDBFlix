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
    public static class JackettService
    {
        private static readonly string key = "4ix0cjg1siw9ol050f4a27ktg1vc53ei";
        private static RestClient client = new RestClient("http://127.0.0.1:9117/torznab/all");
        public static List<int> MovieCategories = new List<int>() { 2030, 2040, 2070 };
        public static List<int> ShowCategories = new List<int>() { 5030, 5040 };

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
        /*
        public static Show GetShow(int Id)
        {
            var request = new RestRequest($"/tv/{Id}");
            request.AddParameter("api_key", key);
            request.AddParameter("language", language);
            request.AddParameter("append_to_response", "alternative_titles,content_ratings,credits,external_ids,keywords");

            var response = client.Execute<Show>(request);
            if (response.IsSuccessful) return response.Data;
            else if ((int)response.StatusCode == 429)
            {
                while ((int)response.StatusCode == 429)
                {
                    Thread.Sleep(1000);
                    response = client.Execute<Show>(request);
                }
                return response.Data;
            }
            else return new Show();
        }
        */
    }
}