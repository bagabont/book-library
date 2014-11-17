using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookLibrary.Controllers
{
    /// <summary>
    /// Controller for books.
    /// </summary>
    public class BooksController : Controller
    {
        private readonly LibraryContext _db = new LibraryContext();

        // GET: /Books/
        [Authorize]
        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<ActionResult> Index(string bookCategory, string searchString)
        {
            // select all books from the DB
            var allBooks = _db.Books;

            // get categories of all books
            var categoryQuery = from b in allBooks
                                orderby b.Category.Name
                                select b.Category.Name;

            // select distinct categories
            var categories = await categoryQuery.Distinct().ToListAsync();
            ViewBag.bookCategory = new SelectList(categories);

            var books = from b in allBooks
                        select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = Filter(books, searchString);
            }

            if (!string.IsNullOrEmpty(bookCategory))
            {
                // filter books by category
                books = books.Where(x => x.Category.Name == bookCategory);
            }
            ViewBag.Title = "All Books";
            return View(books);
        }

        // GET: /Books/Available
        [Authorize]
        public async Task<ActionResult> Available(string bookCategory, string searchString)
        {
            // Find all books which have no owner.
            var availableBooks = _db.Books.Where(b => b.Owner == null);

            // get categories of all available books
            var categoryQuery = from b in availableBooks
                                orderby b.Category.Name
                                select b.Category.Name;

            var categories = await categoryQuery.Distinct().ToListAsync();
            ViewBag.bookCategory = new SelectList(categories);

            var books = from b in availableBooks
                        select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = Filter(books, searchString);
            }

            if (!string.IsNullOrEmpty(bookCategory))
            {
                books = books.Where(x => x.Category.Name == bookCategory);
            }
            ViewBag.Title = "Available Books";
            return View("Index", books);
        }

        // GET: /Books/Borrowed
        [Authorize]
        public async Task<ActionResult> Borrowed(string bookCategory, string searchString)
        {
            // get the current user
            var currentUser = _db.Users.Find(User.Identity.GetUserId());

            // select all books borrowed by the current user
            var borrowedBooks = _db.Books.Where(b => b.Owner.UserName == currentUser.UserName);

            var categoryQuery = from b in borrowedBooks
                                orderby b.Category.Name
                                select b.Category.Name;
            // select all categories of borrowed books
            var categories = await categoryQuery.Distinct().ToListAsync();
            ViewBag.bookCategory = new SelectList(categories);

            var books = from b in borrowedBooks
                        select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = Filter(books, searchString);
            }

            if (!string.IsNullOrEmpty(bookCategory))
            {
                books = books.Where(x => x.Category.Name == bookCategory);
            }
            ViewBag.Title = "My Books";
            return View("Index", books);
        }

        // GET: /Books/Create/
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Create()
        {
            var authors = await _db.Authors.ToListAsync();
            ViewBag.AuthorsList = authors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FirstName + " " + a.LastName
            });
            return View();
        }

        // POST: /Books/Create/
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Title,Category,Author,Isbn,Rating")] Book book)
        {
            if (!ModelState.IsValid)
            {
                // Invalid data, simply return to index view.
                return View("Index");
            }
            var authorId = ModelState["Author.Id"].Value.ConvertTo(typeof(int));
            book.Author = _db.Authors.Find(authorId);
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: /Books/CheckIn/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckIn(int? id)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == id && b.Owner == null);
            if (book == null)
            {
                // Invalid data, simply return to index view.
                return View("Index");
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

            return RedirectToAction("Borrowed");
        }

        // POST: /Books/CheckOut/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckOut(int id)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == id && b.Owner != null);
            if (book == null)
            {
                // Invalid data, simply return to index view.
                return View("Index");
            }

            // log transaction to history
            var transaction = new Transaction
             {
                 Book = book,
                 Date = DateTime.Now,
                 Type = TransactionType.CheckOut,
                 User = book.Owner
             };
            _db.Transactions.Add(transaction);

            // release book for further borrowing
            book.Owner = null;
            await _db.SaveChangesAsync();

            return RedirectToAction("Borrowed");
        }

        /// <summary>
        /// Filter books by title, ISBN, author's first and last name
        /// </summary>
        /// <param name="books">Books collection</param>
        /// <param name="searchString">Filter</param>
        /// <returns></returns>
        private static IQueryable<Book> Filter(IQueryable<Book> books, string searchString)
        {
            return books.Where(s => s.Title.Contains(searchString) ||
                   s.Isbn.Contains(searchString) ||
                   s.Author.FirstName.Contains(searchString) ||
                   s.Author.LastName.Contains(searchString));
        }

    }
}