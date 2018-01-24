using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieStore.Models;
using Microsoft.AspNet.Identity;

namespace MovieStore.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        [Authorize(Roles ="Admin")]
        public ActionResult Index() { 
                
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult CheckOut(int? CustomerId)
        {
            if (Session["OrderId"] == null)
            {
                return RedirectToAction("ShopMovies", "Movies");
            }
            Order order = new Order();
            if (User.Identity.IsAuthenticated)
            {
                int orderId = Convert.ToInt32(Session["OrderId"]);
                string UserId= User.Identity.GetUserId();
                order = db.Orders.Find(orderId);
                order.CustomerId = UserId;
                order.OrderDate = DateTime.Now;

                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                CheckOutStatus(orderId);
            }
            else
            {
                int orderId = Convert.ToInt32(Session["OrderId"]);
                order = db.Orders.Find(orderId);
                order.CustomerId = CustomerId.ToString();
                order.OrderDate = DateTime.Now;

                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                CheckOutStatus(orderId);
            }



            Session["OrderId"] = null;
            return View(order);
        }

        public ActionResult CheckOutStatus(int? OrderId)
        {

            return View();
        }
        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Firstname,Lastname,BillingAddress,BillingZip,BillingCity,EmailAddress,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                CheckOut(customer.Id);
                return RedirectToAction("Index","Movies");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Firstname,Lastname,BillingAddress,BillingZip,BillingCity,EmailAddress,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
