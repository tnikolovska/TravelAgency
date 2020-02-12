using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelAgency.Models;

namespace TravelAgency.Controllers
{
    public class CruiseArrangementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CruiseArrangements
        public ActionResult Index()
        {
            var cruiseArrangements = db.CruiseArrangements.Include(c => c.cruise);
            return View(cruiseArrangements.ToList());
        }

        // GET: CruiseArrangements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CruiseArrangement cruiseArrangement = db.CruiseArrangements.Find(id);
            if (cruiseArrangement == null)
            {
                return HttpNotFound();
            }
            return View(cruiseArrangement);
        }

        // GET: CruiseArrangements/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.CruiseId = new SelectList(db.Cruises, "Id", "Name");
            return View();
        }

        // POST: CruiseArrangements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,date,CruiseId,InsideCabin,BalconyCabin,OceanViewCabin")] CruiseArrangement cruiseArrangement)
        {
            if (ModelState.IsValid)
            {
                db.CruiseArrangements.Add(cruiseArrangement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CruiseId = new SelectList(db.Cruises, "Id", "Name", cruiseArrangement.CruiseId);
            return View(cruiseArrangement);
        }

        // GET: CruiseArrangements/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CruiseArrangement cruiseArrangement = db.CruiseArrangements.Find(id);
            if (cruiseArrangement == null)
            {
                return HttpNotFound();
            }
            ViewBag.CruiseId = new SelectList(db.Cruises, "Id", "Name", cruiseArrangement.CruiseId);
            return View(cruiseArrangement);
        }

        // POST: CruiseArrangements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,date,CruiseId,InsideCabin,BalconyCabin,OceanViewCabin")] CruiseArrangement cruiseArrangement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cruiseArrangement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CruiseId = new SelectList(db.Cruises, "Id", "Name", cruiseArrangement.CruiseId);
            return View(cruiseArrangement);
        }

        // GET: CruiseArrangements/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CruiseArrangement cruiseArrangement = db.CruiseArrangements.Find(id);
            if (cruiseArrangement == null)
            {
                return HttpNotFound();
            }
            return View(cruiseArrangement);
        }

        // POST: CruiseArrangements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            CruiseArrangement cruiseArrangement = db.CruiseArrangements.Find(id);
            db.CruiseArrangements.Remove(cruiseArrangement);
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
