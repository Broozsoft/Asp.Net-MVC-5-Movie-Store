using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieStore.Models;
using System.IO;

namespace MovieStore.Controllers
{
    public class MoviesController : Controller
    {

        
        private ApplicationDbContext db = new ApplicationDbContext();
        public MoviesController()
        {
        }
        // GET: Movies
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        public ActionResult ShopMovies()
        {
            var populars = from orders_movie in db.Orders_Movies
                           group orders_movie by orders_movie.MovieId into pop
                           select new { count=pop.Count(), key=pop.Key };
            IDictionary<int, int> Movie = new Dictionary<int, int>();
            List<Movie> PopularMovies = new List<Movie>();
            foreach (var movie in populars)
            {
                Movie.Add(movie.key,movie.count);
            }

            var sorted = from sortedMovies in Movie
                    orderby sortedMovies.Value descending
                    select sortedMovies;


            foreach (var item in sorted)
            {
                var it = db.Movies.Find(item.Key);
                PopularMovies.Add(it);
            }

            return View(PopularMovies.ToList());
        }

        public ActionResult PopularMovies()
        {
            var populars = from orders_movie in db.Orders_Movies
                           group orders_movie by orders_movie.MovieId into pop
                           select new { count = pop.Count(), key = pop.Key };
            IDictionary<int, int> Movie = new Dictionary<int, int>();
            List<Movie> PopularMovies = new List<Movie>();
            foreach (var movie in populars)
            {
                Movie.Add(movie.key, movie.count);
            }

            var sorted = from sortedMovies in Movie
                         orderby sortedMovies.Value descending
                         select sortedMovies;


            foreach (var item in sorted)
            {
                var it = db.Movies.Find(item.Key);
                PopularMovies.Add(it);
            }

            return View(PopularMovies.ToList());
        }
        public ActionResult LatestMovies()
        {
            return View(db.Movies.ToList());
        }

        public ActionResult OldestMovies()
        {
            var latest = db.Movies.OrderByDescending(obj => obj.ReleaseYear).ToList();
            return View(latest);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Title,Director,ReleaseYear,Price,image")] Movie movie, HttpPostedFileBase image)
        {
            string _FileName = "";
            string extension = "";
            string imageName = "";
            try
            {
                if (image.ContentLength > 0)
                {
                    extension = Path.GetExtension(image.FileName);
                    _FileName = Path.GetFileName(image.FileName);
                    imageName = movie.Title + extension;
                    string _path = Path.Combine(Server.MapPath("~/Upload"), imageName);
                    image.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                
            }

            if (ModelState.IsValid)
            {
                movie.image = imageName;
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Title,Director,ReleaseYear,Price,image")] Movie movie, HttpPostedFileBase image)
        {
            string _FileName = "";
            string extension = "";
            string imageName = "";
            try
            {
                if (image.ContentLength > 0)
                {
                    extension = Path.GetExtension(image.FileName);
                    _FileName = Path.GetFileName(image.FileName);
                    imageName = movie.Title + extension;
                    string _path = Path.Combine(Server.MapPath("~/Upload"), imageName);
                    image.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";

            }
            if (ModelState.IsValid)
            {
                movie.image = imageName;
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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
