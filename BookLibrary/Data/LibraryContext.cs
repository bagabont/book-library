using BookLibrary.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookLibrary.Data
{
    public class LibraryContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public LibraryContext()
            : base("DefaultConnection")
        {
        }

        public static LibraryContext Create()
        {
            return new LibraryContext();
        }
    }
}