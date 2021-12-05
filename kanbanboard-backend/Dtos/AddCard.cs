using System;

namespace KanbanBoardApi.Dtos
{
    public class AddCard
    {
        public AddCard(string title, int laneID, string description,
            DateTime? deadline = null)
        {
            Title = title;
            LaneID = laneID;
            Description = description;
            Deadline = deadline;
        }

        public string Title { get; set; }

        public int LaneID { get; set; }

        public string Description { get; set; }

        public DateTime? Deadline { get; set; }


    }
}
