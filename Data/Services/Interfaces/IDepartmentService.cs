using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Services.Interfaces
{
    public interface IDepartmentService
    {
        Department CreateDepartment(DepartmentDto department,int locatioinId);
        DepartmentDto GetDepartment(int departmentId);
        ICollection<DepartmentDto> GetDepartments();
        Department UpdateDepartment(DepartmentDto department, int locationId);
        Department DeleteDepartment(int departmentId);
    }
}