using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Interfaces
{
    public interface IDepartmentRepository
    {
        bool CreateDepartment(Department department);
        Department ReadDepartment(int departmentId);
        ICollection<Department> ReadDepartments();
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(Department department);
        bool DepartmentExists(int departmentId);
        bool Save();
    }
}
