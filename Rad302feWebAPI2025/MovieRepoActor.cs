using Microsoft.AspNetCore.Mvc;
using Rad302feCL2025;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rad302feWebAPI2025
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieRepoActor : ControllerBase
    {

        private readonly IMovieRepository _movieRepository;
        [Route("api/[controler]")]
        [Route("apiController")]
        public class movieController : ControllerBase
        {
            private readonly IMovieRepository _movieRepository;

            public movieController(IMovieRepository movieRepository)
            {
                _movieRepository = movieRepository;
            }

            // GET: api/<MovieRepoActor>
            [HttpGet]
            public async Task<IActionResult> GetAllMovies()
            {
                var movies = await _movieRepository.GetAllMoviesAsync();
                return Ok(movies);
            }

            // GET api/<MovieRepoActor>/5

            [HttpGet("{id}")]
            public async Task<IActionResult> GetMovieById(int id)
            {
                var movie = await _movieRepository.GetMovieByIdAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }
                return Ok(movie);
            }

            // POST api/<MovieRepoActor>
            [HttpPost]
            public async Task<IActionResult> AddMovie([FromBody] Movie movie)
            {
                if (movie == null)
                {
                    return BadRequest();
                }
                var createdMovie = await _movieRepository.AddMovieAsync(movie);
                return CreatedAtAction(nameof(GetMovieById), new { id = createdMovie.Id }, createdMovie);
            }
            // PUT api/<MovieRepoActor>/5
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie movie)
            {
                if (id != movie.Id)
                {
                    return BadRequest();
                }
                var updatedMovie = await _movieRepository.UpdateMovieAsync(movie);
                if (updatedMovie == null)
                {
                    return NotFound();
                }
                return NoContent();
            }

            // DELETE api/<MovieRepoActor>/5
            [HttpPost]
            public async Task<IActionResult> DeleteMovie(int id)
            {
                var result = await _movieRepository.DeleteMovieAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            // GET api/<MovieRepoActor>/5/actors
            [HttpGet("{id}/actors")]
            public async Task<IActionResult> GetActorsByMovieId(int id)
            {
                var actors = await _movieRepository.GetActorsByMovieIdAsync(id);
                if (actors == null || actors.Count == 0)
                {
                    return NotFound();
                }
                return Ok(actors);
            }
            // GET api/<MovieRepoActor>/5/genres
            [HttpGet("{id}/genres")]
            public async Task<IActionResult> GetGenresByMovieId(int id)
            {
                var genres = await _movieRepository.GetGenresByMovieIdAsync(id);
                if (genres == null || genres.Count == 0)
                {
                    return NotFound();
                }
                return Ok(genres);
            }

        }


    }
}
