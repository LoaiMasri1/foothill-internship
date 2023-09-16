using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class TableService
{
    private readonly RestaurantReservationDbContext _context;

    public TableService(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<TableResponse> CreateTableAsync(TableRequest tableRequest)
    {
        var newTable = new Table
        {
             Capacity = tableRequest.Capacity,
             ResturantId = tableRequest.RestaurantId,
        };

        await _context.Tables.AddAsync(newTable);
        await _context.SaveChangesAsync();

        var response = new TableResponse(
            newTable.TableId,
            newTable.Capacity,
            newTable.ResturantId);

        return response;
    }

    public async Task<TableResponse> UpdateTableAsync(int id, TableRequest tableRequest)
    {
        var table = await _context.Tables.FindAsync(id)
            ?? throw new NotFoundException($"Table with id {id} does not exist");

        var isRestaurantExist = await _context.Resturants
            .AnyAsync(x => x.ResturantsId == tableRequest.RestaurantId);

        if (!isRestaurantExist)
        {
            throw new NotFoundException($"Restaurant with id {tableRequest.RestaurantId} does not exist");
        }

        table.Capacity = tableRequest.Capacity;
        table.ResturantId = tableRequest.RestaurantId;

        await _context.SaveChangesAsync();

        var response = new TableResponse(
            table.TableId,
            table.Capacity,
            table.ResturantId);

        return response;
    }

    public async Task DeleteTableAsync(int id)
    {
        var table = await _context.Tables.FindAsync(id)
            ?? throw new NotFoundException($"Table with id {id} does not exist");

        _context.Tables.Remove(table);
        await _context.SaveChangesAsync();
    }
}
