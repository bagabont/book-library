using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookLibrary.Models
{
    public class Book
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Title { get; set; }

        public virtual Category Category { get; set; }

        public virtual Author Author { get; set; }

        [DisplayName("ISBN")]
        public string Isbn { get; set; }

        public virtual ApplicationUser Owner { get; set; }
    }
}