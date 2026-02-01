using Microsoft.AspNet.Identity;
using Project.net7Oct.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.net7Oct.Controllers
{
    [Authorize]
    public class MYCARTController : Controller

    {

          
      
            private Entities db = new Entities();
            //private YourDbContext db = new YourDbContext();

            // ✅ Add Product to Cart
            [HttpPost]
            [AllowAnonymous]
            public ActionResult AddToCart(int productId)
            {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    TempData["Error"] = "Please login first to add items to your cart.";
            //    return RedirectToAction("Login", "Account");
            //}
            if (!User.Identity.IsAuthenticated)
            {
                // Store the message and target page in TempData
                TempData["Error"] = "Please login first to add items to your cart.";

                // Redirect to login with a returnUrl pointing to the Shop page
                return RedirectToAction("Login", "Account", new
                {
                    returnUrl = Url.Action("Shop", "Home")
                });
            }
           


            string userId = User.Identity.GetUserId();

                var cart = db.Carts.FirstOrDefault(c => c.UserId == userId);
                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = userId,
                        CreatedDate = DateTime.Now
                    };
                    db.Carts.Add(cart);
                    db.SaveChanges();
                }

                var product = db.Products.Find(productId);
                if (product == null)
                    return HttpNotFound();

                if (product.StockQuantity <= 0)
                {
                    TempData["Error"] = "This product is out of stock.";
                    return RedirectToAction("Index", "Products");
                }

                var cartItem = db.CartItems.FirstOrDefault(ci => ci.CartId == cart.CartId && ci.ProductId == productId);

                if (cartItem != null)
                {
                    if (cartItem.Quantity + 1 > product.StockQuantity)
                    {
                        TempData["Error"] = "Cannot add more than available stock.";
                    }
                    else
                    {
                        cartItem.Quantity++;
                    }
                }
                else
                {
                    cartItem = new CartItem
                    {
                        CartId = cart.CartId,
                        ProductId = product.ProductId,
                        Quantity = 1
                    };
                    db.CartItems.Add(cartItem);
                }


            
            db.SaveChanges();
                TempData["Success"] = "Product added to cart successfully!";
                return RedirectToAction("Index");
            }

            // ✅ Show User's Cart
            public ActionResult Index()
            {
                string userId = User.Identity.GetUserId();
                var cart = db.Carts.FirstOrDefault(c => c.UserId == userId);

                if (cart == null)
                {
                    ViewBag.Total = 0;
             
                return View(new List<CartItem>());

                }

                var items = db.CartItems
                              .Include("Product")
                              .Where(ci => ci.CartId == cart.CartId)
                              .ToList();

                // 🧮 Calculate subtotal at runtime
                foreach (var item in items)
                {
                    item.Product.Price = item.Product.Price; // just ensures product is loaded
                }

                // 🧮 Total price calculation
                ViewBag.Total = items.Sum(ci => ci.Quantity * ci.Product.Price);

                return View(items);
            }

            // ✅ Increase Quantity
            [HttpPost]
            public ActionResult IncreaseQuantity(int cartItemId)
            {
                var item = db.CartItems.Find(cartItemId);
                if (item == null) return HttpNotFound();

                var product = db.Products.Find(item.ProductId);
                if (item.Quantity + 1 > product.StockQuantity)
                {
                    TempData["Error"] = "Cannot add more than available stock.";
                    return RedirectToAction("Index");
                }

                item.Quantity++;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // ✅ Decrease Quantity
            [HttpPost]
            public ActionResult DecreaseQuantity(int cartItemId)
            {
                var item = db.CartItems.Find(cartItemId);
                if (item == null) return HttpNotFound();

                if (item.Quantity > 1)
                {
                    item.Quantity--;
                    db.SaveChanges();
                }
                else
                {
                    db.CartItems.Remove(item);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            // ✅ Remove Item from Cart
            [HttpPost]
            public ActionResult RemoveItem(int cartItemId)
            {
                var item = db.CartItems.Find(cartItemId);
                if (item == null) return HttpNotFound();

                db.CartItems.Remove(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetCartCount()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            string userId = User.Identity.GetUserId();
            var cart = db.Carts.FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
                return Json(0, JsonRequestBehavior.AllowGet);

            //int count = db.CartItems
            //              .Where(ci => ci.CartId == cart.CartId)
            //              .Sum(ci => ci.Quantity);

            int count = db.CartItems
              .Where(ci => ci.CartId == cart.CartId)
              .Sum(ci => (int?)ci.Quantity) ?? 0;

            return Json(count, JsonRequestBehavior.AllowGet);
        }

    }
    }

