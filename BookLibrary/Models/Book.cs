using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookLibrary.Models
{
    public class Book
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        public virtual Author Author { get; set; }

        [Required]
        [DisplayName("ISBN")]
        [RegularExpression(@"((978[\--– ])?[0-9][0-9\--– ]{10}[\--– ][0-9xX])|((978)?[0-9]{9}[0-9Xx])", ErrorMessage = "Value is not a valid ISBN")]
        public string Isbn { get; set; }

        public virtual ApplicationUser Owner { get; set; }
    }
}