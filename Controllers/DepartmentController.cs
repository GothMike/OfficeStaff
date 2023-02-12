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

        [HttpGet("{departmentId}")]
        [ProducesResponseType(200, Type = typeof(Department))]
        [ProducesResponseType(400)]
        public IActionResult ReadDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            var department = _mapper.Map<DepartmentDto>(_departmentRepository.ReadDepartment(departmentId));


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

            var department = _departmentRepository.ReadDepartments().Where(c => c.Name.Trim().ToUpper() == departmentCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (department != null)
            {
                ModelState.AddModelError("", "Департамент уже создан");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentMap = _mapper.Map<Department>(departmentCreate);

            departmentMap.Location = _locationRepository.ReadLocation(locationId);

            if (!_departmentRepository.CreateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при создании департамента");
                return StatusCode(500, ModelState);
            }

            return Ok("Успешно создано");
        }




    }
}
