namespace AirportTicket.Common.Services.Intf;

public interface IBaseService<T> where T : class
{
    Result<ICollection<T>> GetAll();
    Result<ICollection<T>> GetAll(Func<T, bool> predicate);
    Result<T?> Get(Func<T, bool> predicate);
    Task<Result<T>> AddAsync(T entity);
    Result<T> Update(Guid id, T entity);
}