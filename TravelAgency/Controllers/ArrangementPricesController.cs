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
    public class ArrangementPricesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ArrangementPrices
        public ActionResult Index()
        {
            var arrangementPrices = db.ArrangementPrices.Include(a => a.hotel);
            return View(arrangementPrices.ToList());
        }

        // GET: ArrangementPrices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArrangementPrice arrangementPrice = db.ArrangementPrices.Find(id);
            if (arrangementPrice == null)
            {
                return HttpNotFound();
            }
            return View(arrangementPrice);
        }

        // GET: ArrangementPrices/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "Name");
            return View();
        }

        // POST: ArrangementPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,HotelId,date,price")] ArrangementPrice arrangementPrice)
        {
            if (ModelState.IsValid)
            {
                db.ArrangementPrices.Add(arrangementPrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "Name", arrangementPrice.HotelId);
            return View(arrangementPrice);
        }

        // GET: ArrangementPrices/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArrangementPrice arrangementPrice = db.ArrangementPrices.Find(id);
            if (arrangementPrice == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "Name", arrangementPrice.HotelId);
            return View(arrangementPrice);
        }

        // POST: ArrangementPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,HotelId,date,price")] ArrangementPrice arrangementPrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arrangementPrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "Name", arrangementPrice.HotelId);
            return View(arrangementPrice);
        }

        // GET: ArrangementPrices/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArrangementPrice arrangementPrice = db.ArrangementPrices.Find(id);
            if (arrangementPrice == null)
            {
                return HttpNotFound();
            }
            return View(arrangementPrice);
        }

        // POST: ArrangementPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            ArrangementPrice arrangementPrice = db.ArrangementPrices.Find(id);
            db.ArrangementPrices.Remove(arrangementPrice);
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
