using System;
using BookLibrary.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace BookLibrary.Data
{
    public class LibraryInitializer : DropCreateDatabaseIfModelChanges<LibraryContext>
    {
        private readonly Random _rnd = new Random();

        protected override void Seed(LibraryContext context)
        {
            var categories = new List<Category>
            {
                new Category{Name = "Science"},
                new Category{Name = "Science Fiction"},
                new Category{Name = "Fairytales"},
                new Category{Name = "Math"}
            };
            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();

            var authors = new List<Author>
            {
                new Author{FirstName = "Joanne", LastName = "Rowling"},
                new Author{FirstName = "John", LastName = "Doe"},
                new Author{FirstName = "Aleksandra", LastName = "Stone"},
                new Author{FirstName = "Peter", LastName = "Parker"},
            };
            authors.ForEach(s => context.Authors.Add(s));
            context.SaveChanges();

            var books = new List<Book>
            {
                new Book{Title= "Harry Potter", Isbn= "978-3-16-148410-0"},
                new Book{Title= "Little Mermaid", Isbn= "978-3-16-148410-1"},
                new Book{Title= "Random World", Isbn= "978-3-16-148410-2"},
                new Book{Title= "Natural Juice", Isbn= "978-3-16-148410-3"},
                new Book{Title= "Animal Planet", Isbn= "978-3-16-148410-4"},
                new Book{Title= "Space Rangers", Isbn= "978-3-16-148410-5"},
                new Book{Title= "Big Bad Raccoon", Isbn= "978-3-16-148410-6"}
            };
            books.ForEach(b => b.Author = authors[_rnd.Next(0, authors.Count - 1)]);
            books.ForEach(b => b.Category = categories[_rnd.Next(0, categories.Count - 1)]);
            books.ForEach(s => context.Books.Add(s));
            context.SaveChanges();
        }
    }
}