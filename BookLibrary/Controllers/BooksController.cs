using System.Data.Entity;
using System.Threading.Tasks;
using BookLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookLibrary.Models;
using Microsoft.AspNet.Identity;

namespace BookLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _db = new LibraryContext();

        // GET: /Books/
        [Authorize]
        public async Task<ActionResult> Index(string bookCategory, string searchString)
        {
            var categoryQuery = from d in _db.Books
                                orderby d.Category.Name
                                select d.Category.Name;

            var categories = await categoryQuery.Distinct().ToListAsync();
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

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CheckIn(int? id)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == id && b.Owner == null);
            if (book == null)
            {
                throw new NotImplementedException();
            }
            var currentUser = _db.Users.Find(User.Identity.GetUserId());
            book.Owner = currentUser;

            var transaction = new Transaction
            {
                Book = book,
                Date = DateTime.Now,
                Type = TransactionType.CheckIn,
                User = book.Owner
            };

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CheckOut(int id)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == id && b.Owner != null);
            if (book == null)
            {
                throw new NotImplementedException();
            }

            var transaction = new Transaction
             {
                 Book = book,
                 Date = DateTime.Now,
                 Type = TransactionType.CheckOut,
                 User = book.Owner
             };

            book.Owner = null;
            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}