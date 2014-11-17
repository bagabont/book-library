using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    /// <summary>
    /// Represents a transaction type.
    /// </summary>
    public enum TransactionType
    {
        [Display(Name = "Check IN")]
        CheckIn,

        [Display(Name = "Check OUT")]
        CheckOut
    }
}