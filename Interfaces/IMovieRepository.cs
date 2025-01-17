using MovieFactoryWebAPI.Models;

namespace MovieFactoryWebAPI.Interfaces
{
    public interface IMovieRepository
    {
        ICollection<Movie>? GetMovies();

        ICollection<Movie>? MovieStartWith(string movieStartWith);

        Movie? GetMovie(int id);

        Movie? GetMovieByRole(string roleName);

        Movie? GetMovieByRole(int movieId);

        ICollection<Role>? GetAllRoleForThisMovie(int movieId);

        bool MovieExists(int id);

        bool CreateMovie(Movie movie);

        bool UpdateMovie(Movie movie);

        bool DeleteMovie(Movie movie);

        bool Save();
    }
}
