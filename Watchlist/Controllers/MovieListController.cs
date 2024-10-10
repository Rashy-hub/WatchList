using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Watchlist.Data;
using Watchlist.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Watchlist.Controllers
{
    [Authorize]
    public class MovieListController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<MovieListController> _logger;


        // **Single Constructor with All Dependencies Injected**
        public MovieListController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            ILogger<MovieListController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        private Task<AppUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var moviesUser = await _context.MoviesUser
                .Include(x => x.Movie)
                .Where(x => x.IdUser == user.Id)
                .ToListAsync();

            var model = moviesUser.Select(x => new MovieViewModel
            {
                IdMovie = x.IdMovie,
                Title = x.Movie.Title,
                Year = x.Movie.Year,
                hasSeen = x.hasSeen,
                isInTheList = true, 
                Rating = x.Rating
            }).ToList();
            ViewData["Title"] = $"{user?.firstname} Movie List";

            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovie([FromBody] MovieRequest request)
        {
            _logger.LogInformation("AddMovie action invoked with MovieId: {MovieId}", request.MovieId);

            var user = await GetCurrentUserAsync();
            //if (user == null)
            //{
            //    _logger.LogWarning("Unauthorized access attempt to AddMovie.");
            //    return Unauthorized(new { success = false, message = "Unauthorized" });
            //}

            // Check if the movie exists in database
            var movie = await _context.Movies.FindAsync(request.MovieId);
            if (movie == null)
            {
                return NotFound(new { success = false, message = $"Movie with ID {request.MovieId} not found." });
            }

            var existingMovie = await _context.MoviesUser
                .FirstOrDefaultAsync(mu => mu.IdUser == user.Id && mu.IdMovie == request.MovieId);

            if (existingMovie != null)
            {
                _logger.LogWarning("Movie with ID {MovieId} already exists in user {UserId}'s list.", request.MovieId, user.Id);
                return BadRequest(new { success = false, message = "Movie already in your list." });
            }

            var movieUser = new MovieUser
            {
                IdUser = user.Id,
                Movie = movie, // Use the navigation property
                hasSeen = false,
                Rating = 0,
            };

            try
            {
                _context.MoviesUser.Add(movieUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding movie to user's list.");
                return StatusCode(500, new { success = false, message = "An error occurred while adding the movie." });
            }

            _logger.LogInformation("Movie with ID {MovieId} added to user {UserId}.", request.MovieId, user.Id);

            return Ok(new { success = true, message = "Movie added successfully." });
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveMovie([FromBody] MovieRequest request)
        {
            _logger.LogInformation("RemoveMovie action invoked with MovieId: {MovieId}", request.MovieId);

            var user = await GetCurrentUserAsync();
            //if (user == null)
            //{
            //    _logger.LogWarning("Unauthorized access attempt to RemoveMovie.");
            //    return Unauthorized(new { success = false, message = "Unauthorized" });
            //}

            var existingMovie = await _context.MoviesUser
                .FirstOrDefaultAsync(mu => mu.IdUser == user.Id && mu.IdMovie == request.MovieId);

            if (existingMovie == null)
            {
                _logger.LogWarning("Movie with ID {MovieId} not found in user {UserId}'s list.", request.MovieId, user.Id);
                return BadRequest(new { success = false, message = "Movie not found in your list." });
            }

            _context.MoviesUser.Remove(existingMovie);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Movie with ID {MovieId} removed from user {UserId}.", request.MovieId, user.Id);

            return Ok(new { success = true, message = "Movie removed successfully." });
        }
    }

    public class MovieRequest
    {
        public int MovieId { get; set; }
    }
}
