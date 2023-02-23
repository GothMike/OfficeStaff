using Microsoft.EntityFrameworkCore;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Persistence;

namespace OfficeStaff.Data.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IBaseRepository _baseRepository;
        public CountryRepository(ApplicationContext applicationContext, IBaseRepository baseRepository)
        {
            _applicationContext = applicationContext;
            _baseRepository = baseRepository;
        }

        public bool CountryExists(int countryId)
        {
            return _applicationContext.Countries.Any(e => e.Id == countryId);
        }

        public bool CreateCountry(Country country)
        {
            _applicationContext.Add(country);
            return _baseRepository.Save();
        }

        public bool DeleteCountry(Country country)
        {
            _applicationContext.Remove(country);
            return _baseRepository.Save();
        }

        public ICollection<Country> GetCountries()
        {
           return _applicationContext.Countries.ToList();
        }

        public Country GetCountry(int countryId)
        {
            return _applicationContext.Countries.Where(c => c.Id == countryId).FirstOrDefault();
        }

        public bool UpdateCountry(Country country)
        {
            _applicationContext.Countries.Update(country);
            return _baseRepository.Save();
        }
    }
}