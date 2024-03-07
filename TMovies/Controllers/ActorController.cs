using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMovies.Data;
using TMovies.Models;
using TMovies.Repository.Abstract;
using TMovies.Repository.Concrete;

namespace TMovies.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorsRepository _actorsRepository;

        public ActorController(IActorsRepository actorsRepository)
        {
            _actorsRepository = actorsRepository;
        }
        [Authorize]
        public async Task<IActionResult> CreateActor()
        {
            var model = new Actor();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateActor(Actor actor)
        {
            ModelState.Remove("Movie");
            ModelState.Remove("TvShow");
            if (ModelState.IsValid)
            {
                await _actorsRepository.Create(actor);
                return RedirectToAction("Index", "Home");
            }
            return View(actor);
        }
    }
}
