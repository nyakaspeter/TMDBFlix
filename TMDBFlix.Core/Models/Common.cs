using System;
using System.Collections.Generic;
using System.Text;

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
}
