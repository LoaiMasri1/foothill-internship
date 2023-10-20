using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Middlewares;
using RestaurantReservation.API.Services;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;

namespace RestaurantReservation.API.Controllers;

[Authorize]
[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IOrderService _orderService;

    public EmployeeController(IEmployeeService employeeService, IOrderService orderService)
    {
        _employeeService = employeeService;
        _orderService = orderService;
    }

    /// <summary>
    /// Creates a new employee.
    /// </summary>
    /// <param name="employeeRequest">The employee request.</param>
    /// <returns>The created employee.</returns>
    /// <response code="200">Returns the created employee.</response>
    /// <response code="400">Validation error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(EmployeeResponse), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> CreateEmployeeAsync(EmployeeRequest employeeRequest)
    {
        var employeeResponse = await _employeeService.CreateEmployeeAsync(employeeRequest);

        var uri = $"{HttpContext.Request.Path}/{employeeResponse.EmployeeId}";

        return Created(uri, employeeResponse);
    }

    /// <summary>
    /// Deletes an employee by ID.
    /// </summary>
    /// <param name="id">The employee ID.</param>
    /// <returns>No content.</returns>
    /// <response code="204">No content.</response>
    /// <response code="404">If the employee is not found.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    public async Task<IActionResult> DeleteEmployeeAsync(int id)
    {
        await _employeeService.DeleteEmployeeAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Lists all managers.
    /// </summary>
    /// <returns>The list of managers.</returns>
    /// <response code="200">Returns the list of managers.</response>
    [HttpGet("managers")]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> ListManagers()
    {
        var managers = await _employeeService.ListManagersAsync();

        return Ok(managers);
    }

    /// <summary>
    /// Updates an employee by ID.
    /// </summary>
    /// <param name="id">The employee ID.</param>
    /// <param name="employeeRequest">The employee request.</param>
    /// <returns>The updated employee.</returns>
    /// <response code="200">Returns the updated employee.</response>
    /// <response code="400">Validation error.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(EmployeeResponse), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    public async Task<IActionResult> UpdateEmployeeAsync(int id, EmployeeRequest employeeRequest)
    {
        var employeeResponse = await _employeeService.UpdateEmployeeAsync(id, employeeRequest);

        return Ok(employeeResponse);
    }

    /// <summary>
    /// Calculates the average order amount for a given employee.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <returns>The average order amount.</returns>
    /// <response code="200">Returns the average order amount.</response>
    [HttpGet("{employeeId:int}/average-order-amount")]
    [ProducesResponseType(typeof(decimal), 200)]
    public async Task<IActionResult> CalculateAverageOrderAmountAsync(int employeeId)
    {
        var averageOrderAmount = await _orderService.CalculateAverageOrderAmountAsync(employeeId);

        return Ok(averageOrderAmount);
    }
}
