using api_cinema_challenge.Models;

namespace api_cinema_challenge.Repository;

public interface ICustomerRepository
{
    public Task<Customer> CreateCustomer(Customer customer);
    public Task<IEnumerable<Customer>> GetCustomers();
    public Task<Customer> UpdateCustomer(Customer customer);
    public Task<bool> DeleteCustomer(Customer customer);
}