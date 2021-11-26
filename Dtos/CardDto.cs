using System;

namespace KanbanBoardApi.Dtos
{
    public class CardDto
    {
        public CardDto(int id, string title, int laneID, string description, 
            int order, DateTime? deadline = null)
        {
            Id = id;
            Title = title;
            LaneID = laneID;
            Description = description;
            Order = order;
            Deadline = deadline;
        }
      
        public  int Id { get; set; }

        public string Title { get; set; }

        public int LaneID { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

        public DateTime? Deadline { get; set; }


    }
}
