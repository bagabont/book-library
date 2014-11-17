using BookLibrary.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _db = new LibraryContext();

        public async Task<ActionResult> Index()
        {
            var transactions = await _db.Transactions.ToListAsync();
            return View(transactions);
        }
    }
}