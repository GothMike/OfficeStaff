using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Interfaces
{
    public interface ILocationRepository
    {
        bool CreateLocation(Location Location);
        Location ReadLocation(int locationId);
        ICollection<Location> ReadLocations();
        bool UpdateLocation(Location location);
        bool DeleteLocation(Location location);
        bool LocationExists(int locationId);
        bool Save();
    }
}
