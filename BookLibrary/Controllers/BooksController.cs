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

        // GET: /Books/Create/
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
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
                return View("Index");
            }
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

        // POST: /Books/CheckOut/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
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