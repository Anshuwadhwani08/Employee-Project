using Employee_migration.Models.Domains;

namespace EmployeeProject.Repository
{
    public interface IEmployeeRepository
    {
        Task< IEnumerable<Employee>> GetAll();

        Task<Employee> Add(Employee employee);

        Task< Employee> GetbyID(Guid id);

        Task <Employee> Delete(Employee employee);

        Task<Employee> Update(Guid id,Employee employee);
    }
}
