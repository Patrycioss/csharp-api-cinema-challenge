using api_cinema_challenge.Models;

namespace api_cinema_challenge.Repository;

public interface IMovieRepository
{
    public Task<Movie> CreateMovie(Movie movie);
    public Task<IEnumerable<Movie>> GetMovies();
    public Task<Movie?> UpdateMovie(Movie movie);
    public Task DeleteMovie(Movie movie);
}