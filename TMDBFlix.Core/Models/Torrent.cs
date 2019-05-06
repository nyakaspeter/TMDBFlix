using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TMDBFlix.Core.Models
{

    public class Enclosure
    {
        public string Url { get; set; }
        public string Length { get; set; }
        public string Type { get; set; }
    }

    [DeserializeAs(Name = "attr")]
    public class Attr
    {
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }
        [DeserializeAs(Name = "value")]
        public string Value { get; set; }
    }

    [DeserializeAs(Name = "item")]
    public class Torrent
    {
        public string Title { get; set; }
        public string Guid { get; set; }
        public string Jackettindexer { get; set; }
        public string Comments { get; set; }
        public string PubDate { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public Enclosure Enclosure { get; set; }
        public List<Attr> Attributes { get; set; }
    }
}
