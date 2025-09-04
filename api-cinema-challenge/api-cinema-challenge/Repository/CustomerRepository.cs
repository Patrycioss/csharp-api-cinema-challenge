using api_cinema_challenge.Data;
using api_cinema_challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace api_cinema_challenge.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly CinemaContext _context;

    public CustomerRepository(CinemaContext context)
    {
        _context = context;
    }
    
    public async Task<Customer> CreateCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    public Task<Customer?> GetCustomer(int id)
    {
        return _context.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
    }

    public async Task<Customer> UpdateCustomer(Customer customer)
    {
        _context.Update(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task DeleteCustomer(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }
}