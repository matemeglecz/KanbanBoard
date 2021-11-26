using System.Collections.Generic;

namespace KanbanBoardApi.Dtos
{
    public class GetLaneDto
    {
        public GetLaneDto(int iD, string title, int order, ICollection<CardDto> cards)
        {
            ID = iD;
            Title = title;
            Order = order;
            Cards = cards;
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

        public ICollection<CardDto> Cards { get; private set; }
    }
}
