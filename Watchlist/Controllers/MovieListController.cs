using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Watchlist.Data;
using Watchlist.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Watchlist.Controllers
{
    [Authorize]
    public class MovieListController : Controller
    {
        private readonly ApplicationDbContext _contexte;
        private readonly UserManager<AppUser> _userManager;

        //inject db context in the constructor , don't forget the userManager
        public MovieListController(ApplicationDbContext contexte,UserManager<AppUser> manager)
        {
            _contexte = contexte;
            _userManager = manager;
           
        }
        private Task<AppUser> GetCurrentUserAsync() 
        { 
            return _userManager.GetUserAsync(HttpContext.User); 
        }

        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            AppUser user = await GetCurrentUserAsync();
            return user?.Id;
        }



        public async Task<IActionResult> Index()
        {
            var id = await GetCurrentUserId();

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }

            var moviesUser = _contexte.MoviesUser
                .Include(x => x.Movie) 
                .Where(x => x.IdUser == id);

            var model = moviesUser.Select(x => new MovieViewModel
            {
                IdMovie = x.IdMovie,
                Title = x.Movie.Title,
                Year = x.Movie.Year,
                hasSeen = x.hasSeen,
                isInTheList = true,
                Rating = x.Rating
            }).ToList();

            return View(model);
        }

    }
}
