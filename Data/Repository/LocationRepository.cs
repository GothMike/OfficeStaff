using OfficeStaff.Data.Interfaces;
using OfficeStaff.Data.Models;
using OfficeStaff.Persistence;
using System.Diagnostics.Metrics;

namespace OfficeStaff.Data.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationContext _applicationContext;

        public LocationRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool CreateLocation(Location location)
        {
            _applicationContext.Add(location);
            return Save();
        }

        public bool DeleteLocation(Location Location)
        {
            _applicationContext.Remove(Location);
            return Save();
        }

        public bool LocationExists(int locationId)
        {
            return _applicationContext.Locations.Any(l => l.Id == locationId);
        }

        public Location GetLocation(int locationId)
        {
            return _applicationContext.Locations.Where(l => l.Id == locationId).FirstOrDefault();
        }

        public ICollection<Location> GetLocations()
        {
            return _applicationContext.Locations.ToList();
        }

        public bool UpdateLocation(Location Location)
        {
            _applicationContext.Locations.Update(Location);
            return Save();
        }

        public bool Save()
        {
            var saved = _applicationContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
