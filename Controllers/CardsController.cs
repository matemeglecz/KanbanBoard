using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanbanBoardApi.Data;
using KanbanBoardApi.Dal;
using KanbanBoardApi.Dtos;
using System.Diagnostics.Contracts;

namespace KanbanBoardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {        
        private readonly ICardRepository cardRepository;
        private readonly ILaneRepository laneRepository;

        public CardsController(ICardRepository cardRepository, ILaneRepository laneRepository)
        {
            this.cardRepository = cardRepository;
            this.laneRepository = laneRepository;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<IEnumerable<GetCard>> GetCards() => await cardRepository.ListCards().ConfigureAwait(false);

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCard>> GetCard(int id)
        {
            var card = await cardRepository.GetCardOrNull(id).ConfigureAwait(false);
 
            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        // PUT: api/Cards/5/edit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/edit")]
        public async Task<IActionResult> EditCard(int id, EditCard newCardDto)
        {
            // ha az order és vagy a lane nem egyezik meg akkor mozgatni kell, egyébként csak frissíteni
            if (id != newCardDto.Id)
            {
                return BadRequest();
            }

            if (newCardDto is null)
            {
                throw new ArgumentNullException(nameof(newCardDto));
            }

            return await cardRepository.EditCard(newCardDto).ConfigureAwait(false) ? NoContent() : NotFound();
        }

        // PUT: api/Cards/5/move
        [HttpPut("{id}/move")]
        public async Task<IActionResult> MoveCard(int id, MoveCard moveCard)
        {
            // ha az order és vagy a lane nem egyezik meg akkor mozgatni kell, egyébként csak frissíteni
            if (id != moveCard.Id)
            {
                return BadRequest();
            }

            if (moveCard is null)
            {
                throw new ArgumentNullException(nameof(moveCard));
            }

            return await cardRepository.MoveCard(moveCard).ConfigureAwait(false) ? NoContent() : NotFound();
        }

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetCard>> PostCard(GetCard card)
        {
            if (card is null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            if (!laneRepository.LaneExists(card.LaneID))
            {
                return NotFound();
            }

            var addedCard = await cardRepository.AddCard(card).ConfigureAwait(false);

            return CreatedAtAction("GetCard", new { id = addedCard.Id }, addedCard);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id) => await cardRepository.DeleteCard(id).ConfigureAwait(false) ? NoContent() : NotFound();

    }
}
