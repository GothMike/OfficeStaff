using OfficeStaff.Data.Interfaces;
using OfficeStaff.Data.Models;
using OfficeStaff.Persistence;

namespace OfficeStaff.Services
{
    public class EmployeeService : IEmployeeService
    {
        readonly private ApplicationContext _applicationContext;
        public EmployeeService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool ChangeAPositionAnEmployee(Employee employee)
        {
            _applicationContext.Update(employee);
            return Save();
        }

        public bool SetAManager(Employee employee)
        {
            _applicationContext.Update(employee);
            return Save();
        }

        public bool Save()
        {
            var saved = _applicationContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
