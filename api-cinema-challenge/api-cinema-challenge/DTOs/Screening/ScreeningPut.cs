namespace api_cinema_challenge.DTOs.Screening;

public class ScreeningPut
{
    public int ScreenNumber { get; set; }
    public int Capacity { get; set; }
    public DateTime StartsAt { get; set; }
}