using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TMovies.Data;
using TMovies.Models;
using TMovies.Repository.Abstract;

namespace TMovies.Controllers
{
    public class TvShowController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IActorsRepository _actorsRepository;
        private readonly ApplicationDbContext _db;
        private readonly ITvShowRepository _tvShowRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public TvShowController(ILogger<HomeController> logger, IActorsRepository actorsRepository, ApplicationDbContext db, ITvShowRepository tvShowRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _actorsRepository = actorsRepository;
            _db = db;
            _tvShowRepository = tvShowRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> TvShows()
        {
            var model = await _tvShowRepository.GetAll();
            return View(model);
        }
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> CreateTvShow(TvShow tvshow, List<int> SelectedActors)
        {
            tvshow.Actors = new List<TvShowActor>();
            if (ModelState.IsValid)
            {
                try
                {
                    await _tvShowRepository.Create(tvshow);

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

                    return RedirectToAction("TvShows");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the movie.");
                    return View(tvshow);
                }
            }
            return View(tvshow);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditTvShow(int id)
        {
            var tvShow = await _tvShowRepository.GetById(id);
            if (tvShow == null)
            {
                return NotFound();
            }

            var actors = await _actorsRepository.GetAll();
            var actorsList = actors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name,
                Selected = tvShow.Actors.Any(actor => actor.ActorId == a.Id)
            }).ToList();

            ViewBag.ActorsList = actorsList;
            var formattedDate = tvShow.Year.ToString("yyyy-MM-dd");
            ViewBag.FormattedDate = formattedDate;

            return View(tvShow);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteTvShow(int id)
        {
            await _tvShowRepository.Delete(id);
            return Ok(new { success = true });
        }
    }
}
