using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Interfaces
{
    public interface ILocationRepository
    {
        bool CreateLocation(Location Location);
        Location GetLocation(int locationId);
        ICollection<Location> GetLocations();
        bool UpdateLocation(Location location);
        bool DeleteLocation(Location location);
        bool LocationExists(int locationId);
    }
}
