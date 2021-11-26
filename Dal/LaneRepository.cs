using KanbanBoardApi.Data;
using System.Linq;

namespace KanbanBoardApi.Dal
{
    public class LaneRepository : ILaneRepository
    {
        private readonly KanbanBoardContext db;

        public LaneRepository(KanbanBoardContext db)
            => this.db = db;



        public bool LaneExists(int id)
        {
            return db.Lanes.Any(e => e.ID == id);
        }
    }
}
