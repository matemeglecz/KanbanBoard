using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanbanBoardApi.Data;
using KanbanBoardApi.Dal;
using Microsoft.AspNetCore.Cors;

namespace KanbanBoardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanesController : ControllerBase
    {
        private readonly KanbanBoardContext _context;

        public LanesController(KanbanBoardContext context)
        {
            _context = context;
        }

        // GET: api/Lanes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lane>>> GetLanes()
        {
            return await _context.Lanes
                .Include(b => b.Cards)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Lanes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lane>> GetLane(int id)
        {
            var lane = await _context.Lanes
                .Include(b => b.Cards)
                .AsNoTracking()
                .FirstAsync(b => b.ID == id);

            if (lane == null)
            {
                return NotFound();
            }

            return lane;
        }

        // PUT: api/Lanes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLane(int id, Lane lane)
        {
            if (id != lane.ID)
            {
                return BadRequest();
            }            

            _context.Entry(lane).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LaneExists(id))
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

        // POST: api/Lanes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lane>> PostLane(Lane lane)
        {
            _context.Lanes.Add(lane);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLane), new { id = lane.ID }, lane);
        }

        // DELETE: api/Lanes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLane(int id)
        {
            var lane = await _context.Lanes.FindAsync(id);
            if (lane == null)
            {
                return NotFound();
            }

            _context.Lanes.Remove(lane);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LaneExists(int id)
        {
            return _context.Lanes.Any(e => e.ID == id);
        }
    }
}
