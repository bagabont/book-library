using System.ComponentModel;

namespace BookLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual Category Category { get; set; }

        public virtual Author Author { get; set; }

        [DisplayName("ISBN")]
        public string Isbn { get; set; }

        public virtual ApplicationUser Owner { get; set; }
    }
}