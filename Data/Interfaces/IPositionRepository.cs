using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Interfaces
{
    public interface IPositionRepository
    {
        bool CreatePosition(Position position);
        Position ReadPosition(int positionId);
        ICollection<Position> ReadPositons();
        bool UpdatePosition(Position position);
        bool DeletePosition(Position position);
        bool PositionExists(int positionId);
        bool Save();
    }
}
