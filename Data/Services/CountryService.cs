using AutoMapper;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Data.Services.Interfaces;

namespace OfficeStaff.Data.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public Country CreateCountry(CountryDto country)
        {
            var countryMap = _mapper.Map<Country>(country);
            
            return countryMap;
        }

        public Country DeleteCountry(int countryId)
        {
            var countryToDelete = _countryRepository.GetCountry(countryId);

            return countryToDelete;
        }

        public ICollection<CountryDto> GetCountries () 
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            return countries;
        }

        public CountryDto GetCountry(int countryId) 
        {
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));
            
            return country;
        }

        public Country UpdateCountry(CountryDto country)
        {
            var countryMap = _mapper.Map<Country>(country);

            return countryMap;
        }
    }
}