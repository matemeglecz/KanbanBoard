using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KanbanBoardApi.Data
{
    public class Card
    {
        public int ID { get; set; }
        [Required]
        public int LaneID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int Order { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }
        
        [JsonIgnore]
        public Lane Lane { get; set; }

        public Card() { }
        public Card(int id, string title, int laneID, DateTime? deadline, string description)
        {
            ID = id;
            Title = title;
            LaneID = laneID;
            Deadline = deadline;           
            Description = description == null ? "" : description;
        }

        public Card(string title, int laneID, DateTime? deadline, string description)
        {
            Title = title;
            LaneID = laneID;
            Deadline = deadline;
            Description = description == null ? "" : description;
        }
    }
}
