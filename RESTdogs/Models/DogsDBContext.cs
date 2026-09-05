using Microsoft.EntityFrameworkCore;

namespace RESTdogs.Models
{
    public class DogsDBContext : DbContext
    {
        public DogsDBContext(DbContextOptions<DogsDBContext> options) : base(options)
        {
        }
        public DbSet<Dog> Dogs { get; set; }
    }
}
