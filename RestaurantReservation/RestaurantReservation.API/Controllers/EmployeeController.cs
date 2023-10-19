using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;

namespace RestaurantReservation.API.Controllers;

[Authorize]
[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService) =>
        _employeeService = employeeService;

    [HttpPost]
    public async Task<IActionResult> CreateEmployeeAsync(EmployeeRequest employeeRequest)
    {
        var employeeResponse = await _employeeService.CreateEmployeeAsync(employeeRequest);

        var uri = $"{HttpContext.Request.Path}/{employeeResponse.EmployeeId}";

        return Created(uri, employeeResponse);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployeeAsync(int id)
    {
        await _employeeService.DeleteEmployeeAsync(id);

        return NoContent();
    }

    [HttpGet("managers")]
    public async Task<IActionResult> ListManagers()
    {
        var managers = await _employeeService.ListManagersAsync();

        return Ok(managers);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEmployeeAsync(int id, EmployeeRequest employeeRequest)
    {
        var employeeResponse = await _employeeService.UpdateEmployeeAsync(id, employeeRequest);

        return Ok(employeeResponse);
    }
}
