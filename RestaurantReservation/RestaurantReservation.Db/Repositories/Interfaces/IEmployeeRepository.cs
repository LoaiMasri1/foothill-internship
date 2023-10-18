using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
        Task<bool> IsExistAsync(int id);
        Task<IEnumerable<Employee>> ListManagersAsync();
        Task<Employee> UpdateEmployeeAsync(int id, Employee employee);
    }
}
