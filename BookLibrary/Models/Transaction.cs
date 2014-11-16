using System;

namespace BookLibrary.Models
{
    public enum TransactionType
    {
        CheckIn,
        CheckOut
    }

    public class Transaction
    {
        public int Id { get; set; }

        public virtual Book Book { get; set; }

        public DateTime Date { get; set; }

        public TransactionType Type { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}