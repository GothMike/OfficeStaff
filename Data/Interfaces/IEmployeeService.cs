using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Interfaces
{
    public interface IEmployeeService
    {
        bool ChangeAPositionAnEmployee(Employee employee);
        bool SetAManager(Employee employee);
        public bool Save();
    }
}
