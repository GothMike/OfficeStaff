using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Interfaces
{
    public interface ICountryRepository
    {
        bool CreateCountry(Country country);
        Country ReadCountry(int countryId);
        ICollection<Country> ReadCountries();
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        bool CountryExists(int countryId);
        bool Save();
    }
}
