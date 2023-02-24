using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;

namespace OfficeStaff.Data.Services.Interfaces
{
    public interface IPositionService
    {
        Position CreatePosition(PositionDto position, bool isAManagerPosition);
        PositionDto GetPosition(int positionId);
        ICollection<PositionDto> GetPositions();
        Position UpdatePosition(PositionDto position, bool isAManagerPosition);
        Position DeletePosition(int positionId);
    }
}