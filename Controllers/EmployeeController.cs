using Api_First.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // In-memory employee list
        static List<Employee> emp = new List<Employee>()
        {
            new Employee
            {
                Name = "Shivam Kumar",
                Gender = "Male",
                EmpId = "MAN18078",
                City = "Ahmedabad",
                Desgination = "Trainee"
            },
            new Employee
            {
                Name = "Rahul Kumar",
                Gender = "Male",
                EmpId = "MAN18079",
                City = "Ahmedabad",
                Desgination = "Trainee"
            }
        };

        // ✅ GET: api/employee
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(emp);
        }

        // ✅ GET: api/employee/{id}
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(string id)
        {
            var employee = emp.FirstOrDefault(e => e.EmpId == id);
            if (employee == null)
                return NotFound(new { message = "Employee not found" });

            return Ok(employee);
        }

        // ✅ POST: api/employee
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee newEmployee)
        {
            if (emp.Any(e => e.EmpId == newEmployee.EmpId))
                return BadRequest(new { message = "Employee ID already exists" });

            emp.Add(newEmployee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.EmpId }, newEmployee);
        }

        // ✅ PUT: api/employee/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(string id, [FromBody] Employee updatedEmployee)
        {
            var employee = emp.FirstOrDefault(e => e.EmpId == id);
            if (employee == null)
                return NotFound(new { message = "Employee not found" });

            employee.Name = updatedEmployee.Name;
            employee.Gender = updatedEmployee.Gender;
            employee.City = updatedEmployee.City;
            employee.Desgination = updatedEmployee.Desgination;

            return Ok(employee);
        }

        // ✅ DELETE: api/employee/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(string id)
        {
            var employee = emp.FirstOrDefault(e => e.EmpId == id);
            if (employee == null)
                return NotFound(new { message = "Employee not found" });

            emp.Remove(employee);
            return Ok(new { message = "Employee deleted successfully" });
        }
    }
}
