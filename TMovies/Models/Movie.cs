using System.ComponentModel.DataAnnotations;
using TMovies.Models;
using TMovies.Models.Enum;

namespace TMovies.Models
{

    public class Movie
    {
        public Movie()
        {
            Actors = new List<MovieActor>(); // Initialize Actors collection
        }
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Plot { get; set; }
        public string Director { get; set; }
        public string ImgLink { get; set; }
        public string ImgBanner { get; set; }
        public int Duration { get; set; }
        public string IMDbLink { get; set; }    
        public Genre Genre { get; set; }

        // Change ActorsId to ICollection<Actors>
        public ICollection<MovieActor> Actors { get; set; }
    }
}