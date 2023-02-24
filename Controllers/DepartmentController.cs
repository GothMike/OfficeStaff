using Microsoft.AspNetCore.Mvc;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Data.Services.Interfaces;

namespace OfficeStaff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentRepository departmentRepository, ILocationRepository locationRepository, IDepartmentService departmentService)
        {
            _departmentRepository = departmentRepository;
            _locationRepository = locationRepository;
            _departmentService = departmentService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Department>))]
        public IActionResult GetDepartments()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_departmentService.GetDepartments());
        }

        [HttpGet("{departmentId}")]
        [ProducesResponseType(200, Type = typeof(Department))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_departmentService.GetDepartment(departmentId));
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

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentMap = _departmentService.CreateDepartment(departmentCreate, locationId);

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

            if (!_locationRepository.LocationExists(locationId))
                return NotFound("Локация не найдена");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentMap = _departmentService.UpdateDepartment(departmentCreate, locationId);

            if (!_departmentRepository.UpdateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при редактировании департамента");
                return StatusCode(500, ModelState);
            }

            return Ok($"Департамент отредактирован в базе данных");
        }

        [HttpDelete("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            var departmentToDelete = _departmentService.DeleteDepartment(departmentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_departmentRepository.DeleteDepartment(departmentToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении департамента");
                return StatusCode(500, ModelState);
            }

            return Ok($"Департамент  удален из базы данных");
        }
    }
}