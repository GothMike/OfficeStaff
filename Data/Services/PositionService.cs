using AutoMapper;
using OfficeStaff.Data.Dto;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Data.Services.Interfaces;

namespace OfficeStaff.Data.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionService(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }
        public Position CreatePosition(PositionDto position, bool isAManagerPosition)
        {
            var positionMap = _mapper.Map<Position>(position);
            positionMap.IsAManagerPosition = isAManagerPosition;

            return positionMap; 
        }

        public Position DeletePosition(int positionId)
        {
            var positionToDelete = _positionRepository.GetPosition(positionId);

            return positionToDelete;
        }

        public PositionDto GetPosition(int positionId)
        {
            var position = _mapper.Map<PositionDto>(_positionRepository.GetPosition(positionId));

            return position;
        }

        public ICollection<PositionDto> GetPositions()
        {
            var positions = _mapper.Map<List<PositionDto>>(_positionRepository.GetPositions());

            return positions;
        }

        public Position UpdatePosition(PositionDto position, bool isAManagerPosition)
        {
            var positionMap = _mapper.Map<Position>(position);
            positionMap.IsAManagerPosition = isAManagerPosition;

            return positionMap;
        }
    }
}