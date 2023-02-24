using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Services.Interfaces
{
    public interface ICountryService
    {
        ICollection<CountryDto> GetCountries();
        CountryDto GetCountry(int countryId);
        Country CreateCountry(CountryDto country);
        Country UpdateCountry(CountryDto country);
        Country DeleteCountry(int countryId);
    }
}