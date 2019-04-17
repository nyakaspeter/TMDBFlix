using System.Collections.Generic;

namespace TMDBFlix.Core.Models
{
    public class MultiSearchItem
    {
            public string poster_path { get; set; }
            public int popularity { get; set; }
            public int id { get; set; }
            public string overview { get; set; }
            public string backdrop_path { get; set; }
            public float vote_average { get; set; }
            public string media_type { get; set; }
            public string first_air_date { get; set; }
            public List<string> origin_country { get; set; }
            public List<int> genre_ids { get; set; }
            public string original_language { get; set; }
            public int vote_count { get; set; }
            public string name { get; set; }
            public string original_name { get; set; }
            public bool adult { get; set; }
            public string release_date { get; set; }
            public string original_title { get; set; }
            public string title { get; set; }
            public bool video { get; set; }
            public string profile_path { get; set; }
            public List<Performance> known_for { get; set; }
    }
}
