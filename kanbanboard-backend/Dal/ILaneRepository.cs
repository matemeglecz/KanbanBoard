using KanbanBoardApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanbanBoardApi.Dal
{
    public interface ILaneRepository
    {
        public Task<IReadOnlyCollection<GetLane>> ListLanes();

        public Task<GetLane> GetLaneOrNull(int id);

        public Task<GetLane> AddLane(AddLane laneDto);

        public Task<bool> DeleteLane(int id);

        public bool LaneExists(int id);
    }
}
