using AutoMapper;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Interfaces;
using OfficeStaff.Data.Models;
using OfficeStaff.Persistence;
using System.Collections.Specialized;
using System.ComponentModel;

namespace OfficeStaff.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IPositionRepository positionRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public ICollection<EmployeeDto> GetEmployees()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployees());
            return employees;
        }





    }
}
