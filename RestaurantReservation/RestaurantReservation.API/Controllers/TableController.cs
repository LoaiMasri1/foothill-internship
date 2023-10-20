using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Middlewares;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Controllers;

[Authorize]
[ApiController]
[Route("tables")]
public class TableController : ControllerBase
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService) => _tableService = tableService;

    /// <summary>
    /// Creates a new table.
    /// </summary>
    /// <param name="tableRequest">The table request.</param>
    /// <returns>The created table.</returns>
    /// <response code="201">Returns the created table.</response>
    /// <response code="400">Validation error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(TableResponse), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> CreateTableAsync(TableRequest tableRequest)
    {
        var tableResponse = await _tableService.CreateTableAsync(tableRequest);

        var uri = $"{HttpContext.Request.Path}/{tableResponse.TableId}";

        return Created(uri, tableResponse);
    }

    /// <summary>
    /// Deletes a table by ID.
    /// </summary>
    /// <param name="id">The ID of the table to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">No content.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteTableAsync(int id)
    {
        await _tableService.DeleteTableAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Updates a table by ID.
    /// </summary>
    /// <param name="id">The ID of the table to update.</param>
    /// <param name="tableRequest">The table request.</param>
    /// <returns>The updated table.</returns>
    /// <response code="200">Returns the updated table.</response>
    /// <response code="400">Validation error.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(TableResponse), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> UpdateTableAsync(int id, TableRequest tableRequest)
    {
        var tableResponse = await _tableService.UpdateTableAsync(id, tableRequest);

        return Ok(tableResponse);
    }
}
