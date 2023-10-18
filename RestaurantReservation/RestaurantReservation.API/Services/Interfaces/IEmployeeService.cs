using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeResponse> CreateEmployeeAsync(EmployeeRequest employeeRequest);
        Task DeleteEmployeeAsync(int id);
        Task<IEnumerable<EmployeeResponse>> ListManagersAsync();
        Task<EmployeeResponse> UpdateEmployeeAsync(int id, EmployeeRequest employeeRequest);
    }
}
