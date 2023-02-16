using Microsoft.EntityFrameworkCore;
using OfficeStaff.Data.Interfaces;
using OfficeStaff.Data.Models;
using OfficeStaff.Persistence;

namespace OfficeStaff.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext _applicationContext;

        public EmployeeRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool CreateEmployee(Employee employee, int departmentId, int positionId)
        {
            /*var employeeDepartmentEntity = _applicationContext.Departments.Where(d => d.Id == departmentId).FirstOrDefault();
            var employeePositionEntity = _applicationContext.Positions.Where(p => p.Id == positionId).FirstOrDefault();

            var newEmployee = new Employee()
            {
                Department = employeeDepartmentEntity,
                Position = employeePositionEntity
            };*/
            _applicationContext.Add(employee);

            return Save();
        }


        public bool DeleteEmployee(Employee employee)
        {
            _applicationContext.Remove(employee);
            return Save();
        }

        public bool EmployeeExists(int employeeId)
        {
            return _applicationContext.Employees.Any(e => e.Id == employeeId);
        }

        public Employee GetEmployee(int employeeId)
        {
            return _applicationContext.Employees.Where(e => e.Id == employeeId).FirstOrDefault();
        }

        public ICollection<Employee> GetEmployees()
        {
            return _applicationContext.Employees.ToList();
        }
        public bool UpdateEmployee(Employee employee)
        {
            _applicationContext.Employees.Update(employee);
            return Save();
        }

        public bool Save()
        {
            var saved = _applicationContext.SaveChanges();
            return saved > 0 ? true : false;    
        }
    }
}
