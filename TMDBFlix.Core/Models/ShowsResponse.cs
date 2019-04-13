using System;
using System.Collections.Generic;
using System.Text;

namespace TMDBFlix.Core.Models
{
    public class ShowsResponse
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Show> results { get; set; }
    }
}
