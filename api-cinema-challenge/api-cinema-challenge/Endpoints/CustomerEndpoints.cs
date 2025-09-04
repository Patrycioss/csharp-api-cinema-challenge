using api_cinema_challenge.DTOs.Customer;
using api_cinema_challenge.Models;
using api_cinema_challenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api_cinema_challenge.Endpoints;

public static class CustomerEndpoints
{
    public static void ConfigureCustomerEndpoints(this WebApplication app)
    {
        var customers = app.MapGroup("customers");
        customers.MapPost("/", Create).WithDescription("Create a new customer account.");
        customers.MapGet("/", GetAll).WithDescription("Get a list of every customer.");
        customers.MapPatch("/{id:int}", Update).WithDescription(
            "Update an existing customer. For ease of implementation, all fields are required from the client.");
        customers.MapDelete("/{id:int}", Delete).WithDescription(
            "Delete an existing customer. When deleting data, it's useful to send the deleted record back to the client so they can re-create it if deletion was a mistake.\n");
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    private static async Task<IResult> Create(ICustomerRepository customerRepository, CustomerPut customerPut)
    {
        var createTime = DateTime.UtcNow;
        var customer = new Customer
        {
            Email = customerPut.Email,
            Name = customerPut.Name,
            Phone = customerPut.Phone,
            CreatedAt = createTime,
            UpdatedAt = createTime,
        };
        var result = await customerRepository.CreateCustomer(customer);
        return Results.Created("/", result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    private static async Task<IResult> GetAll(ICustomerRepository customerRepository)
    {
        var result = await customerRepository.GetCustomers();

        var resultArray = result.ToArray();
        if (resultArray.Length == 0)
        {
            return Results.NoContent();
        }

        return Results.Ok(resultArray);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static async Task<IResult> Update(ICustomerRepository customerRepository, int id, CustomerPut customerPut)
    {
        var customer = await customerRepository.GetCustomer(id);
        if (customer == null)
        {
            return Results.NotFound("Customer not found");
        }

        customer.Name = customerPut.Name;
        customer.Phone = customerPut.Phone;
        customer.Email = customerPut.Email;
        customer.UpdatedAt = DateTime.UtcNow;

        var result = await customerRepository.UpdateCustomer(customer);
        return Results.Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static async Task<IResult> Delete(ICustomerRepository customerRepository, int id)
    {
        var customer = await customerRepository.GetCustomer(id);
        if (customer == null)
        {
            return Results.NotFound("Customer not found");
        }

        await customerRepository.DeleteCustomer(customer);
        return Results.Ok(customer);
    }
}