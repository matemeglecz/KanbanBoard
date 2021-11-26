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
        private readonly KanbanBoardContext _context;
        private readonly ICardRepository cardRepository;
        private readonly ILaneRepository laneRepository;

        public CardsController(KanbanBoardContext context, ICardRepository cardRepository, ILaneRepository laneRepository)
        {
            _context = context;
            this.cardRepository = cardRepository;
            this.laneRepository = laneRepository;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<IEnumerable<CardDto>> GetCards() => await cardRepository.GetCards();

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDto>> GetCard(int id)
        {
            var card = await cardRepository.GetCardOrNull(id);
            Console.WriteLine(card.Title);

            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(int id, CardDto newCardDto)
        {
            // ha az order és vagy a lane nem egyezik meg akkor mozgatni kell, egyébként csak frissíteni
            if (id != newCardDto.Id)
            {
                return BadRequest();
            }                                                               

            return await cardRepository.UpdateCard(newCardDto) ? NoContent() : NotFound();
        }

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(CardDto card)
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
        public async Task<IActionResult> DeleteCard(int id)
        {
            var res = await cardRepository.DeleteCard(id);
            if (!res)
                return NotFound();

            return NoContent();
        }
        
    }
}
