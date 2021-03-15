using System.Collections.Generic;

namespace TMDBFlix.Models
{
    /// <summary>
    /// Contains person data for REST response deserialization
    /// </summary>
    public class Person
    {

        public string credit_id { get; set; }
        public string birthday { get; set; }
        public List<Performance> known_for { get; set; }
        public string known_for_department { get; set; }
        public string deathday { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public List<string> also_known_as { get; set; }
        public int gender { get; set; }
        public string biography { get; set; }
        public float popularity { get; set; }
        public string place_of_birth { get; set; }
        public string profile_path { get; set; }
        public bool adult { get; set; }
        public string imdb_id { get; set; }
        public string homepage { get; set; }
        public int cast_id { get; set; }
        public string character { get; set; }
        public int order { get; set; }
        public string job { get; set; }
        public ExternalIds external_ids { get; set; }
        public MovieCredits movie_credits { get; set; }
        public ShowCredits tv_credits { get; set; }
        public ImagesResponse images { get; set; }
    }
}
