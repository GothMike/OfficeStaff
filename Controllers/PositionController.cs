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
    public class PositionController : Controller
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionController(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        [HttpGet("{positionId}")]
        [ProducesResponseType(200, Type = typeof(Position))]
        [ProducesResponseType(400)]
        public IActionResult ReadLocation(int positionId)
        {
            if (!_positionRepository.PositionExists(positionId))
                return NotFound();

            var position = _mapper.Map<PositionDto>(_positionRepository.GetPosition(positionId));


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(position);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] PositionDto positionCreate)
        {
            if (positionCreate == null)
                return BadRequest(ModelState);

            var position = _positionRepository.GetPositions().Where(c => c.Name.Trim().ToUpper() == positionCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (position != null)
            {
                ModelState.AddModelError("", "Позиция уже создана");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var positionMap = _mapper.Map<Position>(positionCreate);

            if (!_positionRepository.CreatePosition(positionMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при создании позиции");
                return StatusCode(500, ModelState);
            }

            return Ok("Успешно создано");
        }


    }
}
