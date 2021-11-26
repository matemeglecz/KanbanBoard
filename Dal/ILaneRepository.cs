using KanbanBoardApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanbanBoardApi.Dal
{
    public interface ILaneRepository
    {
        public Task<IReadOnlyCollection<GetLaneDto>> ListLanes();

        public Task<GetLaneDto> GetLaneOrNull(int id);

        public Task<GetLaneDto> AddLane(AddLaneDto laneDto);

        public Task<bool> DeleteLane(int id);

        public bool LaneExists(int id);
    }
}
