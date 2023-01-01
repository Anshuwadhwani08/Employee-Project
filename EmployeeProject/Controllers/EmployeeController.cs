using Employee_migration.Models.Domains;
using EmployeeProject.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.Controllers
{
    [ApiController]
    [Route("Employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        [HttpGet]
        [ActionName("GetAllRecord")]
        public async Task<IActionResult> GetAllRecord()
        {
            var record = await employeeRepository.GetAll();

            //return dto records


            return Ok(record);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var record = await employeeRepository.GetbyID(id);
            if(record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            var record = await employeeRepository.Add(employee);
            return CreatedAtAction(nameof(GetAllRecord), new { id = employee.EmployeeId }, employee);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(Guid guid)
        {
            var record = await employeeRepository.GetbyID(guid);
            if (record == null)
            {
                return NotFound();
            }
            await employeeRepository.Delete(record);
            return Ok("Deleted Successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Guid id ,Employee employee)
        {
            var record = employeeRepository.GetbyID(id);
            if (record == null)
            {
                return NotFound();
            }
            employee.EmployeeId = id;
            await employeeRepository.Update(id,employee);
            return Ok();

        }
    }
}
