using Microsoft.AspNetCore.Mvc;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Data.Services.Interfaces;

namespace OfficeStaff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : Controller
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IPositionService _positionService;

        public PositionController(IPositionRepository positionRepository, IPositionService positionService)
        {
            _positionRepository = positionRepository;
            _positionService = positionService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Position>))]
        [ProducesResponseType(400)]
        public IActionResult GetPositions()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_positionService.GetPositions());
        }

        [HttpGet("{positionId}")]
        [ProducesResponseType(200, Type = typeof(Position))]
        [ProducesResponseType(400)]
        public IActionResult GetPosition(int positionId)
        {
            if (!_positionRepository.PositionExists(positionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_positionService.GetPosition(positionId));
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

            var positionMap = _positionService.CreatePosition(positionCreate, isAManagerPosition);

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
        public IActionResult UpdatePosition(int positionId, [FromQuery] bool isAManagerPosition, [FromBody] PositionDto positionUptade)
        {
            if (positionUptade == null)
                return BadRequest();

            if (positionId != positionUptade.Id)
                return BadRequest();

            if (!_positionRepository.PositionExists(positionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var positionMap = _positionService.UpdatePosition(positionUptade, isAManagerPosition);

            if (!_positionRepository.UpdatePosition(positionMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при редактировании позиции");
                return StatusCode(500, ModelState);
            }

            return Ok($"Позиция отредактирована в базе данных");
        }

        [HttpDelete("{positionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePosition(int positionId)
        {
            if (!_positionRepository.PositionExists(positionId))
                return NotFound();

            var positionToDelete = _positionService.DeletePosition(positionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_positionRepository.DeletePosition(positionToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении позиции");
                return StatusCode(500, ModelState);
            }

            return Ok($"Позиция удалена из базы данных");
        }
    }
}