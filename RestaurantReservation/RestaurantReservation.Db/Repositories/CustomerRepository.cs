using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.Db.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly RestaurantReservationDbContext _context;

    public CustomerRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task<Customer> UpdateCustomerAsync(int id, Customer customer)
    {
        var exist = await IsExistAsync(id);
        if (!exist)
        {
            throw new NotFoundException($"Customer with id {id} does not exist");
        }
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer =
            await _context.Customers.FindAsync(id)
            ?? throw new NotFoundException($"Customer with id {id} does not exist");

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(int id)
    {
        var exists = await _context.Customers.AnyAsync(x => x.CustomerId == id);
        return exists;
    }

    public IEnumerable<Customer> GetCustomersWithLargeParties(int minPartySize)
    {
        var customers = _context.Customers
            .FromSqlRaw($"EXEC FindCustomersWithLargeParties {@minPartySize}", minPartySize)
            .AsEnumerable();

        return customers;
    }

    public async Task<Customer> GetCustomerByEmailAsync(string email)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);

        if (customer == null)
        {
            throw new NotFoundException($"Customer with email {email} does not exist");
        }

        return customer;

    }
}
