using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.net7Oct.Models;

namespace Project.net7Oct.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CartItemsController : Controller
    {
        private Entities db = new Entities();

        // GET: CartItems
        public ActionResult Index()
        {
            var cartItems = db.CartItems.Include(c => c.Cart).Include(c => c.Product);
            return View(cartItems.ToList());
        }

        // GET: CartItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        //// GET: CartItems/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CartId = new SelectList(db.Carts, "CartId", "UserId");
        //    ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
        //    return View();
        //}

        //// POST: CartItems/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CartItemId,CartId,ProductId,Quantity")] CartItem cartItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.CartItems.Add(cartItem);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CartId = new SelectList(db.Carts, "CartId", "UserId", cartItem.CartId);
        //    ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", cartItem.ProductId);
        //    return View(cartItem);
        //}

        //// GET: CartItems/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CartItem cartItem = db.CartItems.Find(id);
        //    if (cartItem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CartId = new SelectList(db.Carts, "CartId", "UserId", cartItem.CartId);
        //    ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", cartItem.ProductId);
        //    return View(cartItem);
        //}

        //// POST: CartItems/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CartItemId,CartId,ProductId,Quantity")] CartItem cartItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(cartItem).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CartId = new SelectList(db.Carts, "CartId", "UserId", cartItem.CartId);
        //    ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", cartItem.ProductId);
        //    return View(cartItem);
        //}

        //// GET: CartItems/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CartItem cartItem = db.CartItems.Find(id);
        //    if (cartItem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cartItem);
        //}

        //// POST: CartItems/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CartItem cartItem = db.CartItems.Find(id);
        //    db.CartItems.Remove(cartItem);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
