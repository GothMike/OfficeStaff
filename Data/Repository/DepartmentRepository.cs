using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Persistence;
using System.Diagnostics.Metrics;

namespace OfficeStaff.Data.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IBaseRepository _baseRepository;

        public DepartmentRepository(ApplicationContext applicationContext, IBaseRepository baseRepository)
        {
            _applicationContext = applicationContext;
            _baseRepository = baseRepository;
        }

        public bool CreateDepartment(Department department)
        {
            _applicationContext.Add(department);
            return _baseRepository.Save();
        }

        public bool DeleteDepartment(Department department)
        {
            _applicationContext.Remove(department);
            return _baseRepository.Save();
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
            return _baseRepository.Save();
        }
    }
}