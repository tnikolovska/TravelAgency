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
    public class DistantDestinationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DistantDestinations
        public ActionResult Index()
        {
            return View(db.DistantDestinations.ToList());
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult AddHotelToDestination(int id)
        {
            DistantDestination destination = db.DistantDestinations.Find(id);
            AddHotelToDestination model = new AddHotelToDestination();
            model.destination = destination;
            model.DistantDestinationId = id;
            model.hotelNames = new List<Hotel>();
            model.hotelNames = db.Hotels.Include(h => h.destination).Include(h => h.arrangements).Where(h => h.destination.Id == destination.Id).ToList();
            return View(model);

        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddHotelToDestination(AddHotelToDestination model)
        {
            DistantDestination destination = db.DistantDestinations.Find(model.DistantDestinationId);
            Hotel Hotel = db.Hotels.Include(m => m.destination).Include(m => m.arrangements).FirstOrDefault(m => m.Id == model.HotelId);
            destination.Hotels.Add(Hotel);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = model.DistantDestinationId });

        }

        // GET: DistantDestinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistantDestination distantDestination = db.DistantDestinations.Include(m=>m.Hotels).FirstOrDefault(m=>m.Id==id);
            if (distantDestination == null)
            {
                return HttpNotFound();
            }
            return View(distantDestination);
        }

        // GET: DistantDestinations/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DistantDestinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,MainImage")] DistantDestination distantDestination)
        {
            if (ModelState.IsValid)
            {
                db.DistantDestinations.Add(distantDestination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(distantDestination);
        }

        // GET: DistantDestinations/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistantDestination distantDestination = db.DistantDestinations.Find(id);
            if (distantDestination == null)
            {
                return HttpNotFound();
            }
            return View(distantDestination);
        }

        // POST: DistantDestinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,MainImage")] DistantDestination distantDestination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(distantDestination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(distantDestination);
        }

        // GET: DistantDestinations/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistantDestination distantDestination = db.DistantDestinations.Find(id);
            if (distantDestination == null)
            {
                return HttpNotFound();
            }
            return View(distantDestination);
        }

        // POST: DistantDestinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            DistantDestination distantDestination = db.DistantDestinations.Find(id);
            db.DistantDestinations.Remove(distantDestination);
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
