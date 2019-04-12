using System.Collections.Generic;

namespace TMDBFlix.Core.Models
{
    public class Movie
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public object belongs_to_collection { get; set; }
        public int budget { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
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
    }

    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
        public override string ToString()
        {
            return name;
        }
    }

    public class Company
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
        public override string ToString()
        {
            return name;
        }
    }

    public class Country
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
        public override string ToString()
        {
            return name;
        }
    }

    public class Language
    {
            public string iso_639_1 { get; set; }
            public string name { get; set; }
        public override string ToString()
        {
            return name;
        }
    }

}
