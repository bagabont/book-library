using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [Required]
        public virtual Author Author { get; set; }

        [Required]
        public string Isbn { get; set; }
    }
}