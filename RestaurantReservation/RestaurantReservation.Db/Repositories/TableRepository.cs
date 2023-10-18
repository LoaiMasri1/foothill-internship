using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.Db.Repositories;

public class TableRepository : ITableRepository
{
    private readonly RestaurantReservationDbContext _context;

    public TableRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<Table> CreateTableAsync(Table table)
    {
        await _context.Tables.AddAsync(table);
        await _context.SaveChangesAsync();

        return table;
    }

    public async Task<Table> UpdateTableAsync(int id, Table table)
    {
        var exist = await IsExistAsync(id);
        if (!exist)
        {
            throw new NotFoundException($"Table with id {id} does not exist");
        }
        _context.Tables.Update(table);
        await _context.SaveChangesAsync();

        return table;
    }

    public async Task DeleteTableAsync(int id)
    {
        var table =
            await _context.Tables.FindAsync(id)
            ?? throw new NotFoundException($"Table with id {id} does not exist");

        _context.Tables.Remove(table);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(int id)
    {
        var exists = await _context.Tables.AnyAsync(x => x.TableId == id);
        return exists;
    }
}
