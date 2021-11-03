using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanbanBoardApi.Data;
using KanbanBoardApi.Models;

namespace KanbanBoardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly KanbanBoardContext _context;

        public CardsController(KanbanBoardContext context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(int id, Card newCard)
        {
            //TODO ha az order és vagy a board nem egyezik meg akkor mozgatni kell, egyébként csak frissíteni
            if (id != newCard.ID)
            {
                return BadRequest();
            }
            if (!CardExists(id) || !DestinationBoardExists(newCard.BoardID))
            {
                return NotFound();
            }

            var oldCard = await _context.Cards.FindAsync(id);
            _context.Entry(oldCard).State = EntityState.Detached;
            
            if(oldCard.BoardID != newCard.BoardID)
            {
                //TODO régi oszlopban aki mögötte volt fel, új oszlopban aki mögötte van le                
                var cardsOld = await _context.Cards.Where(c => c.BoardID == oldCard.BoardID).Where(c => c.ID != oldCard.ID).ToListAsync();
                MoveUpCards(cardsOld, oldCard.Order);                
                var cardsNew = await _context.Cards.Where(c => c.BoardID == newCard.BoardID).Where(c => c.ID != oldCard.ID).ToListAsync();
                MoveDownCards(cardsNew, newCard.Order);
                
            } else if(oldCard.Order != newCard.Order)
            {
                //TODO eredeti hely mögött van fel, új hely mögött van le                
                var cards = await _context.Cards.Where(c => c.BoardID == oldCard.BoardID).Where(c => c.ID != oldCard.ID).ToListAsync();
                MoveUpCards(cards, oldCard.Order);
                MoveDownCards(cards, newCard.Order);                
            }

            if (newCard.Description == null)
                newCard.Description = "";            

            _context.Entry(newCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {            
            if (!DestinationBoardExists(card.BoardID))
            {
                return NotFound();
            }

            try
            {
                var maxOrder = _context.Cards.Where(c => c.BoardID == card.BoardID).Max(c => c.Order);
                card.Order = maxOrder + 1;
            }
            catch
            {
                card.Order = 0;
            }
            if (card.Description == null)
                card.Description = "";            

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCard", new { id = card.ID }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            //TODO mozgatni a többit mert eltűnik
            var deletedCard = await _context.Cards.FindAsync(id);

            var cards = await _context.Cards.Where(c => c.BoardID == deletedCard.BoardID).Where(c => c.ID != deletedCard.ID).ToListAsync();
            MoveUpCards(cards, deletedCard.Order);

            if (deletedCard == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(deletedCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.ID == id);
        }

        private bool DestinationBoardExists(int id)
        {
            return _context.Boards.Any(e => e.ID == id);      
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
                if(c.Order >= limit)
                    c.Order += 1;
            });
        }
    }
}
