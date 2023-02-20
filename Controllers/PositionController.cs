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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Position>))]
        [ProducesResponseType(400)]
        public IActionResult GetPositions()
        {
            var positions = _mapper.Map<List<PositionDto>>(_positionRepository.GetPositions());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(positions);
        }

        [HttpGet("{positionId}")]
        [ProducesResponseType(200, Type = typeof(Position))]
        [ProducesResponseType(400)]
        public IActionResult GetPosition(int positionId)
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
        public IActionResult CreatePosition([FromQuery] bool isAManagerPosition, [FromBody] PositionDto positionCreate)
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

            positionMap.IsAManagerPosition = isAManagerPosition;

            if (!_positionRepository.CreatePosition(positionMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при создании позиции");
                return StatusCode(500, ModelState);
            }

            return Ok("Успешно создано");
        }

        [HttpPut("{positionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePosition(int positionId, [FromBody] PositionDto positionUptade)
        {
            if (positionUptade == null)
                return BadRequest();

            if (positionId != positionUptade.Id)
                return BadRequest();

            if (!_positionRepository.PositionExists(positionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var positionMap = _mapper.Map<Position>(positionUptade);

            if (!_positionRepository.UpdatePosition(positionMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при редактировании позиции");
                return StatusCode(500, ModelState);
            }

            return Ok($"Позиция {positionMap.Name} - отредактирована в базе данных");
        }

        [HttpDelete("{positionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePosition(int positionId)
        {
            if (!_positionRepository.PositionExists(positionId))
                return NotFound();

            var positionToDelete = _positionRepository.GetPosition(positionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_positionRepository.DeletePosition(positionToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении позиции");
                return StatusCode(500, ModelState);
            }

            return Ok($"Позиция {positionToDelete.Name} - удалена из базы данных");
        }
    }
}