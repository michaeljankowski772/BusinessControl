using Microsoft.EntityFrameworkCore;

namespace BusinessControlService
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<WorkshopJob> Jobs { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
