using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TVDatabase.Models;

namespace TVDatabase.Controllers
{
    public class TVTablesController : Controller
    {
        private TVDatabaseEntities1 db = new TVDatabaseEntities1();

        // GET: TVTables
        public ActionResult Index(string movieGenre, string searchString)
        {
            //return View(db.Movies.ToList());

            //genre search
            var GenreLst = new List<string>();
            var GenreQry = from d in db.TVTables
                           orderby d.Genre
                           select d.Genre;
            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            //title search
            var movies = from m in db.TVTables
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.ShowName.Contains(searchString));
            }

            //last bit of genre search
            //movieGenre = "Science Fiction";
            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            return View(movies.ToList());
        }

        // GET: TVTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVTable tVTable = db.TVTables.Find(id);
            if (tVTable == null)
            {
                return HttpNotFound();
            }

            string referrer = HttpContext.Request.UrlReferrer.ToString();
            if (!referrer.Contains("Details") && !referrer.Contains("Delete") && !referrer.Contains("Edit"))
            {
                int currentViews = tVTable.Views;
                tVTable.Views = currentViews + 1;

                if (ModelState.IsValid)
                {
                    db.Entry(tVTable).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            ViewBag.refer = HttpContext.Request.UrlReferrer.ToString();
            return View(tVTable);
        }

        public ActionResult Like(int id)
        {
            TVTable show = db.TVTables.Find(id);

            int currentLikes = show.Likes;
            show.Likes = currentLikes + 1;

            if (ModelState.IsValid)
            {
                db.Entry(show).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { id });
        }

        public ActionResult Dislike(int id)
        {
            TVTable show = db.TVTables.Find(id);

            int currentDislikes = show.Dislikes;
            show.Dislikes = currentDislikes + 1;

            if (ModelState.IsValid)
            {
                db.Entry(show).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { id });
        }

        //public ActionResult Views(int id)
        //{
        //    TVTable show = db.TVTables.Find(id);

        //    int currentViews = show.Views;
        //    show.Views = currentViews + 1;

        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(show).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }

        //    return RedirectToAction("Details", new { id });
        //}

        // GET: TVTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TVTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ShowName,Genre,DateFrom,DateTo,NumberOfSeasons,CurrentlyRunning,Description,Actors,PeakViewers,Network,Image,Rating,Country,Views,Likes,Dislikes,SecretPassword")] TVTable tVTable)
        {
            if (ModelState.IsValid)
            {
                db.TVTables.Add(tVTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tVTable);
        }

        // GET: TVTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVTable tVTable = db.TVTables.Find(id);
            if (tVTable == null)
            {
                return HttpNotFound();
            }
            return View(tVTable);
        }

        // POST: TVTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ShowName,Genre,DateFrom,DateTo,NumberOfSeasons,CurrentlyRunning,Description,Actors,PeakViewers,Network,Image,Rating,Country,Views,Likes,Dislikes,SecretPassword")] TVTable tVTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tVTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { tVTable.ID });
            }
            return View(tVTable);
        }

        // GET: TVTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVTable tVTable = db.TVTables.Find(id);
            if (tVTable == null)
            {
                return HttpNotFound();
            }
            return View(tVTable);
        }

        // POST: TVTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TVTable tVTable = db.TVTables.Find(id);
            db.TVTables.Remove(tVTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Table(string movieGenre, string searchString)
        {
            //return View(db.Movies.ToList());

            //genre search
            var GenreLst = new List<string>();
            var GenreQry = from d in db.TVTables
                           orderby d.Genre
                           select d.Genre;
            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            //title search
            var movies = from m in db.TVTables
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.ShowName.Contains(searchString));
            }

            //last bit of genre search
            //movieGenre = "Science Fiction";
            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            return View(movies.ToList());
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
