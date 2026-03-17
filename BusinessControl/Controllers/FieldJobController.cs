using BusinessControlService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessControlService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FieldJobController : ControllerBase
    {

        private readonly AppDbContext _context;
        public FieldJobController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("GetFieldJobsFull")]
        public async Task<ActionResult<IEnumerable<FieldJob>>> GetFieldJobsWithWorkers()
        {
            return await _context.FieldJobs.Include(z => z.Worker).Include(z => z.Machine).ToListAsync();
        }

        [Authorize]
        [HttpPost("SetFieldJob")]
        public async Task<ActionResult<IEnumerable<FieldJob>>> SetFieldJob([FromBody] FieldJob fieldJob)
        {
            //todo validation
            if (fieldJob == null)
                return BadRequest(new { Message = "FieldJob is null" });

            var existing = await _context.FieldJobs.FindAsync(fieldJob.Id);

            if (existing == null)
            {
                _context.FieldJobs.Add(fieldJob);
            }
            else
            {
                //add mapping maybe?
                _context.FieldJobs.Update(fieldJob);
            }
           
            await _context.SaveChangesAsync();
            return Ok(fieldJob);
        }

    }
}
