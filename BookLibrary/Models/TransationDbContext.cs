using System.Data.Entity;

namespace BookLibrary.Models
{
    public class TransationDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public TransationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}