using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TMDBFlix.Core.Models
{

    public class Movie
    {
        [JsonProperty(PropertyName = "poster_path")]
        public string Poster { get; }

        [JsonProperty(PropertyName = "adult")]
        public bool Adult { get; }

        [JsonProperty(PropertyName = "overview")]
        public string Overview { get; }

        [JsonProperty(PropertyName = "release_date")]
        public string ReleaseDate { get; }

        [JsonProperty(PropertyName = "genre_ids")]
        public List<int> Genres { get; }

        [JsonProperty(PropertyName = "id")]
        public int ID { get; }

        [JsonProperty(PropertyName = "original_title")]
        public string OriginalTitle { get; }

        [JsonProperty(PropertyName = "original_language")]
        public string OriginalLanguage { get; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; }

        [JsonProperty(PropertyName = "backdrop_path")]
        public string Backdrop { get; }

        [JsonProperty(PropertyName = "popularity")]
        public double Popularity { get; }

        [JsonProperty(PropertyName = "vote_count")]
        public int Votes { get; }

        [JsonProperty(PropertyName = "video")]
        public bool Video { get; }

        [JsonProperty(PropertyName = "vote_average")]
        public string Rating { get; }
    }
}
