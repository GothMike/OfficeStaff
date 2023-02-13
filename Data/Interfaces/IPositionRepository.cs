using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Interfaces
{
    public interface IPositionRepository
    {
        bool CreatePosition(Position position);
        Position GetPosition(int positionId);
        ICollection<Position> GetPositions();
        bool UpdatePosition(Position position);
        bool DeletePosition(Position position);
        bool PositionExists(int positionId);
        bool Save();
    }
}
