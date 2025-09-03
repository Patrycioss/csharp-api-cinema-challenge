using api_cinema_challenge.Data;
using api_cinema_challenge.DTOs.Screening;
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
    
    public async Task<Screening> CreateScreening(ScreeningPut screeningPut)
    {
        var createTime = DateTime.UtcNow;
        var screening = new Screening
        {
            Id = await GetNewId(),
            ScreenNumber = screeningPut.ScreenNumber,
            Capacity = screeningPut.Capacity,
            StartsAt = screeningPut.StartsAt,
            CreatedAt = createTime,
            UpdatedAt = createTime,
        };
        
        _context.Screenings.Add(screening);
        await _context.SaveChangesAsync();
        return screening;
    }

    public async Task<IEnumerable<Screening>> GetScreenings()
    {
        return await _context.Screenings.ToListAsync();
    }

    private async Task<int> GetNewId()
    {
        if (!_context.Screenings.Any())
        {
            return 1;
        }
        
        return await _context.Screenings.MaxAsync(e => e.Id) + 1;
    }
}