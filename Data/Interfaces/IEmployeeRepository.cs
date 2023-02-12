using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        bool CreateEmployee(Employee employee, int departmentId, int positionId);
        Employee ReadEmployee(int employeeId);
        ICollection<Employee> ReadEmployees();
        bool UpdateEmployee(Employee employee, int departmentId, int positionId);
        bool DeleteEmployee(Employee employee);
        bool EmployeeExists(int employeeId);
        bool Save();
    }
}
