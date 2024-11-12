using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers.EmployeeController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDataController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepository;

       public EmployeeDataController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employeedata employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee data is invalid.");
            }

            try
            {
                // Call the repository to add the employee
                var employeeId = await _employeeRepository.AddEmployeeAsync(employee);
                return Ok(new { EmployeeId = employeeId, Message = "Employee added successfully" });
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the insertion
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
