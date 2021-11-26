using KanbanBoardApi.Data;
using KanbanBoardApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanBoardApi.Dal
{
    public class CardRepository : ICardRepository
    {
        private readonly KanbanBoardContext db;
        private readonly ILaneRepository laneRepository;

        public CardRepository(KanbanBoardContext db, ILaneRepository laneRepository)
        {
            this.db = db;
            this.laneRepository = laneRepository;
        }
            

        public async Task<IReadOnlyCollection<CardDto>> GetCards()
        {
            return await db.Cards
                .Select(dbRecord => dbRecord.GetCardDto())
                .ToArrayAsync();
        }

        public async Task<CardDto> GetCardOrNull(int id)
        {
            var card = await db.Cards.FindAsync(id);
            return card?.GetCardDto();
        }

        public async Task<CardDto> AddCard(CardDto cardDto)
        {
            Card newCard = cardDto.GetCard();

            try
            {
                var maxOrder = db.Cards.Where(c => c.LaneID == cardDto.LaneID).Max(c => c.Order);
                newCard.Order = maxOrder + 1;
            }
            catch
            {
                newCard.Order = 0;
            }

            await db.Cards.AddAsync(newCard);
            await db.SaveChangesAsync();

            return newCard.GetCardDto();
        }

        public async Task<bool> DeleteCard(int id)
        {            
            var deletedCard = await db.Cards.FindAsync(id);
            if (deletedCard == null) 
                return false;

            var cards = await db.Cards.Where(c => c.LaneID == deletedCard.LaneID).Where(c => c.ID != deletedCard.ID).ToListAsync();
            MoveUpCards(cards, deletedCard.Order);

            db.Cards.Remove(deletedCard);

                
            await db.SaveChangesAsync();
            return true;                
            
        }

        public async Task<bool> UpdateCard(CardDto newCardDto)
        {
            if (!CardExists(newCardDto.Id) || !laneRepository.LaneExists(newCardDto.LaneID))
            {
                return false;
            }

            var oldCard = await db.Cards.FindAsync(newCardDto.Id);
            db.Entry(oldCard).State = EntityState.Detached;

            if (oldCard.LaneID != newCardDto.LaneID)
            {
                // régi oszlopban aki mögötte volt fel, új oszlopban aki mögötte van le                
                var cardsOld = await db.Cards.Where(c => c.LaneID == oldCard.LaneID).Where(c => c.ID != oldCard.ID).ToListAsync();
                MoveUpCards(cardsOld, oldCard.Order);                
                var cardsNew = await db.Cards.Where(c => c.LaneID == newCardDto.LaneID).Where(c => c.ID != oldCard.ID).ToListAsync();
                MoveDownCards(cardsNew, newCardDto.Order);

            }
            else if (oldCard.Order != newCardDto.Order)
            {
                // eredeti hely mögött van fel, új hely mögött van le                
                var cards = await db.Cards.Where(c => c.LaneID == oldCard.LaneID).Where(c => c.ID != oldCard.ID).ToListAsync();
                MoveUpCards(cards, oldCard.Order);
                MoveDownCards(cards, newCardDto.Order);                
            }

            db.Entry(newCardDto.GetCard()).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(newCardDto.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public bool CardExists(int id)
        {
            return db.Cards.Any(e => e.ID == id);
        }

        private void MoveUpCards(List<Card> cards, int limit)
        {
            cards.ForEach(c =>
            {
                if (c.Order >= limit)
                    c.Order -= 1;
            });
        }

        private void MoveDownCards(List<Card> cards, int limit)
        {
            cards.ForEach(c =>
            {
                if (c.Order >= limit)
                    c.Order += 1;
            });
        }

    }

    

    internal static class CardRepositoryExtensions
    {
        public static CardDto GetCardDto(this Card dbRecord) 
            => new CardDto(dbRecord.ID, dbRecord.Title, dbRecord.LaneID, dbRecord.Description, dbRecord.Order, dbRecord.Deadline);

        public static Card GetCard(this CardDto dto) 
            =>  new Card(dto.Id, dto.Title, dto.LaneID, dto.Deadline, dto.Description);


        
    }


}
