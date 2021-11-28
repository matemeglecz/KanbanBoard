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

        public async Task<IReadOnlyCollection<GetCard>> ListCards()
        {
            return await db.Cards
                .Select(dbRecord => dbRecord.GetCardDto())
                .AsNoTracking()
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public async Task<GetCard> GetCardOrNull(int id)
        {
            var card = await db.Cards.FirstOrDefaultAsync(c => c.ID == id).ConfigureAwait(false);
            return card?.GetCardDto();
        }

        public async Task<GetCard> AddCard(GetCard cardDto)
        {
            if (cardDto is null)
            {
                throw new System.ArgumentNullException(nameof(cardDto));
            }

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

            await db.Cards.AddAsync(newCard).ConfigureAwait(false);
            await db.SaveChangesAsync().ConfigureAwait(false);

            return newCard.GetCardDto();
        }

        public async Task<bool> DeleteCard(int id)
        {            
            var deletedCard = await db.Cards.FindAsync(id).ConfigureAwait(false);
            if (deletedCard == null) 
                return false;

            var cards = await db.Cards.Where(c => c.LaneID == deletedCard.LaneID).Where(c => c.ID != deletedCard.ID).ToListAsync().ConfigureAwait(false);
            MoveUpCards(cards, deletedCard.Order);

            db.Cards.Remove(deletedCard);

                
            await db.SaveChangesAsync().ConfigureAwait(false);
            return true;                
            
        }

        public async Task<bool> EditCard(EditCard newCardDto)
        {
            if (newCardDto is null)
            {
                throw new System.ArgumentNullException(nameof(newCardDto));
            }

            if (!CardExists(newCardDto.Id))
            {
                return false;
            }

            var card = await db.Cards.FindAsync(newCardDto.Id).ConfigureAwait(false);
            db.Entry(card).State = EntityState.Modified;            

            if(newCardDto.Title != null)
                card.Title = newCardDto.Title;
            if (newCardDto.Description!= null)
                card.Description = newCardDto.Description;
            if (newCardDto.Deadline != null)
                card.Deadline = newCardDto.Deadline;

            return await ModifiedSaveChanges(newCardDto.Id).ConfigureAwait(false);
        }

        public async Task<bool> MoveCard(MoveCard newCardDto)
        {
            if (newCardDto is null)
            {
                throw new System.ArgumentNullException(nameof(newCardDto));
            }

            if (!CardExists(newCardDto.Id) || !laneRepository.LaneExists(newCardDto.LaneID))
            {
                return false;
            }

            var card = await db.Cards.FindAsync(newCardDto.Id).ConfigureAwait(false);
            db.Entry(card).State = EntityState.Modified;

            if (card.LaneID != newCardDto.LaneID)
            {
                // régi oszlopban aki mögötte volt fel, új oszlopban aki mögötte van le                
                var cardsOld = await db.Cards.Where(c => c.LaneID == card.LaneID).Where(c => c.ID != card.ID).ToListAsync().ConfigureAwait(false);
                MoveUpCards(cardsOld, card.Order);
                var cardsNew = await db.Cards.Where(c => c.LaneID == newCardDto.LaneID).Where(c => c.ID != card.ID).ToListAsync().ConfigureAwait(false);
                MoveDownCards(cardsNew, newCardDto.Order);

            }
            else if (card.Order != newCardDto.Order)
            {
                // eredeti hely mögött van fel, új hely mögött van le                
                var cards = await db.Cards.Where(c => c.LaneID == card.LaneID).Where(c => c.ID != card.ID).ToListAsync().ConfigureAwait(false);
                MoveUpCards(cards, card.Order);
                MoveDownCards(cards, newCardDto.Order);
            }

            card.LaneID = newCardDto.LaneID;
            card.Order = newCardDto.Order;

            return await ModifiedSaveChanges(newCardDto.Id).ConfigureAwait(false);
        }

        public bool CardExists(int id)
        {
            return db.Cards.Any(e => e.ID == id);
        }

        private static void MoveUpCards(List<Card> cards, int limit)
        {
            cards.ForEach(c =>
            {
                if (c.Order >= limit)
                    c.Order -= 1;
            });
        }

        private static void MoveDownCards(List<Card> cards, int limit)
        {
            cards.ForEach(c =>
            {
                if (c.Order >= limit)
                    c.Order += 1;
            });
        }

        private async Task<bool> ModifiedSaveChanges(int id)
        {
            try
            {
                await db.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

    }    

    internal static class CardRepositoryExtensions
    {
        public static GetCard GetCardDto(this Card dbRecord) 
            => new GetCard(dbRecord.ID, dbRecord.Title, dbRecord.LaneID, dbRecord.Description, dbRecord.Order, dbRecord.Deadline);

        public static Card GetCard(this GetCard dto) 
            =>  new Card(dto.Id, dto.Title, dto.LaneID, dto.Deadline, dto.Description);


        
    }
}
