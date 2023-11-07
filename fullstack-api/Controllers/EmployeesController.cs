using fullstack_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fullstack_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly fullstackDbContext _myDbContext;

        public EmployeesController(fullstackDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _myDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _myDbContext.Employees.AddAsync(employeeRequest);
            await _myDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _myDbContext.Employees.FirstOrDefaultAsync(employee => employee.Id == id);
            if(employee == null) 
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee newEmployee)
        {
            var employee = await _myDbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            employee.Name = newEmployee.Name;
            employee.Email = newEmployee.Email;
            employee.Phone = newEmployee.Phone;
            employee.Salary = newEmployee.Salary;
            employee.Department = newEmployee.Department;

            await _myDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _myDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _myDbContext.Employees.Remove(employee);
            await _myDbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
