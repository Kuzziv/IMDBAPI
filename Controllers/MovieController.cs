using IMDBLib.DAO;
using IMDBLib.DataBase;
using IMDBLib.Models.Movie;
using IMDBLib.Models.Views;
using IMDBLib.Services.DAOServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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

        // GET: api/<MovieController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieView>>> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var paginatedMovies = await _movieService.GetAllMovies(pageNumber, pageSize);

                return Ok(paginatedMovies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while retrieving data.");
            }
        }

        // make a get by the searchString 
        [HttpGet("{searchString}")]
        public async Task<ActionResult<IEnumerable<MovieView>>> Get(string searchString, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var paginatedMovies = await _movieService.GetMovieListByTitle(searchString, pageNumber, pageSize);
                return Ok(paginatedMovies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while retrieving data.");
            }
        }

        // GET: api/<MovieController>/tt0000001

        [HttpDelete("{tconst}")]
        public async Task<ActionResult<MovieView>> Delete(string tconst)
        {
            try
            {
                await _movieService.DeleteMovie(tconst);
                return Ok();
            }
            catch (ArgumentException)
            {
                // Movie not found, return 404 Not Found
                return NotFound("Movie not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while deleting data.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie([FromBody] TitleDAO titleDAO)
        {
            try
            {
                // Call the service method to add the new movie
                var isAdded = await _movieService.AddMovie(titleDAO);
                if (isAdded)
                {
                    return Ok(); // Return 200 OK if addition is successful
                }
                else
                {
                    return StatusCode(500, "Failed to add movie"); // Return 500 Internal Server Error if addition fails
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while adding movie.");
            }
        }

        // update movie
        [HttpPut]
        public async Task<ActionResult> UpdateMovie([FromBody] TitleDAO titleDAO)
        {
            try
            {
                // Call the service method to update the movie
                var isUpdated = await _movieService.UpdateMovie(titleDAO);
                if (isUpdated)
                {
                    return Ok(); // Return 200 OK if update is successful
                }
                else
                {
                    return StatusCode(500, "Failed to update movie"); // Return 500 Internal Server Error if update fails
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while updating movie.");
            }
        }

    }
}
