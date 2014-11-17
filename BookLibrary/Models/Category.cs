using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookLibrary.Models
{
    public class Category
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Name { get; set; }
    }
}