using AutoMapper;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Data.Services.Interfaces;

namespace OfficeStaff.Data.Services
{
    public class EmployeeService : IEmployeeService
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

        public EmployeeDto GetEmployee(int employeeId)
        {
            var employee = _mapper.Map<EmployeeDto>(_employeeRepository.GetEmployee(employeeId));

            return employee;
        }

        public Employee CreateEmployee(int departmentId, int positionId, EmployeeDto employee)
        {
            var employeeMap = _mapper.Map<Employee>(employee);
            employeeMap.Department = _departmentRepository.GetDepartment(departmentId);
            employeeMap.Position = _positionRepository.GetPosition(positionId);

            return employeeMap;
        }

        public Employee UpdateEmployee(EmployeeDto employee)
        {
            var employeeMap = _mapper.Map<Employee>(employee);

            return employeeMap;
        }

        public Employee ChangeADepartment(int departmentId, EmployeeDto employee)
        {
            var employeeMap = _employeeRepository.GetEmployee(employee.Id);
            employeeMap.Department = _departmentRepository.GetDepartment(departmentId);

            return employeeMap;
        }

        public Employee ChangeAPosition(int positionId, EmployeeDto employee)
        {
            var employeeMap = _employeeRepository.GetEmployee(employee.Id);
            employeeMap.Position = _positionRepository.GetPosition(positionId);

            var managerPosition = _positionRepository.GetPosition((int)_employeeRepository.GetEmployee(employee.Id).PositionId);

            if (managerPosition.IsAManagerPosition)
            {
                IEnumerable<Employee> managersemployee = _employeeRepository.GetEmployees().Where(c => c.managerId == employee.Id);

                foreach (var coworker in managersemployee)
                {
                    coworker.managerId = null;
                }
            }

            return employeeMap;
        }

        public Employee SetAManager(int employeeId, int managerId)
        {
            var employeeMap = _employeeRepository.GetEmployee(employeeId);
            employeeMap.managerId = managerId;

            return employeeMap;
        }

        public Employee DeleteEmployee(int employeeId)
        {
            var managerPosition = _positionRepository.GetPosition((int)_employeeRepository.GetEmployee(employeeId).PositionId);

            if (managerPosition.IsAManagerPosition)
            {
                IEnumerable<Employee> managersemployee = _employeeRepository.GetEmployees().Where(c => c.managerId == employeeId);

                foreach (var employee in managersemployee)
                {
                    employee.managerId = null;
                }
            }

            var employeeToDelete = _employeeRepository.GetEmployee(employeeId);

            return employeeToDelete;
        }
    }
}