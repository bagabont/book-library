using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookLibrary.Models;

namespace BookLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookDbContext _db = new BookDbContext();

        public ActionResult Index()
        {
            IEnumerable<Book> books = null;
            if (_db.Books != null)
            {
                books = _db.Books.ToList();
            }
            return View(books);
        }
    }
}