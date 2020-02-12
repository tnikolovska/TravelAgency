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
    public class HotelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Hotels
        public ActionResult Index()
        {
            var hotels = db.Hotels.Include(h => h.destination);
            return View(hotels.ToList());
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult AddArrangement(int id)
        {
            AddArangement arrangement = new AddArangement();
            Hotel hotel = db.Hotels.Include(h => h.destination).Include(h => h.arrangements).FirstOrDefault(h => h.Id == id);
            arrangement.hotel = hotel;
            arrangement.HotelId = id;
            return View(arrangement);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddArrangement(AddArangement model)
        {
            Hotel hotel = db.Hotels.Find(model.HotelId);
            ArrangementPrice price = new ArrangementPrice();
            price.date = model.dateTime;
            price.price = model.price;
            price.HotelId = model.HotelId;
            price.hotel = hotel;
            db.ArrangementPrices.Add(price);
            db.SaveChanges();
            var terminCenaOdBaza = db.ArrangementPrices.FirstOrDefault(t => t.date == model.dateTime && t.price == model.price);
            hotel.arrangements.Add(terminCenaOdBaza);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = model.HotelId });
        }
        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Include(h=>h.arrangements).FirstOrDefault(h=>h.Id==id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Hotels/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.DistantDestinationId = new SelectList(db.DistantDestinations, "Id", "Name");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Name,DistantDestinationId,Description,URLAddress,MainImage,image1,image6,image2,image3,image4,image5,stars")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Hotels.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistantDestinationId = new SelectList(db.DistantDestinations, "Id", "Name", hotel.DistantDestinationId);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistantDestinationId = new SelectList(db.DistantDestinations, "Id", "Name", hotel.DistantDestinationId);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Name,DistantDestinationId,Description,URLAddress,MainImage,image1,image6,image2,image3,image4,image5,stars")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistantDestinationId = new SelectList(db.DistantDestinations, "Id", "Name", hotel.DistantDestinationId);
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Include(h=>h.destination).FirstOrDefault(h=>h.Id==id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            db.Hotels.Remove(hotel);
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
