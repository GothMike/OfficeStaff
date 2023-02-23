using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Persistence;
using System.Diagnostics.Metrics;

namespace OfficeStaff.Data.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IBaseRepository _baseRepository;

        public LocationRepository(ApplicationContext applicationContext, IBaseRepository baseRepository)
        {
            _applicationContext = applicationContext;
            _baseRepository = baseRepository;
        }

        public bool CreateLocation(Location location)
        {
            _applicationContext.Add(location);
            return _baseRepository.Save();
        }

        public bool DeleteLocation(Location Location)
        {
            _applicationContext.Remove(Location);
            return _baseRepository.Save();
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
            return _baseRepository.Save();
        }
    }
}