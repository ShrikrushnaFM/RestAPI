using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employeedata>> GetAllEmployeesAsync();
        Task<int> AddEmployeeAsync(Employeedata employee);
    }
}
