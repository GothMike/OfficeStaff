using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        bool CreateEmployee(Employee employee, int departmentId, int positionId);
        Employee GetEmployee(int employeeId);
        ICollection<Employee> GetEmployees();
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Employee employee);
        bool EmployeeExists(int employeeId);
        bool Save();
    }
}
