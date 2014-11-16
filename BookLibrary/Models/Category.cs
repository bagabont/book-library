using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}