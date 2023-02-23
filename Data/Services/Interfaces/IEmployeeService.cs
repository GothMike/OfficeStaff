using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Services.Interfaces
{
    public interface IEmployeeService
    {
        ICollection<EmployeeDto> GetEmployees();
        EmployeeDto GetEmployee(int employeeId);
        Employee CreateEmployee(int departmentId, int positionId, EmployeeDto employee);
        Employee UpdateEmployee(EmployeeDto employee);
        Employee ChangeAPosition(int positionId, EmployeeDto employee);
        Employee ChangeADepartment(int departmentId, EmployeeDto employee);
        Employee SetAManager(int employeeId, int managerId);
        Employee DeleteEmployee(int employeeId);
    }
}