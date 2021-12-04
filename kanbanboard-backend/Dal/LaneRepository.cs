using KanbanBoardApi.Data;
using KanbanBoardApi.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<IReadOnlyCollection<GetLane>> ListLanes()
        {
            return await db.Lanes
                .Include(l => l.Cards)
                .Select(dbRecord => dbRecord.GetLaneDto())
                .AsNoTracking()
                .ToArrayAsync().ConfigureAwait(true);
        }

        public async Task<GetLane> GetLaneOrNull(int id)
        {
            var lane = await db.Lanes
                .Include(l => l.Cards)
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.ID==id).ConfigureAwait(true);
            return lane?.GetLaneDto();
        }

        public async Task<GetLane> AddLane(AddLane laneDto)
        {
            if (laneDto is null)
            {
                throw new System.ArgumentNullException(nameof(laneDto));
            }

            Lane newLane = laneDto.GetLane();

            try
            {
                var maxOrder = db.Lanes.Max(l => l.Order);
                if (laneDto.Order > maxOrder)
                    newLane.Order = maxOrder + 1;
                else
                {
                    var laneOrderChangedList = await db.Lanes.Where(l => l.Order >= laneDto.Order).ToListAsync().ConfigureAwait(true);
                    laneOrderChangedList.ForEach(l => l.Order += 1);
                    newLane.Order = laneDto.Order;
                }
            }
            catch (ArgumentNullException)
            {
                newLane.Order = 0;
            }

            await db.Lanes.AddAsync(newLane).ConfigureAwait(true);
            await db.SaveChangesAsync().ConfigureAwait(true);

            return newLane.GetLaneDto();
        }

        public async Task<bool> DeleteLane(int id)
        {
            var deletedLane = await db.Lanes.FirstOrDefaultAsync(l => l.ID == id).ConfigureAwait(true);

            if (deletedLane == null)
                return false;

            var laneOrderChangedList = await db.Lanes.Where(l => l.Order >= deletedLane.Order).ToListAsync().ConfigureAwait(true);
            laneOrderChangedList.ForEach(l => l.Order -= 1);            

            db.Lanes.Remove(deletedLane);
            await db.SaveChangesAsync().ConfigureAwait(true);

            return true;
        }

        public bool LaneExists(int id)
        {
            return db.Lanes.Any(e => e.ID == id);
        }
        
    }
    internal static class LaneRepositoryExtensions
    {
        public static GetLane GetLaneDto(this Lane dbRecord)
        {
            var cardDtos = dbRecord.Cards?
                            .Select(c =>
                                new GetCard(c.ID, c.Title, c.LaneID,
                                    c.Description, c.Order, c.Deadline)
                                )
                            .ToList();
            return new GetLane(dbRecord.ID, dbRecord.Title, dbRecord.Order, cardDtos ?? new List<GetCard>());
        }

        public static Lane GetLane(this AddLane lane)
        {
            return new Lane(lane.Title);
        }

    }
}
