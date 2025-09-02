using api_cinema_challenge.DTOs.Customer;
using api_cinema_challenge.Models;

namespace api_cinema_challenge.Repository;

public interface ICustomerRepository
{
    public Task<Customer> CreateCustomer(CustomerPut customerPut);
    public Task<IEnumerable<Customer>> GetCustomers();
    public Task<Customer?> UpdateCustomer(int id, CustomerPut customerPut);
    public Task<Customer?> DeleteCustomer(int id);
}