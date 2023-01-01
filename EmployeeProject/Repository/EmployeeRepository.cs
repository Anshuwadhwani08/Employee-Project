using Employee_migration.Data;
using Employee_migration.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProject.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext employeeContext;

        public EmployeeRepository(EmployeeContext employeeContext)
        {
            this.employeeContext = employeeContext;
        }

        public async  Task<Employee> Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid();
            await employeeContext.AddAsync(employee);
            await employeeContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> Delete(Employee employee)
        {
            employeeContext.Employees.Remove(employee);
            await employeeContext.SaveChangesAsync();
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await employeeContext.Employees.ToListAsync(); 
        }

        public async Task<Employee> GetbyID(Guid id)
        {
            var record = await employeeContext.Employees.FindAsync(id);
            if(record == null)
            {
                return null;
            }
            return record;
        }

        public async Task<Employee> Update(Guid id ,Employee employee)
        {
            var record = employeeContext.Employees.Find(id);
            //if (record == null)
            //{
            //    return null;
            //}
            record.EmployeeName = employee.EmployeeName;
            record.EmployeeStatus = employee.EmployeeStatus;
            record.EmployeeDepartment = employee.EmployeeDepartment;
            employeeContext.Update(record);
            await employeeContext.SaveChangesAsync();
            return record;
            
        }
    }
}
