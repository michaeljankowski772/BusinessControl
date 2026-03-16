using BusinessControlService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BusinessControlService
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<WorkshopJob> WorkshopJobs { get; set; }
        public DbSet<MachineJob> MachineJobs { get; set; }
        public DbSet<FieldJob> FieldJobs { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

    }
}
