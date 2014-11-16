namespace BookLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }

        public string Isbn { get; set; }
    }
}