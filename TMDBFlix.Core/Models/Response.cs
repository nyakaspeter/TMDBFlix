using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Data classes for REST response deserialization
/// </summary>
namespace TMDBFlix.Core.Models
{
    public class MoviesResponse
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Movie> results { get; set; }
    }

    public class MultiSearchResponse
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<MultiSearchItem> results { get; set; }
    }

    public class PeopleResponse
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Person> results { get; set; }
    }

    public class ShowsResponse
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Show> results { get; set; }
    }

    public class TitlesResponse
    {
        public List<Title> titles { get; set; }
        public List<Title> results { get; set; }
    }

    public class KeywordsResponse
    {
        public List<Keyword> keywords { get; set; }
        public List<Keyword> results { get; set; }
    }

    public class VideosResponse
    {
        public List<Video> results { get; set; }
    }

    public class ImagesResponse
    {
        public List<Image> backdrops { get; set; }
        public List<Image> posters { get; set; }
        public List<Image> profiles { get; set; }
        public List<Image> results { get; set; }
    }

    public class ReleaseResponse
    {
        public List<Release> results { get; set; }
    }

    public class ContentRatingsResponse
    {
        public List<ContentRating> results { get; set; }
    }
}
