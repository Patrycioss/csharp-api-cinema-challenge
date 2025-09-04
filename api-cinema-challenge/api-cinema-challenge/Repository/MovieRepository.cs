using api_cinema_challenge.Data;
using api_cinema_challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace api_cinema_challenge.Repository;

public class MovieRepository : IMovieRepository
{
    private readonly CinemaContext _context;
    
    public MovieRepository(CinemaContext context)
    {
        _context = context;
    }
    
    public async Task<Movie> CreateMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<IEnumerable<Movie>> GetMovies()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<Movie?> GetMovie(int id)
    {
        return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Movie> UpdateMovie(Movie movie)
    {
        _context.Update(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task DeleteMovie(Movie movie)
    {
        _context.Remove(movie);
        await _context.SaveChangesAsync();
    }
}