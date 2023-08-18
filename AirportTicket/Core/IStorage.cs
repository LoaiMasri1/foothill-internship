namespace AirportTicket.Core
{
    public interface IStorage
    {
        Task<ICollection<T>> ReadAsync<T>() where T : class;
        Task WriteAsync<T>(ICollection<T> data) where T : class;
    }
}