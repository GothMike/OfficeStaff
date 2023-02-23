using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        bool CreateDepartment(Department department);
        Department GetDepartment(int departmentId);
        ICollection<Department> GetDepartments();
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(Department department);
        bool DepartmentExists(int departmentId);
    }
}