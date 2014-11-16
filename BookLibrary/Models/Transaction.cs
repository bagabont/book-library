using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class Transaction
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public virtual Book Book { get; set; }

        //[ForeignKey("UserId")]
        //public virtual ApplicationUser User { get; set; }

        public DateTime CheckedIn { get; set; }

        public DateTime CheckedOut { get; set; }
    }
}