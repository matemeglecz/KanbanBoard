using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KanbanBoardApi.Dtos
{
    public class CardDto
    {
        public CardDto(int id, string title, int laneID, string description, int order, DateTime? deadline = null)
        {
            Id = id;
            Title = title;
            LaneID = laneID;
            Description = description;
            Order = order;
            Deadline = deadline;
        }
      
        public  int Id { get; private set; }

        public string Title { get; private set; }

        public int LaneID { get; private set; }

        public string Description { get; private set; }

        public int Order { get; private set; }

        public DateTime? Deadline { get; private set; }


    }
}
