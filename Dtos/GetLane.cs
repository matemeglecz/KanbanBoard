using System.Collections.Generic;

namespace KanbanBoardApi.Dtos
{
    public class GetLane
    {
        public GetLane(int iD, string title, int order, ICollection<GetCard> cards)
        {
            ID = iD;
            Title = title;
            Order = order;
            Cards = cards;
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

        public ICollection<GetCard> Cards { get; private set; }
    }
}
