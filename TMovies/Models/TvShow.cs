using System.ComponentModel.DataAnnotations;
using TMovies.Models.Enum;

namespace TMovies.Models
{
    public class TvShow
    {
        public TvShow()
        {
            Actors = new List<TvShowActor>(); // Initialize Actors collection
        }
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public DateTime Year { get; set; }
        public string Plot { get; set; }
        public string Director { get; set; }
        public int Seasons { get; set; }
        public int Episodes { get; set; }
        public string ImgLink { get; set; }
        public string ImgBanner { get; set; }
        public string IMDbLink { get; set; }
        public int Duration { get; set; }
        public Genre Genre { get; set; }

        // Change ActorsId to ICollection<Actors>
        public ICollection<TvShowActor> Actors { get; set; }
    }
}