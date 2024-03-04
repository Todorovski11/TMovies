using System.Collections.Generic;

namespace TMovies.Models.ViewModels
{
    public class IndexViewModel
    {
        public List<Movie> Movie { get; set; }
        public List<TvShow> TvShow { get; set; }
    }
}