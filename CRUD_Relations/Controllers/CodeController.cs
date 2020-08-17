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
    public class CodeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Code
        public ActionResult Index()
        {
            var codes = db.Codes.Include(c => c.Movie);
            return View(codes.ToList());
        }

        // GET: Code/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }
            return View(code);
        }

        // GET: Code/Create
        public ActionResult Create()
        {
            ViewBag.CodeId = new SelectList(db.Movies, "MovieId", "Title");
            return View();
        }

        // POST: Code/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodeId,Number")] Code code)
        {
            if (ModelState.IsValid)
            {
                db.Codes.Add(code);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodeId = new SelectList(db.Movies, "MovieId", "Title", code.CodeId);
            return View(code);
        }

        // GET: Code/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodeId = new SelectList(db.Movies, "MovieId", "Title", code.CodeId);
            return View(code);
        }

        // POST: Code/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodeId,Number")] Code code)
        {
            if (ModelState.IsValid)
            {
                db.Entry(code).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodeId = new SelectList(db.Movies, "MovieId", "Title", code.CodeId);
            return View(code);
        }

        // GET: Code/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }
            return View(code);
        }

        // POST: Code/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Code code = db.Codes.Find(id);
            db.Codes.Remove(code);
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
