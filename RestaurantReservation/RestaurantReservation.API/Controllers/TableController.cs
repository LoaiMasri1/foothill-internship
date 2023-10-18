using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("tables")]
public class TableController : ControllerBase
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService) => _tableService = tableService;

    [HttpPost]
    public async Task<IActionResult> CreateTableAsync(TableRequest tableRequest)
    {
        var tableResponse = await _tableService.CreateTableAsync(tableRequest);

        var uri = $"{HttpContext.Request.Path}/{tableResponse.TableId}";

        return Created(uri, tableResponse);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTableAsync(int id)
    {
        await _tableService.DeleteTableAsync(id);

        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTableAsync(int id, TableRequest tableRequest)
    {
        var tableResponse = await _tableService.UpdateTableAsync(id, tableRequest);

        return Ok(tableResponse);
    }
}
