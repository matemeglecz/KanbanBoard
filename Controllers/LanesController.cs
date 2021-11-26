using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KanbanBoardApi.Dal;
using KanbanBoardApi.Dtos;

namespace KanbanBoardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanesController : ControllerBase
    {
        //private readonly KanbanBoardContext _context;
        private readonly ILaneRepository laneRepository;

        public LanesController(ILaneRepository laneRepository)
        {
            this.laneRepository = laneRepository;
        }

        // GET: api/Lanes
        [HttpGet]
        public async Task<IEnumerable<GetLaneDto>> GetLanes() => await laneRepository.ListLanes();

        // GET: api/Lanes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetLaneDto>> GetLane(int id)
        {
            var lane = await laneRepository.GetLaneOrNull(id);

            if (lane == null)
            {
                return NotFound();
            }

            return Ok(lane);
        }

        // PUT: api/Lanes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       /* [HttpPut("{id}")]
        public async Task<IActionResult> PutLane(int id, AddEditLaneDto lane)
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
        }*/

        // POST: api/Lanes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetLaneDto>> PostLane(AddLaneDto laneDto)
        {
            var lane = await laneRepository.AddLane(laneDto);
            return CreatedAtAction(nameof(GetLane), new { id = lane.ID }, lane);
        }

        // DELETE: api/Lanes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLane(int id)
        {         
            return await laneRepository.DeleteLane(id) ? NoContent() : NotFound();
        }
      
    }
}
