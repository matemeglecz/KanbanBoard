using KanbanBoardApi.Data;
using KanbanBoardApi.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanBoardApi.Dal
{
    public class LaneRepository : ILaneRepository
    {
        private readonly KanbanBoardContext db;

        public LaneRepository(KanbanBoardContext db)
            => this.db = db;

        public async Task<IReadOnlyCollection<GetLaneDto>> ListLanes()
        {
            return await db.Lanes
                .Include(l => l.Cards)
                .Select(dbRecord => dbRecord.GetLaneDto())
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<GetLaneDto> GetLaneOrNull(int id)
        {
            var lane = await db.Lanes
                .Include(l => l.Cards)
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.ID==id);
            return lane?.GetLaneDto();
        }

        public async Task<GetLaneDto> AddLane(AddLaneDto laneDto)
        {
            Lane newLane = laneDto.GetLane();

            try
            {
                var maxOrder = db.Lanes.Max(l => l.Order);
                if (laneDto.Order > maxOrder)
                    newLane.Order = maxOrder + 1;
                else
                {
                    var laneOrderChangedList = await db.Lanes.Where(l => l.Order >= laneDto.Order).ToListAsync();
                    laneOrderChangedList.ForEach(l => l.Order += 1);
                    newLane.Order = laneDto.Order;
                }
            }
            catch
            {
                newLane.Order = 0;
            }

            await db.Lanes.AddAsync(newLane);
            await db.SaveChangesAsync();

            return newLane.GetLaneDto();
        }

        public async Task<bool> DeleteLane(int id)
        {
            var deletedLane = await db.Lanes.FirstOrDefaultAsync(l => l.ID == id);
            if (deletedLane == null)
                return false;
            
            db.Lanes.Remove(deletedLane);
            await db.SaveChangesAsync();

            return true;
        }

        public bool LaneExists(int id)
        {
            return db.Lanes.Any(e => e.ID == id);
        }
        
    }
    internal static class LaneRepositoryExtensions
    {
        public static GetLaneDto GetLaneDto(this Lane dbRecord)
        {
            var cardDtos = dbRecord.Cards?
                            .Select(c =>
                                new CardDto(c.ID, c.Title, c.LaneID,
                                    c.Description, c.Order, c.Deadline)
                                )
                            .ToList();
            return new GetLaneDto(dbRecord.ID, dbRecord.Title, dbRecord.Order, cardDtos ?? new List<CardDto>());
        }

        public static Lane GetLane(this AddLaneDto lane)
        {
            return new Lane(lane.Title);
        }
    }
}
