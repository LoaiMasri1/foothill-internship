using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Services;

namespace RestaurantReservation.Repositories;

public class EmployeeRepository
{
    private readonly RestaurantReservationDbContext _context;

    public EmployeeRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task<Employee> UpdateEmployeeAsync(int id, Employee employee)
    {
        var exist = await IsExistAsync(id);
        if (!exist)
        {
            throw new NotFoundException($"Employee with id {id} does not exist");
        }
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id)
            ?? throw new NotFoundException($"Employee with id {id} does not exist");

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(int id)
    {
        var exists = await _context.Employees.AnyAsync(x => x.EmployeeId == id);
        return exists;
    }

    public async Task<IEnumerable<Employee>> ListManagersAsync()
    {
        var managers = await _context.Employees
            .Where(x => x.Position == "Manager")
            .ToListAsync();

        return managers;
    }
}
