using System;

namespace KanbanBoardApi.Dtos
{
    public class EditCard
    {
        public EditCard(int id, string title, string description, DateTime? deadline = null)
        {
            Id = id;
            Title = title;
            Description = description;
            Deadline = deadline;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
