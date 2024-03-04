using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using TMovies.Data;
using TMovies.Models;
using TMovies.Models.ViewModels;
using TMovies.Repository.Abstract;
using TMovies.Repository.Concrete;

namespace TMovies.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly IActorsRepository _actorsRepository;
        private readonly ApplicationDbContext _db;
        private readonly ITvShowRepository _tvShowRepository;

        public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepository, IActorsRepository actorsRepository, ApplicationDbContext db, ITvShowRepository tvShowRepository)
        {
            _logger = logger;
            _movieRepository = movieRepository;
            _actorsRepository = actorsRepository;
            _db = db;
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
        public async Task<IActionResult> CreateMovie()
        {
            var actors = await _actorsRepository.GetAll();

            var actorsList = actors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            ViewBag.ActorsList = actorsList;

            var model = new Movie();

            return View(model);
        }

        public async Task<IActionResult> CreateTvShow()
        {
            var actors = await _actorsRepository.GetAll();

            var actorsList = actors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();
            ViewBag.ActorsList = actorsList;

            var model = new TvShow();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateMovie(Movie movie, List<int> SelectedActors)
        {
            movie.Actors = new List<MovieActor>();
            if (ModelState.IsValid)
            {
                try
                {
                    // Add the movie to the database
                    await _movieRepository.Create(movie);

                    // Create MovieActor instances for each selected actor
                    if (SelectedActors != null && SelectedActors.Any())
                    {
                        foreach (var actorId in SelectedActors)
                        {
                            var movieActor = new MovieActor
                            {
                                MovieId = movie.Id,
                                ActorId = actorId,
                            };
                            _db.MovieActors.Add(movieActor);
                        }
                        await _db.SaveChangesAsync();
                    }

                    return RedirectToAction("Movies", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the movie.");
                    // Log the exception
                    return View(movie);
                }
            }
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTvShow(TvShow tvshow, List<int> SelectedActors)
        {
            tvshow.Actors = new List<TvShowActor>();
            if (ModelState.IsValid)
            {
                try
                {
                    // Add the movie to the database
                    await _tvShowRepository.Create(tvshow);


                    // Create MovieActor instances for each selected actor
                    if (SelectedActors != null && SelectedActors.Any())
                    {
                        foreach (var actorId in SelectedActors)
                        {
                            var tvShowActor = new TvShowActor
                            {
                                TvShowId = tvshow.Id,
                                ActorId = actorId,
                            };
                            _db.TvShowActors.Add(tvShowActor);
                        }
                        await _db.SaveChangesAsync();
                    }

                    return RedirectToAction("TvShows", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the movie.");
                    // Log the exception
                    return View(tvshow);
                }
            }
            return View(tvshow);
        }



        [HttpGet]
        public async Task<IActionResult> Movies()
        {
            var model = await _movieRepository.GetAll(); // Assuming GetAll() returns Task<List<Movie>>
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> TvShows()
        {
            var model = await _tvShowRepository.GetAll(); // Assuming GetAll() returns Task<List<Movie>>
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditTvShow(int id)
        {
            var tvShow = await _tvShowRepository.GetById(id);
            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTvShow(int id, TvShow updatedTvShow)
        {
            if (id != updatedTvShow.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(updatedTvShow);
            }

            try
            {
                await _tvShowRepository.Update(updatedTvShow);
                return RedirectToAction("TvShows"); 
            }
            catch (Exception)
            {
                return View(updatedTvShow);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditMovie(int id)
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovie(int id, Movie updateMovie)
        {
            if (id != updateMovie.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(updateMovie);
            }

            try
            {
                await _movieRepository.Update(updateMovie);
                return RedirectToAction("Movies");
            }
            catch (Exception)
            {
                return View(updateMovie);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTvShow(int id)
        {
            await _tvShowRepository.Delete(id);
            return Ok(new { success = true });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _movieRepository.Delete(id);
            return Ok(new { success = true });
        }


    }
}
