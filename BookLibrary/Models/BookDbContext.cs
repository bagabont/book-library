using System.Data.Entity;

namespace BookLibrary.Models
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookDbContext()
            : base("DefaultConnection")
        {
        }
    }
}