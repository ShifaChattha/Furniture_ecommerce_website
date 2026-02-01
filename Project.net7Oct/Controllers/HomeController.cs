using Project.net7Oct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.net7Oct.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Blog()
        {
            ViewBag.Message = "Your Blog page.";

            return View();
        }
        public ActionResult Services()
        {
            ViewBag.Message = "Your Service page.";

            return View();
        }
        private Entities db = new Entities();

        public ActionResult Shop(int? categoryId)
        {
            var categories = db.Categories.ToList();
            ViewBag.Categories = categories;

            var products = db.Products.Include("Category").ToList();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
            }

            return View(products);
        }
        
         //[Authorize(Roles = "Admin,Content Writer,stock Manager")]
       
        //[Authorize(Roles = "Admin,Content writer,Stock Manager")]
        //public ActionResult AdminPannel()
        //{
        //    ViewBag.Message = "Your Service page.";
        //    return View();
        //}
        [Authorize]
        public ActionResult AdminPannel()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Stock Manager") || User.IsInRole("Content writer"))
            {
                ViewBag.Message = "Welcome to the Admin Panel.";
                return View();
            }

            TempData["Error"] = "🚫 Access Denied: This panel is only for Admins not for Custmor.";
            return RedirectToAction("Index", "Home"); // or redirect to any page you prefer
        }
        public ActionResult Thankyou()
        {
            //ViewBag.Message = "Your Service page.";

            return View();
        }
    }

}