using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class EmployeeService
{
    private readonly RestaurantReservationDbContext _context;

    public EmployeeService(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeResponse> CreateEmployeeAsync(EmployeeRequest employeeRequest)
    {
        var newEmployee = new Employee
        {
            FirstName = employeeRequest.FirstName,
            LastName = employeeRequest.LastName,
            Position = employeeRequest.Position,
            ResturantId = employeeRequest.ResturantId
        };

        await _context.Employees.AddAsync(newEmployee);
        await _context.SaveChangesAsync();

        var response = new EmployeeResponse(
            newEmployee.EmployeeId,
            newEmployee.ResturantId,
            newEmployee.FirstName,
            newEmployee.LastName,
            newEmployee.Position);

        return response;
    }

    public async Task<EmployeeResponse> UpdateEmployeeAsync(int id, EmployeeRequest employeeRequest)
    {
        var employee = await _context.Employees.FindAsync(id)
            ?? throw new NotFoundException($"Employee with id {id} does not exist");

        var isResturantExist = await _context.Resturants
            .AnyAsync(x => x.ResturantsId == employeeRequest.ResturantId);

        if (!isResturantExist)
        {
            throw new NotFoundException($"Resturant with id {employeeRequest.ResturantId} does not exist");
        }

        employee.FirstName = employeeRequest.FirstName;
        employee.LastName = employeeRequest.LastName;
        employee.Position = employeeRequest.Position;
        employee.ResturantId = employeeRequest.ResturantId;

        await _context.SaveChangesAsync();

        var response = new EmployeeResponse(
            employee.EmployeeId,
            employee.ResturantId,
            employee.FirstName,
            employee.LastName,
            employee.Position);

        return response;
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id)
            ?? throw new NotFoundException($"Employee with id {id} does not exist");

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
}
