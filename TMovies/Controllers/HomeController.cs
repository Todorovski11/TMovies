using Microsoft.AspNetCore.Mvc;
using TMovies.Models.ViewModels;
using TMovies.Repository.Abstract;

namespace TMovies.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ITvShowRepository _tvShowRepository;

        public HomeController(IMovieRepository movieRepository, ITvShowRepository tvShowRepository)
        {
            _movieRepository = movieRepository;
            _tvShowRepository = tvShowRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movieModel = await _movieRepository.GetAll();
            var tvShowModel = await _tvShowRepository.GetAll();
            IndexViewModel viewModel = new IndexViewModel
            {
                Movie = movieModel,
                TvShow = tvShowModel
            };
            return View(viewModel);
        }     
    }
}
