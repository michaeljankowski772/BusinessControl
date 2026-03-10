using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessControlService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkersController : ControllerBase
    {
        /*private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];*/

        private readonly AppDbContext _context;
        public WorkersController(AppDbContext context)
        {
            _context = context;
        }

        /*[HttpGet(Name = "GetWorkers")]
        public async Task<ActionResult<IEnumerable<Worker>>> GetWorkers()
        {
            return await _context.Workers.ToListAsync();
        }*/
        [HttpGet(Name = "GetWorkers")]
        public async Task<ActionResult<IEnumerable<Worker>>> GetWorkers()
        {
            return await _context.Workers.ToListAsync();
        }

    }
}
