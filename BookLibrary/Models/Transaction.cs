using System;

namespace BookLibrary.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public Book Book { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CheckedIn { get; set; }

        public DateTime CheckedOut { get; set; }

        public Transaction(Book book, ApplicationUser user)
        {
            Book = book;
            User = user;
            CheckedIn = DateTime.Now;
            CheckedOut = DateTime.MinValue;
        }
    }
}