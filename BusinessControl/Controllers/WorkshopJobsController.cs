using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessControlService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkshopJobsController : ControllerBase
    {
        /*private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];*/

        private readonly AppDbContext _context;
        public WorkshopJobsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetWorkshopJobsFull")]
        public async Task<ActionResult<IEnumerable<WorkshopJob>>> GetWorkshopJobsWithWorkers()
        {
            return await _context.WorkshopJobs.Include(z=>z.Worker).ToListAsync();
        }

        [HttpGet("GetWorkshopJobs")]
        public async Task<ActionResult<IEnumerable<WorkshopJob>>> GetWorkshopJobs()
        {
             var jobs = await _context.WorkshopJobs.
                Select(z => new
                {
                    z.Id,
                    z.DateStart,
                    z.DateEnd,
                    z.Description,
                    WorkerName = z.Worker.FirstName
                }).ToListAsync();
            return Ok(jobs);
        }

    }
}
