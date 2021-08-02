using Microsoft.EntityFrameworkCore;

namespace CymaxAssessmentAPI.Models
{
    public class CartonContext : DbContext
    {
        public CartonContext(DbContextOptions<CartonContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Carton> Cartons { get; set; }

        
    }
}
