using BusinessControlService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BusinessControlService.Controllers
{
    [ApiController]
    [Route("fieldjobs")]
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
        [HttpGet("getfieldjobs")]
        public async Task<ActionResult<IEnumerable<FieldJob>>> GetFieldJobs()
        {
            return await _context.FieldJobs.ToListAsync();
        }

        [Authorize]
        [HttpGet("getfieldjobswithheaders")]
        public async Task<ActionResult> GetFieldJobsWithHeaders()
        {
            var jobs = await _context.FieldJobs.Select(z => new FieldJobDTO
            {
                Id = z.Id,
                CustomerId = z.CustomerId,
                WorkerId = z.WorkerId,
                MachineId = z.MachineId,
                CustomerFirstName = z.Customer != null ? z.Customer.FirstName : "",
                CustomerLastName = z.Customer != null ? z.Customer.LastName : "",
                WorkerFirstName = z.Worker != null ? z.Worker.FirstName : "",
                WorkerLastName = z.Worker != null ? z.Worker.LastName : "",
                MachineName = z.Machine != null ? z.Machine.MachineName : "",
                FieldArea = z.FieldArea
            }).ToListAsync();

            var columns = new List<string> { "Id", "CustomerFirstName", "CustomerLastName", "WorkerFirstName", "WorkerLastName", "FieldArea" };

            var result = new
            {
                columns,
                data = jobs
            };

            return Ok(result);
        }

        [Authorize]
        [HttpPost("SetFieldJob")]
        public async Task<ActionResult<FieldJob>> SetFieldJob([FromBody] FieldJobDTO fieldJobDTO)
        {
            if (fieldJobDTO == null)
                return BadRequest(new { Message = "FieldJob is null" });

            var existing = await _context.FieldJobs.FindAsync(fieldJobDTO.Id);

            if (existing == null)
            {
                var newJob = new FieldJob
                {
                    MachineId = fieldJobDTO.MachineId,
                    FieldArea = fieldJobDTO.FieldArea,
                    CustomerId = fieldJobDTO.CustomerId,
                    WorkerId = fieldJobDTO.WorkerId
                };

                await _context.FieldJobs.AddAsync(newJob);
                await _context.SaveChangesAsync();

                return Ok(newJob);
            }
            else
            {
                existing.MachineId = fieldJobDTO.MachineId;
                existing.FieldArea = fieldJobDTO.FieldArea;
                existing.CustomerId = fieldJobDTO.CustomerId;
                existing.WorkerId = fieldJobDTO.WorkerId;

                await _context.SaveChangesAsync();

                return Ok(existing);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<FieldJobDTO>> GetFieldJob(int id)
        {
            if (id == 0)
            {
                return BadRequest(new { Message = "Id is 0" });
            }

            var job = await _context.FieldJobs.Select
                (z => new FieldJobDTO
                {
                    Id = z.Id,
                    CustomerId = z.CustomerId,
                    WorkerId = z.WorkerId,
                    MachineId = z.MachineId,
                    CustomerFirstName = z.Customer != null ? z.Customer.FirstName : "",
                    CustomerLastName = z.Customer != null ? z.Customer.LastName : "",
                    WorkerFirstName = z.Worker != null ? z.Worker.FirstName : "",
                    WorkerLastName = z.Worker != null ? z.Worker.LastName : "",
                    MachineName = z.Machine != null ? z.Machine.MachineName : "",
                    FieldArea = z.FieldArea
                }).SingleAsync(z => z.Id == id);

            var columns = new List<string> { "Id", "CustomerFirstName", "CustomerLastName", "WorkerFirstName", "WorkerLastName", "FieldArea" };

            return job;
        }

    }
}
