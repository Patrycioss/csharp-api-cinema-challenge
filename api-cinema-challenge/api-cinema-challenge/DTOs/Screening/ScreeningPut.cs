namespace api_cinema_challenge.DTOs.Screening;

public class ScreeningPut
{
    public int ScreenNumber { get; set; }
    public int Capacity { get; set; }
    public DateTime StartsAt { get; set; }

    public Models.Screening ToScreening()
    {
        return new Models.Screening
        {
            ScreenNumber = ScreenNumber,
            Capacity = Capacity,
            CreatedAt = StartsAt,
        };
    }
}