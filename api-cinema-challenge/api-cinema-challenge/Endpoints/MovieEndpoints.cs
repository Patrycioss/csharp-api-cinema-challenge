using api_cinema_challenge.DTOs.Movie;
using api_cinema_challenge.Models;
using api_cinema_challenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api_cinema_challenge.Endpoints;

public static class MovieEndpoints
{
    public static void ConfigureMovieEndpoints(this WebApplication app)
    {
        var movies = app.MapGroup("movies");
        movies.MapPost("/", Create).WithDescription("Create a new movie.");
        movies.MapGet("/", GetAll).WithDescription("Get a list of every movie.");
        movies.MapPatch("/{id:int}", Update).WithDescription("Update an existing movie. For ease of implementation, all fields are required from the client.");
        movies.MapDelete("/{id:int}", Delete).WithDescription("Delete an existing movie. When deleting data, it's useful to send the deleted record back to the client so they can re-create it if deletion was a mistake.");
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    private static async Task<IResult> Create(IMovieRepository movieRepository, MoviePut moviePut)
    {
        var createdAt = DateTime.UtcNow;
        var movie = new Movie
        {
            Title = moviePut.Title,
            Description = moviePut.Description,
            Rating = moviePut.Rating,
            RuntimeMins = moviePut.RuntimeMins,
            CreatedAt = createdAt,
            UpdatedAt = createdAt,
        };

        var result = await movieRepository.CreateMovie(movie);
        return Results.Created("/", new MoviePost
        {
            Id = result.Id,
            Title = result.Title,
            Description = result.Description,
            Rating = result.Rating,
            RuntimeMins = result.RuntimeMins,
            CreatedAt = createdAt,
            UpdatedAt = createdAt,
        });
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    private static async Task<IResult> GetAll(IMovieRepository movieRepository)
    {
        var result = await movieRepository.GetMovies();
        var postResults = result.Select(m =>
            new MoviePost
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                Rating = m.Rating,
                RuntimeMins = m.RuntimeMins,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt
            }
        ).ToArray();

        if (postResults.Length == 0)
        {
            return Results.NoContent();
        }
        
        return Results.Ok(postResults);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static async Task<IResult> Update(IMovieRepository movieRepository, int id, MoviePut moviePut)
    {
        var movie = await movieRepository.GetMovie(id);
        if (movie == null)
        {
            return Results.NotFound("Movie not found");
        }
        
        movie.Title = moviePut.Title;
        movie.Description = moviePut.Description;
        movie.Rating = moviePut.Rating;
        movie.RuntimeMins = moviePut.RuntimeMins;
        
        var result = await movieRepository.UpdateMovie(movie);
        return Results.Ok(new MoviePost
        {
            Id = result.Id,
            Title = result.Title,
            Description = result.Description,
            Rating = result.Rating,
            RuntimeMins = result.RuntimeMins,
            CreatedAt = result.CreatedAt,
            UpdatedAt = result.UpdatedAt
        });
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static async Task<IResult> Delete(IMovieRepository movieRepository, int id)
    {
        var movie = await movieRepository.GetMovie(id);
        if (movie == null)
        {
            return Results.NotFound("Movie not found");
        }

        await movieRepository.DeleteMovie(movie);
        return Results.Ok(movie);
    }
}