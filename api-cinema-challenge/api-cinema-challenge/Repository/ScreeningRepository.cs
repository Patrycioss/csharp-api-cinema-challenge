using api_cinema_challenge.Data;
using api_cinema_challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace api_cinema_challenge.Repository;

public class ScreeningRepository : IScreeningRepository
{
    private readonly CinemaContext  _context;

    public ScreeningRepository(CinemaContext context)
    {
        _context = context;
    }
    
    public async Task<Screening> CreateScreening(Screening screening)
    {
        _context.Screenings.Add(screening);
        await _context.SaveChangesAsync();
        return screening;
    }

    public async Task<IEnumerable<Screening>> GetScreenings()
    {
        return await _context.Screenings.ToListAsync();
    }
}