using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookLibrary.Models
{
    public enum TransactionType
    {
        [Display(Name = "Check IN")]
        CheckIn,

        [Display(Name = "Check OUT")]
        CheckOut
    }

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