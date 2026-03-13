using BusinessControlService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessControlService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkshopJobController : ControllerBase
    {
        private readonly AppDbContext _context;
        public WorkshopJobController(AppDbContext context)
        {
            _context = context;
        }

        //[Authorize]
        [HttpGet("GetWorkshopJobsFull")]
        public async Task<ActionResult<IEnumerable<WorkshopJob>>> GetWorkshopJobsWithWorkers()
        {
            return await _context.WorkshopJobs.Include(z=>z.Worker).ToListAsync();
        }

        //[Authorize]
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
