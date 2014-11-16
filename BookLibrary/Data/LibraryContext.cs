using BookLibrary.Models;
using System.Data.Entity;

namespace BookLibrary.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public LibraryContext()
            : base("DefaultConnection")
        {
        }
    }
}