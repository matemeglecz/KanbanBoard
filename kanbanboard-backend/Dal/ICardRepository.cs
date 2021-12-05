using KanbanBoardApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanbanBoardApi.Dal
{
    public interface ICardRepository
    {
        public Task<IReadOnlyCollection<GetCard>> ListCards();

        public Task<GetCard> GetCardOrNull(int id);
        public Task<GetCard> AddCard(AddCard cardDto);
        public Task<bool> DeleteCard(int id);
        public Task<bool> EditCard(EditCard newCardDto);
        public Task<bool> MoveCard(MoveCard newCardDto);
        public bool CardExists(int id);

    }
}
