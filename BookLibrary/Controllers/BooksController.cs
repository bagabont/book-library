using System.Data.Entity;
using System.Net;
using BookLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookLibrary.Models;

namespace BookLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _db = new LibraryContext();

        // GET: /Books/
        public ActionResult Index(string bookCategory, string searchString)
        {
            var categories = new List<string>();

            var categoryQuery = from d in _db.Books
                                orderby d.Category.Name
                                select d.Category.Name;

            categories.AddRange(categoryQuery.Distinct());
            ViewBag.bookCategory = new SelectList(categories);

            var books = from m in _db.Books
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(bookCategory))
            {
                books = books.Where(x => x.Category.Name == bookCategory);
            }

            return View(books);
        }

        public ActionResult CheckIn(int id)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == id && b.IsAvailable);
            if (book == null)
            {
                throw new NotImplementedException();
            }

            book.IsAvailable = false;
            _db.SaveChanges();

            var transaction = new Transaction
            {
                Book = book,
                Date = DateTime.Now,
                Type = TransactionType.CheckIn
            };
            _db.Transactions.Add(transaction);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult CheckOut(int id)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == id && !b.IsAvailable);
            if (book == null)
            {
                throw new NotImplementedException();
            }

            book.IsAvailable = true;
            _db.SaveChanges();

            var transaction = new Transaction
            {
                Book = book,
                Date = DateTime.Now,
                Type = TransactionType.CheckOut
            };
            _db.Transactions.Add(transaction);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}