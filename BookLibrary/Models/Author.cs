using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookLibrary.Models
{
    public class Author
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int Id { get; set; }

        [DisplayName("First Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }
    }
}