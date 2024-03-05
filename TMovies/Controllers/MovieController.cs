using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TMovies.Data;
using TMovies.Models;
using TMovies.Repository.Abstract;

namespace TMovies.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly IActorsRepository _actorsRepository;
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public MovieController(ILogger<HomeController> logger, IMovieRepository movieRepository, IActorsRepository actorsRepository, ApplicationDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _movieRepository = movieRepository;
            _actorsRepository = actorsRepository;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Movies()
        {
            var model = await _movieRepository.GetAll();
            return View(model);
        }
        [Authorize]
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
        [HttpPost]
        [Authorize]
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
        [HttpGet]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _movieRepository.Delete(id);
            return Ok(new { success = true });
        }
    }
}
