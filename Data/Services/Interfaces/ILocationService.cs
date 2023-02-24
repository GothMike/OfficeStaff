using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Services.Interfaces
{
    public interface ILocationService
    {
        LocationDto GetLocation(int locationId);
        ICollection<LocationDto> GetLocations();
        Location CreateLocation(LocationDto Location, int countryId);
        Location UpdateLocation(LocationDto location, int countryId);
        Location DeleteLocation(int locationId);
    }
}