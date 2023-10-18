using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        IEnumerable<Customer> GetCustomersWithLargeParties(int minPartySize);
        Task<bool> IsExistAsync(int id);
        Task<Customer> UpdateCustomerAsync(int id, Customer customer);
    }
}
