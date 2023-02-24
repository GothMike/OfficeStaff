using AutoMapper;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Data.Services.Interfaces;

namespace OfficeStaff.Data.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public Location CreateLocation(LocationDto location, int countryId)
        {
            var locationMap = _mapper.Map<Location>(location);
            locationMap.Country = _countryRepository.GetCountry(countryId);

            return locationMap;
        }

        public Location DeleteLocation(int locationId)
        {
            var locationToDelete = _locationRepository.GetLocation(locationId);

            return locationToDelete;
        }

        public LocationDto GetLocation(int locationId)
        {
            var location = _mapper.Map<LocationDto>(_locationRepository.GetLocation(locationId));

            return location;
        }

        public ICollection<LocationDto> GetLocations()
        {
            var locations = _mapper.Map<List<LocationDto>>(_locationRepository.GetLocations());

            return locations;
        }

        public Location UpdateLocation(LocationDto location, int countryId)
        {
            var locationMap = _mapper.Map<Location>(location);
            locationMap.Country = _countryRepository.GetCountry(countryId);

            return locationMap;
        }
    }
}