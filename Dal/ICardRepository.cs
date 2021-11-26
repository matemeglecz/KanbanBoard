using KanbanBoardApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanbanBoardApi.Dal
{
    public interface ICardRepository
    {
        public Task<IReadOnlyCollection<CardDto>> GetCards();

        public Task<CardDto> GetCardOrNull(int id);
        public Task<CardDto> AddCard(CardDto cardDto);
        public Task<bool> DeleteCard(int id);
        public Task<bool> UpdateCard(CardDto newCardDto);
        public bool CardExists(int id);

    }
}
