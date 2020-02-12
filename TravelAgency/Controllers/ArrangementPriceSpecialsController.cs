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
    public class ArrangementPriceSpecialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ArrangementPriceSpecials
        public ActionResult Index()
        {
            var arrangementPriceSpecials = db.ArrangementPriceSpecials.Include(a => a.hotel);
            return View(arrangementPriceSpecials.ToList());
        }

        // GET: ArrangementPriceSpecials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArrangementPriceSpecial arrangementPriceSpecial = db.ArrangementPriceSpecials.Find(id);
            if (arrangementPriceSpecial == null)
            {
                return HttpNotFound();
            }
            return View(arrangementPriceSpecial);
        }

        // GET: ArrangementPriceSpecials/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.HotelSpecialOfferId = new SelectList(db.HotelSpecialOffers, "Id", "Name");
            return View();
        }

        // POST: ArrangementPriceSpecials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,date,price,HotelSpecialOfferId")] ArrangementPriceSpecial arrangementPriceSpecial)
        {
            if (ModelState.IsValid)
            {
                db.ArrangementPriceSpecials.Add(arrangementPriceSpecial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HotelSpecialOfferId = new SelectList(db.HotelSpecialOffers, "Id", "Name", arrangementPriceSpecial.HotelSpecialOfferId);
            return View(arrangementPriceSpecial);
        }

        // GET: ArrangementPriceSpecials/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArrangementPriceSpecial arrangementPriceSpecial = db.ArrangementPriceSpecials.Find(id);
            if (arrangementPriceSpecial == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelSpecialOfferId = new SelectList(db.HotelSpecialOffers, "Id", "Name", arrangementPriceSpecial.HotelSpecialOfferId);
            return View(arrangementPriceSpecial);
        }

        // POST: ArrangementPriceSpecials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,date,price,HotelSpecialOfferId")] ArrangementPriceSpecial arrangementPriceSpecial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arrangementPriceSpecial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HotelSpecialOfferId = new SelectList(db.HotelSpecialOffers, "Id", "Name", arrangementPriceSpecial.HotelSpecialOfferId);
            return View(arrangementPriceSpecial);
        }

        // GET: ArrangementPriceSpecials/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArrangementPriceSpecial arrangementPriceSpecial = db.ArrangementPriceSpecials.Find(id);
            if (arrangementPriceSpecial == null)
            {
                return HttpNotFound();
            }
            return View(arrangementPriceSpecial);
        }

        // POST: ArrangementPriceSpecials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            ArrangementPriceSpecial arrangementPriceSpecial = db.ArrangementPriceSpecials.Find(id);
            db.ArrangementPriceSpecials.Remove(arrangementPriceSpecial);
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
