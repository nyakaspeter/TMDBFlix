using System;
using System.Collections.Generic;
using System.Text;

namespace TMDBFlix.Core.Models
{
    public class PeopleResponse
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Person> results { get; set; }
    }
}
