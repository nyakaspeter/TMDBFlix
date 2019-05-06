using System.Collections.Generic;

namespace TMDBFlix.Core.Models
{
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

    public class Network
    {
        public string name { get; set; }
        public int id { get; set; }
        public string logo_path { get; set; }
        public string origin_country { get; set; }
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

    public class Performance
    {
        public string name { get; set; }
        public string title { get; set; }
        public string media_type { get; set; }
        public int id { get; set; }
    }

    public class Credits
    {
        public List<Person> cast { get; set; }
        public List<Person> crew { get; set; }
    }

    public class ExternalIds
    {
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string facebook_id { get; set; }
        public string instagram_id { get; set; }
        public string twitter_id { get; set; }
    }

    public class Title
    {
        public string iso_3166_1 { get; set; }
        public string title { get; set; }
        public string type { get; set; }
    }

    public class Keyword
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Video
    {
        public string id { get; set; }
        public string iso_639_1 { get; set; }
        public string iso_3166_1 { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string site { get; set; }
        public int size { get; set; }
        public string type { get; set; }
    }

    public class Media
    {
        public string title { get; set; }
        public string name { get; set; }
        public string release_date { get; set; }
    }

    public class Image
    {
        public float aspect_ratio { get; set; }
        public string file_path { get; set; }
        public int height { get; set; }
        public object iso_639_1 { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public int width { get; set; }
        public string image_type { get; set; }
        public string media_type { get; set; }
        public Media media { get; set; }
    }

    public class CollectionInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
    }

    public class Collection
    {
            public int id { get; set; }
            public string name { get; set; }
            public string overview { get; set; }
            public string poster_path { get; set; }
            public string backdrop_path { get; set; }
            public List<Movie> parts { get; set; }
    }

    public class ReleaseDate
    {
        public string certification { get; set; }
        public string iso_639_1 { get; set; }
        public string note { get; set; }
        public string release_date { get; set; }
        public int type { get; set; }
    }

    public class Release
    {
        public string iso_3166_1 { get; set; }
        public List<ReleaseDate> release_dates { get; set; }
    }

    public class MovieCredit : Movie
    {
        public string character { get; set; }
        public string credit_id { get; set; }
        public string department { get; set; }
        public string job { get; set; }
    }

    public class ShowCredit : Show
    {
        public string character { get; set; }
        public string credit_id { get; set; }
        public string department { get; set; }
        public string job { get; set; }
    }

    public class MovieCredits
    {
        public List<MovieCredit> cast { get; set; }
        public List<MovieCredit> crew { get; set; }
    }

    public class ShowCredits
    {
        public List<ShowCredit> cast { get; set; }
        public List<ShowCredit> crew { get; set; }
    }

    public class ContentRating
    {
        public string iso_3166_1 { get; set; }
        public string rating { get; set; }
    }
}
