using api_cinema_challenge.DTOs.Screening;
using api_cinema_challenge.Models;

namespace api_cinema_challenge.Repository;

public interface IScreeningRepository
{
    public Task<Screening> CreateScreening(ScreeningPut screening);
    public Task<IEnumerable<Screening>> GetScreenings();
}