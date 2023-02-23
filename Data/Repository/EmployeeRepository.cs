using Microsoft.EntityFrameworkCore;
using OfficeStaff.Data.Interfaces;
using OfficeStaff.Data.Models;
using OfficeStaff.Persistence;

namespace OfficeStaff.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IBaseRepository _baseRepository;

        public EmployeeRepository(ApplicationContext applicationContext, IBaseRepository baseRepository)
        {
            _applicationContext = applicationContext;
            _baseRepository = baseRepository;
        }

        public bool CreateEmployee(Employee employee, int departmentId, int positionId)
        {
            var positionHistory = new PositionHistory()
            {
                Employee = employee,
                Position = _applicationContext.Positions.Where(e => e.Id == positionId).FirstOrDefault(),
                Created = DateTime.Now,
            };

            _applicationContext.PositionHistory.Add(positionHistory);
            _applicationContext.Add(employee);

            return _baseRepository.Save();
        }

        public bool DeleteEmployee(Employee employee)
        {
            _applicationContext.Remove(employee);
            return _baseRepository.Save();
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
            return _baseRepository.Save();
        }
    }
}