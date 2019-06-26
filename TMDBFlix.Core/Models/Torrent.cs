using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

/// <summary>
/// Classes for torrent data deserialization
/// </summary>
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

    public class Indexer : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public List<Category> Categories { get; set; }
        private bool enabled;
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
                this.NotifyPropertyChanged("Enabled");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
    }

    [DeserializeAs(Name = "category")]
    public class Category : INotifyPropertyChanged
    {
        public Category()
        {

        }
        public Category(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        private bool enabled;
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
                this.NotifyPropertyChanged("Enabled");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
    }

}
