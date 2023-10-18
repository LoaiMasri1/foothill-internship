using AutoMapper;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<EmployeeResponse> CreateEmployeeAsync(EmployeeRequest employeeRequest)
    {
        var newEmployee = _mapper.Map<Employee>(employeeRequest);

        await _employeeRepository.CreateEmployeeAsync(newEmployee);

        var response = _mapper.Map<EmployeeResponse>(newEmployee);
        return response;
    }

    public async Task<EmployeeResponse> UpdateEmployeeAsync(int id, EmployeeRequest employeeRequest)
    {
        var updatedEmployee = _mapper.Map<Employee>(employeeRequest);

        var employee = await _employeeRepository.UpdateEmployeeAsync(id, updatedEmployee);

        var response = _mapper.Map<EmployeeResponse>(employee);

        return response;
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        await _employeeRepository.DeleteEmployeeAsync(id);
    }

    public async Task<IEnumerable<EmployeeResponse>> ListManagersAsync()
    {
        var managers = await _employeeRepository.ListManagersAsync();

        var result = managers.Select(x => _mapper.Map<EmployeeResponse>(x));

        return result;
    }
}
