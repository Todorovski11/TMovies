using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMovies.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<MovieActor> Movie { get; set; }
        public ICollection<TvShowActor> TvShow { get; set; }
    }
}
