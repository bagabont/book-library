using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookLibrary.Models
{
    public class Category
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}