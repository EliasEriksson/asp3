using Microsoft.EntityFrameworkCore;
namespace Cat.Data
{
    public class CatContext : DbContext
    {
        public CatContext(DbContextOptions<CatContext> options) : base(options)
        {
            
        }
        public DbSet<Models.Cat> Cats { get; set; }
    }
}