using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieFactoryWebAPI.Data;
using MovieFactoryWebAPI.Interfaces;
using MovieFactoryWebAPI.Models;

namespace MovieFactoryWebAPI.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MovieRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool MovieExists(int id)
        {
            return _context.Movies.Any(c => c.MovieId == id);
        }

        public bool CreateMovie(Movie movie)
        {
            _context.Add(movie);
            return Save();
        }

        public bool DeleteMovie(Movie movie)
        {
            _context.Remove(movie);
            return Save();
        }

        public ICollection<Movie> GetMovies()
        {
           return _context.Movies.Include(r => r.Roles).ToList();           
        }

        public ICollection<Movie>? MovieStartWith(string movieStartWith)
        {
            return _context.Movies.Where(p => p.MovieName.StartsWith(movieStartWith)).ToList();           
        }

        public Movie? GetMovieByRole(string roleName)
        {
            return _context.Roles.Where(x => x.RoleName.Trim().ToUpper().StartsWith(roleName.Trim().ToUpper())).Select(c => c.Movie).FirstOrDefault();
        }
        public Movie? GetMovie(int id)
        {
            return _context.Movies.FirstOrDefault(c => c.MovieId == id);           
        }

        public Movie? GetMovieByRole(int roleId)
        {
            return _context.Roles.Where(o => o.RoleId == roleId).Select(c => c.Movie).FirstOrDefault();
        }

        public ICollection<Role>? GetAllRoleForThisMovie(int movieId)
        {
            return _context.Roles.Where(c => c.Movie.MovieId == movieId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMovie(Movie country)
        {
            _context.Update(country);
            return Save();
        }
    }
}
