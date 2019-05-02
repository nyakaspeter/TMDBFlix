using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TMDBFlix.Core.Models;
using TMDBFlix.Core.Services;
using TMDBFlix.Helpers;

namespace TMDBFlix.ViewModels
{
    public class PersonDetailViewModel : ClickableViewModel
    {
        private Person person = new Person();

        public Person Person
        {
            get { return person; }
            set { Set(ref person, value); }
        }

        public int Id;
        public ObservableCollection<MovieCredit> Movies = new ObservableCollection<MovieCredit>();
        public ObservableCollection<ShowCredit> Shows = new ObservableCollection<ShowCredit>();
        public ObservableCollection<Image> Images = new ObservableCollection<Image>();

        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        async Task LoadInfo()
        {
            Person = await Task.Run(() => TMDBService.GetPerson(Id));

            var movies = Person.movie_credits.cast;
            movies.AddRange(Person.movie_credits.crew);

            var shows = Person.tv_credits.cast;
            shows.AddRange(Person.tv_credits.crew);

            var distinctMovies = movies.GroupBy(x => x.title).Select(y => y.First()).ToList();
            var distinctShows = shows.GroupBy(x => x.name).Select(y => y.First()).ToList();

            var sortedMovies = distinctMovies.OrderByDescending(v => v.popularity).ToList();
            var sortedShows = distinctShows.OrderByDescending(v => v.popularity).ToList();

            sortedMovies.ImagesFirst().ForEach(v => Movies.Add(v));
            sortedShows.ImagesFirst().ForEach(v => Shows.Add(v));
        }

        async Task LoadImages()
        {
            var images = await Task.Run(() => TMDBService.GetImages($"/person/{Id}/tagged_images"));
            foreach (var v in images.results)
            {
                Images.Add(v);
            }
        }

        public async void LoadData()
        {
            var tasks = new List<Task>()
            {
                LoadInfo(),
                LoadImages()
            };

            await Task.WhenAll(tasks);
            LoadCompleted();
        }

        public PersonDetailViewModel()
        {
        }

    }
}
