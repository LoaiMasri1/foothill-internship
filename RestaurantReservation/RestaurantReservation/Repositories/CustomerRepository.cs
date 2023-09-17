﻿using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Services;

namespace RestaurantReservation.Repositories;

public class CustomerRepository
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
        var customer = await _context.Customers.FindAsync(id)
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
}
