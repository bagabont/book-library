using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}