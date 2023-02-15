using OfficeStaff.Data.Interfaces;
using OfficeStaff.Data.Models;
using OfficeStaff.Persistence;
using System.Diagnostics.Metrics;

namespace OfficeStaff.Data.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationContext _applicationContext;

        public DepartmentRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool CreateDepartment(Department department)
        {
            _applicationContext.Add(department);
            return Save();
        }

        public bool DeleteDepartment(Department department)
        {
            _applicationContext.Add(department);
            return Save();
        }

        public bool DepartmentExists(int departmentId)
        {
           return _applicationContext.Departments.Any(d => d.Id == departmentId);
        }

        public Department GetDepartment(int departmentId)
        {
            return _applicationContext.Departments.Where(d => d.Id == departmentId).FirstOrDefault();
        }

        public ICollection<Department> GetDepartments()
        {
            return _applicationContext.Departments.ToList();
        }

        public bool UpdateDepartment(Department department)
        {
            _applicationContext.Departments.Update(department);
            return Save();
        }

        public bool Save()
        {
            var saved = _applicationContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}