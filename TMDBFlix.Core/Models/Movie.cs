using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TMDBFlix.Core.Models
{
    /// <summary>
    /// Contains movie data for REST response deserialization
    /// </summary>
    public class Movie
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public CollectionInfo belongs_to_collection { get; set; }
        public long budget { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public long revenue { get; set; }
        public int runtime { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public List<int> genre_ids { get; set; }
        public List<Genre> genres { get; set; }
        public List<Company> production_companies { get; set; }
        public List<Country> production_countries { get; set; }
        public List<Language> spoken_languages { get; set; }
        public Credits credits { get; set; }
        public ExternalIds external_ids { get; set; }
        public TitlesResponse alternative_titles { get; set; }
        public KeywordsResponse keywords { get; set; }
        public ReleaseResponse release_dates { get; set; }
    }

}
