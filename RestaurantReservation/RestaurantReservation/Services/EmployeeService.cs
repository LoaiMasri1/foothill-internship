using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Repositories;

namespace RestaurantReservation.Services;

public class EmployeeService
{
    private readonly EmployeeRepository _employeeRepository;

    public EmployeeService(EmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
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

        await _employeeRepository.CreateEmployeeAsync(newEmployee);

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
        
        var updatedEmployee = new Employee
        {
            FirstName = employeeRequest.FirstName,
            LastName = employeeRequest.LastName,
            Position = employeeRequest.Position,
            ResturantId = employeeRequest.ResturantId
        };

        var employee = await _employeeRepository
            .UpdateEmployeeAsync(id, updatedEmployee);

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
        await _employeeRepository.DeleteEmployeeAsync(id);
    }

    public async Task<IEnumerable<EmployeeResponse>> ListManagersAsync() { 
        var managers = await _employeeRepository
            .ListManagersAsync();

        var result = managers.Select(x => new EmployeeResponse(
            x.EmployeeId,
            x.ResturantId,
            x.FirstName,
            x.LastName,
            x.Position));

        return result;
    }
}
