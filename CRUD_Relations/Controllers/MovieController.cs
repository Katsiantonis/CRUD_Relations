using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUD_Relations.Models;

namespace CRUD_Relations.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movie
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Code).Include(m => m.Director);
            return View(movies.ToList());
        }

        // GET: Movie/Details/5
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

        // GET: Movie/Create
        public ActionResult Create()
        {
            ViewBag.MovieId = new SelectList(db.Codes, "CodeId", "Number");

            var lista = db.Employees.ToList();

            var selectEmployee = db.Employees.ToList().Where(x => x.Salary < 45000).Select(x => x.EmployeeId);
            var disabledItems = db.Employees.ToList().Where(x => x.Salary > 45000).Select(x => x.EmployeeId);

            //ViewBag.EmployeeId = new SelectList(lista,"EmployeeId","Name");

            var availableActors = db.Actors.ToList();

            ViewBag.ActorId = new SelectList(availableActors, "ActorId", "FirstName");

            ViewBag.DirectorId = new SelectList(db.Directors, "DirectorId", "FirstName");
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieId,Title,CodeId,DirectorId,ActorId")] Movie movie,List<int> ActorId)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
               
               foreach (int id in ActorId)
                {
                    Actor actor = db.Actors.Find(id);
                    if(actor!=null)
                    {
                        movie.Actors.Add(actor);
                    }
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.MovieId = new SelectList(db.Codes, "CodeId", "Number", movie.MovieId);
            ViewBag.DirectorId = new SelectList(db.Directors, "DirectorId", "FirstName", movie.DirectorId);

            var availableActors = db.Actors.ToList();
            ViewBag.ActorId = new SelectList(availableActors, "ActorId", "FirstName");
            return View(movie);
        }

        // GET: Movie/Edit/5
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
            ViewBag.MovieId = new SelectList(db.Codes, "CodeId", "Number", movie.MovieId);
            ViewBag.DirectorId = new SelectList(db.Directors, "DirectorId", "FirstName", movie.DirectorId);

            var selectedActors = movie.Actors.Select(x => x.ActorId);
            var availableActors = db.Actors.ToList();
            ViewBag.ActorId = new MultiSelectList(availableActors, "ActorId", "FirstName", selectedActors);

            //ViewBag.ActorId = db.Actors.Select(x => new SelectListItem()
            //{
            //    Value = x.ActorId.ToString(),
            //    Text = x.FirstName
            //});

            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Title,CodeId,DirectorId,ActorId")] Movie movie, List<int> ActorId)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Attach(movie);
                db.Entry(movie).Collection("Actors").Load();
                movie.Actors.Clear();
                db.SaveChanges();

                if (ActorId is null || ActorId.Count() == 0)
                {
                    db.SaveChanges();
                }
                else
                {
                    foreach (int id in ActorId)
                    {
                        Actor actor = db.Actors.Find(id);
                        if (actor != null)
                        {
                            movie.Actors.Add(actor);
                        }
                    }
                    db.SaveChanges();
                }

                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieId = new SelectList(db.Codes, "CodeId", "Number", movie.MovieId);
            ViewBag.DirectorId = new SelectList(db.Directors, "DirectorId", "FirstName", movie.DirectorId);
            return View(movie);
        }

        // GET: Movie/Delete/5
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

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
