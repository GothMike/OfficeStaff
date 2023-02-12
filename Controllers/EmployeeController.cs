using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Interfaces;
using OfficeStaff.Data.Models;
using OfficeStaff.Persistence;

namespace OfficeStaff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;


        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromQuery] int departmentId, [FromQuery] int positionId, [FromBody] EmployeeDto employeeCreated)
        {
            if (employeeCreated == null)
                return BadRequest(ModelState);

            var employees = _employeeRepository.ReadEmployees().Where(e => e.LastName.Trim().ToUpper() == employeeCreated.LastName.TrimEnd().ToUpper()).FirstOrDefault();

            if(employees != null) 
            {
                ModelState.AddModelError("", "Employee already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeMap = _mapper.Map<Employee>(employeeCreated);

            if(!_employeeRepository.CreateEmployee(employeeMap, departmentId, positionId))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult ReadEmployee(int employeeId)
        {
            if(!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            var employee = _mapper.Map<EmployeeDto>(_employeeRepository.ReadEmployee(employeeId));


                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(employee);
            }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        [ProducesResponseType(400)]
        public IActionResult ReadEmployees()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.ReadEmployees());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }
    }
}