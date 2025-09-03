using api_cinema_challenge.Data;
using api_cinema_challenge.DTOs.Movie;
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
    
    public async Task<Movie> CreateMovie(MoviePut moviePut)
    {
        var createTime = DateTime.UtcNow;
        var movie = new Movie
        {
            Id = await GetNewId(),
            Title = moviePut.Title,
            Rating = moviePut.Rating,
            Description = moviePut.Description,
            CreatedAt = createTime,
            UpdatedAt = createTime,
        };
        
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<IEnumerable<Movie>> GetMovies()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<Movie?> UpdateMovie(int id, MoviePut moviePut)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
        if (movie == null)
        {
            return null;
        }
        
        movie.Title = moviePut.Title;
        movie.Rating = moviePut.Rating;
        movie.Description = moviePut.Description;
        movie.UpdatedAt = DateTime.UtcNow;
        
        _context.Update(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie?> DeleteMovie(int id)
    {
        var movie =  await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
        if (movie == null)
        {
            return null;
        }
        _context.Remove(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    private async Task<int> GetNewId()
    {
        if (!_context.Movies.Any())
        {
            return 1;
        }
        
        return await _context.Movies.MaxAsync(e => e.Id) + 1;
    }
}