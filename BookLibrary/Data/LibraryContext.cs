using BookLibrary.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookLibrary.Data
{
    public class LibraryContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Gets or sets the context books
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the context authors
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the context categories.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the context transactions
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="LibraryContext"/> class.
        /// </summary>
        public LibraryContext()
            : base("DefaultConnection")
        {
        }

        /// <summary>
        /// Factory method to create a new instance of the <see cref="LibraryContext"/> class.
        /// </summary>
        /// <returns>Instance</returns>
        public static LibraryContext Create()
        {
            return new LibraryContext();
        }
    }
}