using Microsoft.AspNetCore.Mvc;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Data.Services.Interfaces;

namespace OfficeStaff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IPositionRepository positionRepository , IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _positionRepository = positionRepository;
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployees()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_employeeService.GetEmployees());
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployee(int employeeId)
        {
            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_employeeService.GetEmployee(employeeId));
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromQuery] int departmentId, [FromQuery] int positionId, [FromBody] EmployeeDto employeeCreated)
        {
            if (employeeCreated == null)
                return BadRequest(ModelState);

            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound("Департамент не найден");

            if (!_positionRepository.PositionExists(positionId))
                return NotFound("Позиция не найдена");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = _employeeService.CreateEmployee(departmentId, positionId, employeeCreated);

            if (!_employeeRepository.CreateEmployee(employee, departmentId, positionId))
            {
                ModelState.AddModelError("", "Что то пошло не так при создании сотрудника");
                return StatusCode(500, ModelState);
            }

            return Ok("Успешно создано");
        }

        [HttpPut("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] EmployeeDto updateEmployee)
        {
            if (updateEmployee == null)
                return BadRequest();

            if (employeeId != updateEmployee.Id)
                return BadRequest();

            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = _employeeService.UpdateEmployee(updateEmployee);

            if (!_employeeRepository.UpdateEmployee(employee))
            {
                ModelState.AddModelError("", "Что-то пошло не так при редактировании сотрудника");
                return StatusCode(500, ModelState);
            }

            return Ok($"Сотрудник отредактирован в базе данных");
        }

        [HttpPut("{employeeId}/ChangeADepartment")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult ChangeADepartment(int employeeId, [FromQuery] int departmentId, [FromBody] EmployeeDto updateEmployee)
        {
            if (updateEmployee == null)
                return BadRequest();

            if (employeeId != updateEmployee.Id)
                return BadRequest();

            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound("Департамент не найден");

            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = _employeeService.ChangeADepartment(departmentId, updateEmployee);

            if (!_employeeRepository.UpdateEmployee(employee))
            {
                ModelState.AddModelError("", "Что-то пошло не так при редактировании сотрудника");
                return StatusCode(500, ModelState);
            }

            return Ok($"Сотрудник отредактирован в базе данных");
        }

        [HttpPut("{employeeId}/ChangeAPosition")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult ChangeAPosition(int employeeId, [FromQuery] int positionId, [FromBody] EmployeeDto updateEmployee)
        {
            if (updateEmployee == null)
                return BadRequest();

            if (employeeId != updateEmployee.Id)
                return BadRequest();
            if (!_positionRepository.PositionExists(positionId))
                return NotFound("Позиция не найдена");

            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = _employeeService.ChangeAPosition(positionId, updateEmployee);

            if (!_employeeRepository.UpdateEmployee(employee))
            {
                ModelState.AddModelError("", "Что-то пошло не так при редактировании сотрудника");
                return StatusCode(500, ModelState);
            }

            return Ok($"Сотрудник отредактирован в базе данных");
        }

        [HttpPut("{employeeId}/SetAManager")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult SetAManager(int employeeId, [FromQuery] int managerId, [FromBody] EmployeeDto updateEmployee)
        {
            var managerPosition = _positionRepository.GetPosition((int)_employeeRepository.GetEmployee(managerId).PositionId);

            if (updateEmployee == null)
                return BadRequest();

            if(managerId == employeeId) 
                return BadRequest("Нельзя назначить себя своим менеджером");

            if (employeeId != updateEmployee.Id)
                return BadRequest();

            if (!_employeeRepository.EmployeeExists(managerId))
                return NotFound("Менеджер не найден");

            if(!managerPosition.IsAManagerPosition)
                return NotFound("Этот сотрудник не менеджер");

            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = _employeeService.SetAManager(employeeId, managerId);

            if (!_employeeRepository.UpdateEmployee(employee))
            {
                ModelState.AddModelError("", "Что-то пошло не так при редактировании сотрудника");
                return StatusCode(500, ModelState);
            }

            return Ok($"Сотрудник отредактирован в базе данных");
        }

        [HttpDelete("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEmployee(int employeeId)
        {
            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            var employeeToDelete = _employeeService.DeleteEmployee(employeeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_employeeRepository.DeleteEmployee(employeeToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении сотрудника");
                return StatusCode(500, ModelState);
            }

            return Ok($"Сотрудник удален из базы данных");
        }
    }
}