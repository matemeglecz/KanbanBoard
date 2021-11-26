namespace KanbanBoardApi.Dtos
{
    public class AddLaneDto
    {
        
        public AddLaneDto(int iD, string title, int order)
        {
            ID = iD;
            Title = title;
            Order = order;
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

    }
    
}
