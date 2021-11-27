namespace KanbanBoardApi.Dtos
{
    public class MoveCard
    {
        public MoveCard(int id, int laneID, int order)
        {
            Id = id;
            LaneID = laneID;
            Order = order;
        }

        public int Id { get; set; }
        public int LaneID { get; set; }
        public int Order { get; set; }
    }
}
