using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponse> CreateCustomerAsync(CustomerRequest customerRequest);
        Task DeleteCustomerAsync(int id);
        IEnumerable<CustomerResponse> GetCustomersWithLargeParties(int minPartySize);
        Task<CustomerResponse> UpdateCustomerAsync(int id, CustomerRequest customerRequest);
    }
}
