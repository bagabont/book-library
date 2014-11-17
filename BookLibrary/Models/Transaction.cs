using System;
using System.Web.Mvc;

namespace BookLibrary.Models
{
    /// <summary>
    /// Represents a transaction entity.
    /// </summary>
    public class Transaction
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public virtual Book Book { get; set; }

        public DateTime Date { get; set; }

        public TransactionType Type { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}