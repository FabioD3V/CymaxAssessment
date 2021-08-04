using Microsoft.EntityFrameworkCore;

namespace CymaxAssessmentAPI.Models
{
    /// <summary>
    /// This is just a simple DbContext class where we would make more 
    /// use in real world scenarios where we would have connection with databases.
    /// </summary>
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
