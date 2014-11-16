using System.Data.Entity;

namespace BookLibrary.Models
{
    public class CategoryDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public CategoryDbContext()
            : base("DefaultConnection")
        {

        }
    }
}