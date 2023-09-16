using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class CustomerService
{
    private readonly RestaurantReservationDbContext _context;

    public CustomerService(RestaurantReservationDbContext context)
    {
        _context = context;
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

        await _context.Customers.AddAsync(newCustomer);
        await _context.SaveChangesAsync();

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
        var customer = await _context.Customers.FindAsync(id)
            ?? throw new NotFoundException($"Customer with id {id} does not exist");

        customer.FirstName = customerRequest.FirstName;
        customer.LastName = customerRequest.LastName;
        customer.Email = customerRequest.Email;
        customer.PhoneNumber = customerRequest.PhoneNumber;

        await _context.SaveChangesAsync();

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
        var customer = await _context.Customers.FindAsync(id)
            ?? throw new NotFoundException($"Customer with id {id} does not exist");

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public IEnumerable<CustomerResponse> GetCustomersWithLargeParties(int minPartySize) 
    {
        var customers = _context.Customers
            .FromSqlRaw($"EXEC FindCustomersWithLargeParties {@minPartySize}", minPartySize)
            .AsEnumerable();

        var response = customers.Select(x => new CustomerResponse(
            x.CustomerId,
            x.FirstName,
            x.LastName,
            x.Email,
            x.PhoneNumber))
            .ToList();

        return response;
    }

       
    
}
