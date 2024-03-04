namespace TMovies.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; } // Navigation property to access the related Movie entity
        public int ActorId { get; set; }
        public Actor Actor { get; set; } // Navigation property to access the related Actor entity
    }
}