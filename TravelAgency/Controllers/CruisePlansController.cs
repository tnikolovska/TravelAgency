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
    public class CruisePlansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CruisePlans
        public ActionResult Index()
        {
            var cruisePlans = db.CruisePlans.Include(c => c.cruise);
            return View(cruisePlans.ToList());
        }

        // GET: CruisePlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CruisePlan cruisePlan = db.CruisePlans.Find(id);
            if (cruisePlan == null)
            {
                return HttpNotFound();
            }
            return View(cruisePlan);
        }

        // GET: CruisePlans/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.CruiseId = new SelectList(db.Cruises, "Id", "Name");
            return View();
        }

        // POST: CruisePlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Day,Port,Departure,Arrival,CruiseId")] CruisePlan cruisePlan)
        {
            if (ModelState.IsValid)
            {
                db.CruisePlans.Add(cruisePlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CruiseId = new SelectList(db.Cruises, "Id", "Name", cruisePlan.CruiseId);
            return View(cruisePlan);
        }

        // GET: CruisePlans/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CruisePlan cruisePlan = db.CruisePlans.Find(id);
            if (cruisePlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.CruiseId = new SelectList(db.Cruises, "Id", "Name", cruisePlan.CruiseId);
            return View(cruisePlan);
        }

        // POST: CruisePlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Day,Port,Departure,Arrival,CruiseId")] CruisePlan cruisePlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cruisePlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CruiseId = new SelectList(db.Cruises, "Id", "Name", cruisePlan.CruiseId);
            return View(cruisePlan);
        }

        // GET: CruisePlans/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CruisePlan cruisePlan = db.CruisePlans.Find(id);
            if (cruisePlan == null)
            {
                return HttpNotFound();
            }
            return View(cruisePlan);
        }

        // POST: CruisePlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            CruisePlan cruisePlan = db.CruisePlans.Find(id);
            db.CruisePlans.Remove(cruisePlan);
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
