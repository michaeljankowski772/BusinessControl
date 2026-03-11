using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessControlService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FieldJobsController : ControllerBase
    {
        /*private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];*/

        private readonly AppDbContext _context;
        public FieldJobsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetFieldJobsFull")]
        public async Task<ActionResult<IEnumerable<FieldJob>>> GetFieldJobsWithWorkers()
        {
            return await _context.FieldJobs.Include(z=>z.Worker).Include(z=>z.Machine).ToListAsync();
        }

    }
}
