namespace KanbanBoardApi.Dtos
{
    public class AddLane
    {
        
        public AddLane(int iD, string title, int order)
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
