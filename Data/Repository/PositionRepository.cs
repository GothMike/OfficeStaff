using OfficeStaff.Data.Interfaces;
using OfficeStaff.Data.Models;
using OfficeStaff.Persistence;

namespace OfficeStaff.Data.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly ApplicationContext _applicationContext;

        public PositionRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool CreatePosition(Position position)
        {
            _applicationContext.Add(position);
            return Save();
        }

        public bool DeletePosition(Position position)
        {
            throw new NotImplementedException();
        }

        public bool PositionExists(int positionId)
        {
            return _applicationContext.Positions.Any(p => p.Id == positionId);
        }

        public Position GetPosition(int positionId)
        {
            return _applicationContext.Positions.Where(p => p.Id == positionId).FirstOrDefault();
        }

        public ICollection<Position> GetPositions()
        {
            return _applicationContext.Positions.ToList();
        }

        public bool UpdatePosition(Position position)
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
