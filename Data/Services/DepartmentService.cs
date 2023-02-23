using AutoMapper;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Data.Services.Interfaces;

namespace OfficeStaff.Data.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, ILocationRepository locationRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public Department CreateDepartment(DepartmentDto department, int locationId)
        {
            var departmentMap = _mapper.Map<Department>(department);
            departmentMap.Location = _locationRepository.GetLocation(locationId);

            return departmentMap;
        }

        public Department DeleteDepartment(int departmentId)
        {
            var departmentToDelete = _departmentRepository.GetDepartment(departmentId);
            
            return departmentToDelete;
        }

        public DepartmentDto GetDepartment(int departmentId)
        {
            var department = _mapper.Map<DepartmentDto>(_departmentRepository.GetDepartment(departmentId));
            
            return department;
        }

        public ICollection<DepartmentDto> GetDepartments()
        {
            var departments = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartments());

            return departments;
        }

        public Department UpdateDepartment(DepartmentDto department, int locationId)
        {
            var departmentMap = _mapper.Map<Department>(department);
            departmentMap.Location = _locationRepository.GetLocation(locationId);

            return departmentMap;
        }
    }
}