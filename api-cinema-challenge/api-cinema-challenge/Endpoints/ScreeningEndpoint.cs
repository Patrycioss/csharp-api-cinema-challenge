using api_cinema_challenge.DTOs.Screening;
using api_cinema_challenge.Models;
using api_cinema_challenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api_cinema_challenge.Endpoints;

public static class ScreeningEndpoint
{
    public static void ConfigureScreeningEndpoints(this WebApplication app)
    {
        var movies = app.MapGroup("movies");
        movies.MapPost("/{id}/screenings", Create).WithDescription("Create a new movie.");
        movies.MapGet("/{id}/screenings", GetAll).WithDescription("Get a list of every movie.");
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    private static async Task<IResult> Create(IScreeningRepository screeningRepository, int id,
        ScreeningPut screeningPut)
    {
        var createdAt = DateTime.UtcNow;
        var screening = new Screening
        {
            Capacity = screeningPut.Capacity,
            MovieId = id,
            ScreenNumber = screeningPut.ScreenNumber,
            StartsAt = screeningPut.StartsAt,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };

        var result = await screeningRepository.CreateScreening(screening);
        return Results.Created("/", new ScreeningPost
        {
            Id = result.Id,
            ScreenNumber = result.ScreenNumber,
            StartsAt = result.StartsAt,
            Capacity = result.Capacity,
            CreatedAt = createdAt,
            UpdatedAt = createdAt,
        });
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    private static async Task<IResult> GetAll(IScreeningRepository screeningRepository, int id)
    {
        var result = (await screeningRepository.GetScreenings()).Where(screening => screening.MovieId == id);
        var postResults = result.Select(screening =>
            new ScreeningPost
            {
                Id = screening.Id,
                ScreenNumber = screening.ScreenNumber,
                StartsAt = screening.StartsAt,
                Capacity = screening.Capacity,
                CreatedAt = screening.CreatedAt,
                UpdatedAt = screening.UpdatedAt
            }
        ).ToArray();

        if (postResults.Length == 0)
        {
            return Results.NoContent();
        }

        return Results.Ok(postResults);
    }
}