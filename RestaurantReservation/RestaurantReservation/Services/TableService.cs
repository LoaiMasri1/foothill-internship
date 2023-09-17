using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Repositories;

namespace RestaurantReservation.Services;

public class TableService
{
    private readonly TableRepository _tableRepository;

    public TableService(TableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public async Task<TableResponse> CreateTableAsync(TableRequest tableRequest)
    {
        var newTable = new Table
        {
             Capacity = tableRequest.Capacity,
             ResturantId = tableRequest.RestaurantId,
        };

        await _tableRepository.CreateTableAsync(newTable);
        var response = new TableResponse(
            newTable.TableId,
            newTable.Capacity,
            newTable.ResturantId);

        return response;
    }

    public async Task<TableResponse> UpdateTableAsync(int id, TableRequest tableRequest)
    {
        var updatedTable = 
            new Table
            {
                Capacity = tableRequest.Capacity,
                ResturantId = tableRequest.RestaurantId,
            };

        var table = await _tableRepository
            .UpdateTableAsync(id, updatedTable);

        var response = new TableResponse(
            table.TableId,
            table.Capacity,
            table.ResturantId);

        return response;
    }

    public async Task DeleteTableAsync(int id)
    {
        await _tableRepository.DeleteTableAsync(id);    
    }
}
