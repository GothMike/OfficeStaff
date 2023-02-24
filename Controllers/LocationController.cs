using Microsoft.AspNetCore.Mvc;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Data.Services.Interfaces;

namespace OfficeStaff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ILocationService _locationService;

        public LocationController(ILocationRepository locationRepository, ICountryRepository countryRepository, ILocationService locationService)
        {
            _locationRepository = locationRepository;
            _countryRepository = countryRepository;
            _locationService = locationService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Location>))]
        [ProducesResponseType(400)]
        public IActionResult GetLocations()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_locationService.GetLocations());
        }

        [HttpGet("{locationId}")]
        [ProducesResponseType(200, Type = typeof(Location))]
        [ProducesResponseType(400)]
        public IActionResult GetLocation(int locationId)
        {
            if (!_locationRepository.LocationExists(locationId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_locationService.GetLocation(locationId));
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLocation([FromQuery] int countryId,[FromBody] LocationDto locationCreate)
        {
            if (locationCreate == null)
                return BadRequest(ModelState);

            if (!_countryRepository.CountryExists(countryId))
                return NotFound("Страна не найдена");

            var location = _locationRepository.GetLocations().Where(c => c.Name.Trim().ToUpper() == locationCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (location != null)
            {
                ModelState.AddModelError("", "Локация уже создана");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var locationMap = _locationService.CreateLocation(locationCreate, countryId);
            
            if (!_locationRepository.CreateLocation(locationMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при создании локации");
                return StatusCode(500, ModelState);
            }

            return Ok("Успешно создано");
        }

        [HttpPut("{locationId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLocation(int locationId,[FromQuery] int countryId ,[FromBody] LocationDto updatedLocation)
        {
            if (updatedLocation == null)
                return BadRequest();

            if (locationId != updatedLocation.Id)
                return BadRequest();

            if (!_countryRepository.CountryExists(countryId))
                return NotFound("Страна не найдена");

            if (!_locationRepository.LocationExists(locationId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var locationMap = _locationService.UpdateLocation(updatedLocation, countryId);

            if (!_locationRepository.UpdateLocation(locationMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при редактировании локации");
                return StatusCode(500, ModelState);
            }

            return Ok($"Локация отредактирована в базе данных");
        }

        [HttpDelete("{locationId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLocation(int locationId)
        {
            if (!_locationRepository.LocationExists(locationId))
                return NotFound();

            var locationToDelete = _locationService.DeleteLocation(locationId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_locationRepository.DeleteLocation(locationToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении локации");
                return StatusCode(500, ModelState);
            }

            return Ok($"Локация удалена из базы данных");
        }
    }
}