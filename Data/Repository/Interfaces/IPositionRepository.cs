using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Repository.Interfaces
{
    public interface IPositionRepository
    {
        bool CreatePosition(Position position);
        Position GetPosition(int positionId);
        ICollection<Position> GetPositions();
        bool UpdatePosition(Position position);
        bool DeletePosition(Position position);
        bool PositionExists(int positionId);
    }
}
