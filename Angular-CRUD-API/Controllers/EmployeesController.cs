using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Angular_CRUD_API.Data;
using Angular_CRUD_API.Model;
using FluentValidation;

namespace Angular_CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IValidator<Employee> _validator;

        public EmployeesController(ApplicationDBContext context, IValidator<Employee> validator)
        {
            _context = context;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            return await _context.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            var validation = await _validator.ValidateAsync(employee);
            
            if (id != employee.id || !validation.IsValid)
            {
                return BadRequest(id != employee.id ? "Record Does not Exist." : validation.Errors);
            }
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var res = new Response() { message = "Employee Updated Successfully!" };
            return Ok(res); 
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            var validation = await _validator.ValidateAsync(employee);
            if (validation.IsValid)
            {
                if (_context.Employees == null)
                {
                    return Problem("Entity set 'ApplicationDBContext.Employees'  is null.");
                }
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEmployee", new { id = employee.id }, employee);
            }
            else
            {
                return BadRequest(validation.Errors);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            var res = new Response() { message = "Employee Deleted Successfully!" };
            return Ok(res);
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
