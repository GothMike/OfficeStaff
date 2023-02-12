using OfficeStaff.Data.Interfaces;
using OfficeStaff.Data.Models;
using OfficeStaff.Persistence;

namespace OfficeStaff.Data.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationContext _applicationContext;
        public CountryRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool CountryExists(int countryId)
        {
            return _applicationContext.Countries.Any(e => e.Id == countryId);
        }

        public bool CreateCountry(Country country)
        {
            _applicationContext.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public ICollection<Country> ReadCountries()
        {
           return _applicationContext.Countries.ToList();
        }

        public Country ReadCountry(int countryId)
        {
            return _applicationContext.Countries.Where(c => c.Id == countryId).FirstOrDefault();
        }


        public bool UpdateCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _applicationContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
