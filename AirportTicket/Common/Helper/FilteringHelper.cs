using AirportTicket.Common.Services.Intf;

namespace AirportTicket.Common.Helper;

public class FilteringHelper
{
    public static Dictionary<int, T> GetFilteredEntitiesDictionary<T>(
            IBaseService<T> service,
            Func<T, bool>? predicate = null)
        where T : class
    {
        predicate ??= _ => true;

        var entitiesResult = service.GetAll(predicate);

        if (entitiesResult.IsFailure)
        {
            Console.WriteLine(entitiesResult.Error);
            return new Dictionary<int, T>();
        }

        var index = 1;
        var entities = entitiesResult.Value;
        var entityDict = entities.ToDictionary(entity => index++, entity => entity);

        return entityDict;
    }
}
