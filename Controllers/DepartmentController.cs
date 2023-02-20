using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Interfaces;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository;

namespace OfficeStaff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, ILocationRepository locationRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Department>))]
        public IActionResult GetDepartments()
        {
            var departments = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartments());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(departments);
        }

        [HttpGet("{departmentId}")]
        [ProducesResponseType(200, Type = typeof(Department))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            var department = _mapper.Map<DepartmentDto>(_departmentRepository.GetDepartment(departmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(department);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDepartment([FromQuery] int locationId, [FromBody] DepartmentDto departmentCreate)
        {
            if (departmentCreate == null)
                return BadRequest(ModelState);

            if (!_locationRepository.LocationExists(locationId))
                return NotFound("Локация не найдена");

            var department = _departmentRepository.GetDepartments().Where(c => c.Name.Trim().ToUpper() == departmentCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (department != null)
            {
                ModelState.AddModelError("", "Департамент уже создан");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentMap = _mapper.Map<Department>(departmentCreate);

            departmentMap.Location = _locationRepository.GetLocation(locationId);

            if (!_departmentRepository.CreateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при создании департамента");
                return StatusCode(500, ModelState);
            }

            return Ok("Успешно создано");
        }

        [HttpPut("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDepartment(int departmentId,[FromQuery] int locationId, [FromBody] DepartmentDto departmentCreate)
        {
            if (departmentCreate == null)
                return BadRequest();

            if (departmentId != departmentCreate.Id)
                return BadRequest();

            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentMap = _mapper.Map<Department>(departmentCreate);

            departmentMap.Location = _locationRepository.GetLocation(locationId);

            if (!_departmentRepository.UpdateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при редактировании департамента");
                return StatusCode(500, ModelState);
            }

            return Ok($"Департамент {departmentMap.Name} - отредактирован в базе данных");
        }

        [HttpDelete("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            var departmentToDelete = _departmentRepository.GetDepartment(departmentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_departmentRepository.DeleteDepartment(departmentToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении департамента");
                return StatusCode(500, ModelState);
            }

            return Ok($"Департамент {departmentToDelete.Name} - удалена из базы данных");
        }
    }
}
