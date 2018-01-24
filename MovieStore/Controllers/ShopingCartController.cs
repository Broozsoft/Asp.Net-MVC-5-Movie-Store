using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStore.Models;
using System.Net;

namespace MovieStore.Controllers
{
    public class ShopingCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShopingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Order_Movies order_movies = new Order_Movies();
            int movieId= Convert.ToInt32(id);
            var movie = db.Movies.Find(movieId);
            int orderId = Convert.ToInt32(Session["OrderId"]);
            order_movies.OrderId = orderId;
            order_movies.MovieId = movieId;
            order_movies.Price = movie.Price;
            db.Orders_Movies.Add(order_movies);
            db.SaveChanges();


            return RedirectToAction("ShopMovies", "Movies");
        }

        public ActionResult CheckOut()
        {
            return RedirectToAction("Create", "Customers");
        }
        public ActionResult Cart()
        {
            int? CartAmount = 0;
            if (Session["OrderId"] != null)
            {
                int CurrentCart = Convert.ToInt32(Session["OrderId"]);
                var checkCart = db.Orders_Movies.Where(obj => obj.OrderId == CurrentCart).Any();

                if (checkCart)
                {
                    CartAmount = (from order_movies in db.Orders_Movies
                                  where order_movies.OrderId == CurrentCart
                                  select order_movies).Count();
                }
                else
                {
                    CartAmount = 0;
                }
               
                
            }
            
            


            ViewBag.CartAmount = CartAmount;

            return PartialView();
        }

        public ActionResult Decrease(int? id)
        {
            int orderId = Convert.ToInt32(Session["OrderId"]);
            var item = db.Orders_Movies.Where(obj => obj.MovieId == id & obj.OrderId== orderId).FirstOrDefault();
            db.Orders_Movies.Remove(item);
            db.SaveChanges();
            return RedirectToAction("OpenCart", "ShopingCart", new { id = orderId });
        }

        public ActionResult Increase(int? id)
        {
            int orderId = Convert.ToInt32(Session["OrderId"]);
            var item = db.Orders_Movies.Where(obj => obj.MovieId == id & obj.OrderId == orderId).FirstOrDefault();
            db.Orders_Movies.Add(item);
            db.SaveChanges();
            return RedirectToAction("OpenCart", "ShopingCart", new { id = orderId });
        }

        public ActionResult OpenCart(int? id)
        {

            int orderId = 0;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["OrderId"] != null) {
                orderId = Convert.ToInt32(Session["OrderId"]);
            }else if(id!=null)
            {
                orderId = Convert.ToInt32(id);
            }else
            {

            }
            

            var order_Movies = (from orders_movies in db.Orders_Movies
                         where orders_movies.OrderId == orderId
                         group orders_movies by orders_movies.MovieId into g
                         select g   
                      ).ToList();

            List<KeyValuePair<int, Movie>> Movies = new List<KeyValuePair<int, Movie>>();

  

            //Dictionary<int, Movie> movies = new Dictionary<int, Movie>();
            foreach (var item in order_Movies)
            {
                int movieId = Convert.ToInt32(item.Key);
                Movie movie = db.Movies.Find(movieId);
                KeyValuePair<int, Movie> myItem = new KeyValuePair<int, Movie>(item.Count(), movie);
                Movies.Add(myItem);
            }

            return View(Movies);
        }

        public ActionResult SeeCart(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int orderId = Convert.ToInt32(id);

            var order_Movies = (from orders_movies in db.Orders_Movies
                                where orders_movies.OrderId == orderId
                                group orders_movies by orders_movies.MovieId into g
                                select g
                      ).ToList();

            List<KeyValuePair<int, Movie>> Movies = new List<KeyValuePair<int, Movie>>();



            //Dictionary<int, Movie> movies = new Dictionary<int, Movie>();
            foreach (var item in order_Movies)
            {
                int movieId = Convert.ToInt32(item.Key);
                Movie movie = db.Movies.Find(movieId);
                KeyValuePair<int, Movie> myItem = new KeyValuePair<int, Movie>(item.Count(), movie);
                Movies.Add(myItem);
            }

            return View(Movies);
        }

        // GET: ShopingCart/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShopingCart/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopingCart/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopingCart/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShopingCart/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopingCart/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShopingCart/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
