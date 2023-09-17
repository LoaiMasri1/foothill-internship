using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Repositories;

namespace RestaurantReservation.Services;

public class CustomerService
{
    private readonly CustomerRepository _customerRepository;

    public CustomerService(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerResponse> CreateCustomerAsync(CustomerRequest customerRequest)
    {
        var newCustomer = new Customer
        {
            FirstName = customerRequest.FirstName,
            LastName = customerRequest.LastName,
            Email = customerRequest.Email,
            PhoneNumber = customerRequest.PhoneNumber,
        };

        await _customerRepository.CreateCustomerAsync(newCustomer);

        var response = new CustomerResponse(
            newCustomer.CustomerId,
            newCustomer.FirstName,
            newCustomer.LastName,
            newCustomer.Email,
            newCustomer.PhoneNumber
            );

        return response;
    }

    public async Task<CustomerResponse> UpdateCustomerAsync(int id, CustomerRequest customerRequest)
    {
        var updatedCustomer = new Customer
        {
            FirstName = customerRequest.FirstName,
            LastName = customerRequest.LastName,
            Email = customerRequest.Email,
            PhoneNumber = customerRequest.PhoneNumber,
        };

        var customer = await _customerRepository
            .UpdateCustomerAsync(id,updatedCustomer);

        var response = new CustomerResponse(
            customer.CustomerId,
            customer.FirstName,
            customer.LastName,
            customer.Email,
            customer.PhoneNumber);

        return response;
    }

    public async Task DeleteCustomerAsync(int id)
    {
        await _customerRepository.DeleteCustomerAsync(id);
    }

    public IEnumerable<CustomerResponse> GetCustomersWithLargeParties(int minPartySize) 
    {
       var customers = _customerRepository.GetCustomersWithLargeParties(minPartySize);

        var result = customers.Select(x => new CustomerResponse(
                x.CustomerId,
                x.FirstName,
                x.LastName,
                x.Email,
                x.PhoneNumber));

        return result;
    }

       
    
}
