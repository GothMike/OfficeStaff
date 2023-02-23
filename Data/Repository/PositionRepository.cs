using OfficeStaff.Data.Interfaces;
using OfficeStaff.Data.Models;
using OfficeStaff.Persistence;

namespace OfficeStaff.Data.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IBaseRepository _baseRepository;

        public PositionRepository(ApplicationContext applicationContext, IBaseRepository baseRepository)
        {
            _applicationContext = applicationContext;
            _baseRepository = baseRepository;
        }

        public bool CreatePosition(Position position)
        {
            _applicationContext.Add(position);
            return _baseRepository.Save();
        }

        public bool DeletePosition(Position position)
        {
            _applicationContext.Remove(position);
            return _baseRepository.Save();
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
            _applicationContext.Positions.Update(position);
            return _baseRepository.Save();
        }
    }
}