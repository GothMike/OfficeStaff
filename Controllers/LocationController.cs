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
    public class LocationController : Controller
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public LocationController(ILocationRepository locationRepository, ICountryRepository countryRepository , IMapper mapper)
        {
            _locationRepository = locationRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Location>))]
        [ProducesResponseType(400)]
        public IActionResult GetLocations()
        {
            var locations = _mapper.Map<List<LocationDto>>(_locationRepository.GetLocations());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(locations);
        }

        [HttpGet("{locationId}")]
        [ProducesResponseType(200, Type = typeof(Location))]
        [ProducesResponseType(400)]
        public IActionResult GetLocation(int locationId)
        {
            if (!_locationRepository.LocationExists(locationId))
                return NotFound();

            var location = _mapper.Map<LocationDto>(_locationRepository.GetLocation(locationId));


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(location);
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

            var locationMap = _mapper.Map<Location>(locationCreate);
            
            locationMap.Country = _countryRepository.GetCountry(countryId);    

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

            if (!_locationRepository.LocationExists(locationId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var locationMap = _mapper.Map<Location>(updatedLocation);

            locationMap.Country = _countryRepository.GetCountry(countryId);


            if (!_locationRepository.UpdateLocation(locationMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при редактировании локации");
                return StatusCode(500, ModelState);
            }

            return Ok($"Локация '{locationMap.Name}' - отредактирована в базе данных");
        }

        [HttpDelete("{locationId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLocation(int locationId)
        {
            if (!_locationRepository.LocationExists(locationId))
                return NotFound();

            var locationToDelete = _locationRepository.GetLocation(locationId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_locationRepository.DeleteLocation(locationToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении локации");
                return StatusCode(500, ModelState);
            }

            return Ok($"Локация {locationToDelete.Name} - удалена из базы данных");
        }
    }
}
