using Microsoft.AspNet.Identity;
using Project.net7Oct.Models;
using System.Linq;
using System.Web.Mvc;

namespace Project.net7Oct.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private Entities db = new Entities();

        // ✅ Show Checkout Page (no DB write)
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var cart = db.Carts.FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index", "MYCART");
            }

            var items = db.CartItems
                          .Include("Product")
                          .Where(ci => ci.CartId == cart.CartId)
                          .ToList();

            ViewBag.Total = items.Sum(ci => ci.Quantity * ci.Product.Price);
            return View(items);
        }
    }
}