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
            return await _context.FieldJobs.Include(z=>z.Worker).Include(z=>z.Machine).ToListAsync();
        }

    }
}
