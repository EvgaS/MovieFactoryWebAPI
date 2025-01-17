using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieFactoryWebAPI.DTo;
using MovieFactoryWebAPI.Interfaces;
using MovieFactoryWebAPI.Models;

namespace MovieFactoryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController: ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        public IActionResult GetMovies()
        {
            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetMovies());

            if (!movies.Any())
                return NotFound("Any movies have not beeen created yet.Please add actors");

            return Ok(movies);
        }


        [HttpGet("GetMovie/{movieStartWith}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        public IActionResult MovieStartWith(string movieStartWith)
        {
            var movie = _mapper.Map<List<MovieDto>>(_movieRepository.MovieStartWith(movieStartWith));

            if (movie == null)
                return NotFound($"Any movies have not started with - {movieStartWith}!");

            return Ok(movie);
        }


        [HttpGet("{movieId}")]
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(400)]
        public IActionResult GetMovie(int movieId)
        {
            var movie = _mapper.Map<MovieDto>(_movieRepository.GetMovie(movieId));

            if (movie == null)
                return NotFound($"The movie has not find");

            return Ok(movie);
        }


        [HttpGet("/Movie/GetMovieByRoleId/{roleId}")]
        [ProducesResponseType(200, Type = typeof(Movie))]
        public IActionResult GetMovieForRole(int roleId)
        {
            var movie = _movieRepository.GetMovieByRole(roleId);

            if (movie == null)
                return NotFound($"Any movie not consost role with this id");

            return Ok(movie);
        }


        [HttpGet("/Movie/{roleName}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Movie))]
        public IActionResult GetMovieByRole(string roleName)
        {
            var movie = _mapper.Map<MovieDto>(
                _movieRepository.GetMovieByRole(roleName));

            if (movie == null)
                return NotFound($"Any movie not consost role with this name");

            return Ok(movie);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMovie([FromBody] MovieDto movieCreate)
        {
            if (movieCreate == null)
            {
                ModelState.AddModelError("", "Movie Create can t be empty");
                return StatusCode(422, ModelState);
            }

            var movie = _movieRepository.GetMovies()?
                .FirstOrDefault(c => c.MovieName.Trim().ToUpper() == movieCreate.MovieName!.TrimEnd().ToUpper());

            if (movie != null)
            {
                ModelState.AddModelError("", "Movie already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieMap = _mapper.Map<Movie>(movieCreate);

            if (!_movieRepository.CreateMovie(movieMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{movieId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMovie(int movieId)
        {
            var movieToDelete = _movieRepository.GetMovie(movieId);

            if (movieToDelete == null)
            {
                return NotFound();
            }

            if (!_movieRepository.DeleteMovie(movieToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with deleting movie");
            }

            return Ok($"Movie - {movieToDelete.MovieName} have been succesfully deleted");
        }
    }
}
