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
        public async Task<IEnumerable<GetCard>> GetCards() => await cardRepository.ListCards();

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCard>> GetCard(int id)
        {
            var card = await cardRepository.GetCardOrNull(id);
 
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

            return await cardRepository.EditCard(newCardDto) ? NoContent() : NotFound();
        }

        // PUT: api/Cards/5/move
        [HttpPut("{id}/move")]
        public async Task<IActionResult> MoveCard(int id, MoveCard newCardDto)
        {
            // ha az order és vagy a lane nem egyezik meg akkor mozgatni kell, egyébként csak frissíteni
            if (id != newCardDto.Id)
            {
                return BadRequest();
            }

            return await cardRepository.MoveCard(newCardDto) ? NoContent() : NotFound();
        }

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetCard>> PostCard(GetCard card)
        {            
            if (!laneRepository.LaneExists(card.LaneID))
            {
                return NotFound();
            }

            var addedCard = await cardRepository.AddCard(card);

            return CreatedAtAction("GetCard", new { id = addedCard.Id }, addedCard);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id) => await cardRepository.DeleteCard(id) ? NoContent() : NotFound();

    }
}
