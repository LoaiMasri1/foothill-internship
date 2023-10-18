using AutoMapper;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerResponse> CreateCustomerAsync(CustomerRequest customerRequest)
    {
        var newCustomer = _mapper.Map<Customer>(customerRequest);

        await _customerRepository.CreateCustomerAsync(newCustomer);

        var response = _mapper.Map<CustomerResponse>(newCustomer);

        return response;
    }

    public async Task<CustomerResponse> UpdateCustomerAsync(int id, CustomerRequest customerRequest)
    {
        var updatedCustomer = _mapper.Map<Customer>(customerRequest);

        var customer = await _customerRepository.UpdateCustomerAsync(id, updatedCustomer);

        var response = _mapper.Map<CustomerResponse>(customer);

        return response;
    }

    public async Task DeleteCustomerAsync(int id)
    {
        await _customerRepository.DeleteCustomerAsync(id);
    }

    public IEnumerable<CustomerResponse> GetCustomersWithLargeParties(int minPartySize)
    {
        var customers = _customerRepository.GetCustomersWithLargeParties(minPartySize);

        var result = customers.Select(x => _mapper.Map<CustomerResponse>(x));

        return result;
    }
}
