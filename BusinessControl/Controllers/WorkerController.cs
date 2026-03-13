using BusinessControlService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessControlService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkerController : ControllerBase
    {

        private readonly AppDbContext _context;
        public WorkerController(AppDbContext context)
        {
            _context = context;
        }

        /*[HttpGet(Name = "GetWorkers")]
        public async Task<ActionResult<IEnumerable<Worker>>> GetWorkers()
        {
            return await _context.Workers.ToListAsync();
        }*/

        [Authorize]
        [HttpGet(Name = "GetWorkers")]
        public async Task<ActionResult<IEnumerable<Worker>>> GetWorkers()
        {
            return await _context.Workers.ToListAsync();
        }

    }
}
