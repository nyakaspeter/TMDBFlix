using System.Collections.Generic;

namespace TMDBFlix.Models
{
    /// <summary>
    /// Contains show data for REST response deserialization
    /// </summary>
    public class Show
    {
        public string backdrop_path { get; set; }
        public List<Person> created_by { get; set; }
        public List<int> episode_run_time { get; set; }
        public string first_air_date { get; set; }
        public List<Genre> genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public bool in_production { get; set; }
        public List<string> languages { get; set; }
        public string last_air_date { get; set; }
        public Episode last_episode_to_air { get; set; }
        public string name { get; set; }
        public Episode next_episode_to_air { get; set; }
        public List<Network> networks { get; set; }
        public int number_of_episodes { get; set; }
        public int number_of_seasons { get; set; }
        public List<string> origin_country { get; set; }
        public string original_language { get; set; }
        public string original_name { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public List<Company> production_companies { get; set; }
        public List<Season> seasons { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public TitlesResponse alternative_titles { get; set; }
        public ContentRatingsResponse content_ratings { get; set; }
        public Credits credits { get; set; }
        public ExternalIds external_ids { get; set; }
        public KeywordsResponse keywords { get; set; }
    }

    public class Episode
    {
        public string air_date { get; set; }
        public int episode_number { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public string production_code { get; set; }
        public int season_number { get; set; }
        public int show_id { get; set; }
        public string still_path { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public List<Person> crew { get; set; }
        public List<Person> guest_stars { get; set; }
        public ExternalIds external_ids { get; set; }

    }

    public class Season
    {
        public string air_date { get; set; }
        public int episode_count { get; set; }
        public int id { get; set; }
        public string _id { get; set; }
        public int showid { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
        public int season_number { get; set; }
        public List<Episode> episodes { get; set; }
        public Credits credits { get; set; }
        public ExternalIds external_ids { get; set; }
        public int vote_count { get; set; }
        public float vote_average { get; set; }

    }
}
