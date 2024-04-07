// MovieController.cs
using IMDBLib.DTO;
using IMDBLib.Models.Views;
using IMDBLib.Services.APIServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IMDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByMovieTitleAsync(string searchTitle, int page = 1, int pageSize = 10)
        {
            try
            {
                var movies = await _movieService.SearchByMovieTitleAsync(searchTitle, page, pageSize);
                if (movies == null || !movies.Any())
                {
                    return NotFound($"Movie with name '{searchTitle}' not found.");
                }
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMoviesAsync(int page = 1, int pageSize = 10)
        {
            try
            {
                var movies = await _movieService.GetAllMoviesAsync(page, pageSize);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{movieTconst}")]
        public async Task<IActionResult> GetMovieByTconstAsync(string movieTconst)
        {
            try
            {
                var movie = await _movieService.GetMovieByTconstAsync(movieTconst);
                if (movie == null)
                    return NotFound($"Movie with Tconst {movieTconst} not found.");
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMovieAsync(MovieDTO movie)
        {
            try
            {
                await _movieService.AddMovieAsync(movie);
                return StatusCode(201, "Movie added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{movieTconst}")]
        public async Task<IActionResult> UpdateMovieAsync(string movieTconst, MovieDTO updatedMovie)
        {
            try
            {
                await _movieService.UpdateMovieAsync(movieTconst, updatedMovie);
                return Ok($"Movie with Tconst {movieTconst} updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{movieTconst}")]
        public async Task<IActionResult> DeleteMovieAsync(string movieTconst)
        {
            try
            {
                await _movieService.DeleteMovieAsync(movieTconst);
                return Ok($"Movie with Tconst {movieTconst} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
