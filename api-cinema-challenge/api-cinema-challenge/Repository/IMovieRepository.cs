using api_cinema_challenge.DTOs.Movie;
using api_cinema_challenge.Models;

namespace api_cinema_challenge.Repository;

public interface IMovieRepository
{
    public Task<Movie> CreateMovie(MoviePut movie);
    public Task<IEnumerable<Movie>> GetMovies();
    public Task<Movie?> UpdateMovie(int id, MoviePut movie);
    public Task<Movie?> DeleteMovie(int id);
}