using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieStore.Models;
using MovieStore.Models.ViewModels;
using System.Web.SessionState;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MovieStore.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public OrdersController() { }
        public OrdersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        private ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }
        // GET: Orders
        [Authorize]
        public ActionResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;

            var orders = db.Orders.ToList();
            orders.OrderByDescending(obj => obj.Id);


            return View(orders.ToPagedList(currentPageIndex, 10));
            //return View(db.Orders.ToList());
        }

        public ActionResult DisplayUserName(string id)
        {
            Models.ViewModels.UserViewModel userViewModel = new Models.ViewModels.UserViewModel();
            var user=UserManager.FindById(id);
            if (user != null)
            {
                userViewModel.FirstName = user.FirstName;
            }else
            {
                int intId = Convert.ToInt32(id);
                Customer customer = db.Customers.Find(intId);
                if (customer != null)
                {
                    userViewModel.FirstName = customer.Firstname;
                }
                else
                {
                    userViewModel.FirstName = "";
                }
                
                
            }
            
            return PartialView(userViewModel);
        }
        [Authorize]
        public ActionResult PreviousOrder(int? page)
        {
            string UserId= User.Identity.GetUserId();
            int currentPageIndex = page.HasValue ? page.Value : 1;
                var orders = db.Orders.Where(obj=>obj.CustomerId== UserId).ToList();
            orders.OrderByDescending(obj => obj.Id);


            return View(orders.ToPagedList(currentPageIndex, 10));
            //return View(db.Orders.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult Create([Bind(Include = "Id,OrderDate,Customer_id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult Edit([Bind(Include = "Id,OrderDate,Customer_id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            try
            {
                db.Orders.Remove(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
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
