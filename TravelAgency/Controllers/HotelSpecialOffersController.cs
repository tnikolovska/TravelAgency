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
    public class HotelSpecialOffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HotelSpecialOffers
        public ActionResult Index()
        {
            var hotelSpecialOffers = db.HotelSpecialOffers.Include(h => h.specialOffer);
            return View(hotelSpecialOffers.ToList());
        }

        // GET: HotelSpecialOffers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelSpecialOffer hotelSpecialOffer = db.HotelSpecialOffers.Include(h=>h.arrangementPrices).FirstOrDefault(h=>h.Id==id);
            if (hotelSpecialOffer == null)
            {
                return HttpNotFound();
            }
            return View(hotelSpecialOffer);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult AddArrangement(int id)
        {
            AddArrangementSpecialOffer addArrangement = new AddArrangementSpecialOffer();
            HotelSpecialOffer hotel = db.HotelSpecialOffers.Include(h => h.arrangementPrices).Include(h => h.specialOffer).FirstOrDefault(h => h.Id == id);
            addArrangement.hotel = hotel;
            addArrangement.HotelSpecialOfferId = id;
            return View(addArrangement);

        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddArrangement(AddArrangementSpecialOffer model)
        {
            HotelSpecialOffer hotel = db.HotelSpecialOffers.Find(model.HotelSpecialOfferId);
            ArrangementPriceSpecial terminCena = new ArrangementPriceSpecial();
            terminCena.date = model.dateTime;
            terminCena.price = model.price;
            terminCena.hotel = hotel;
            terminCena.HotelSpecialOfferId = model.HotelSpecialOfferId;
            db.ArrangementPriceSpecials.Add(terminCena);
            db.SaveChanges();
            var termincenaOdBaza = db.ArrangementPriceSpecials.FirstOrDefault(t => t.date == model.dateTime && t.price == model.price);
            hotel.arrangementPrices.Add(termincenaOdBaza);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = model.HotelSpecialOfferId });
        }
        // GET: HotelSpecialOffers/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.SpecialOfferId = new SelectList(db.SpecialOffers, "Id", "Name");
            return View();
        }

        // POST: HotelSpecialOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,SpecialOfferId,stars,mainImage,image1,image2,image3,image4,image5,image6")] HotelSpecialOffer hotelSpecialOffer)
        {
            if (ModelState.IsValid)
            {
                db.HotelSpecialOffers.Add(hotelSpecialOffer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecialOfferId = new SelectList(db.SpecialOffers, "Id", "Name", hotelSpecialOffer.SpecialOfferId);
            return View(hotelSpecialOffer);
        }

        // GET: HotelSpecialOffers/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelSpecialOffer hotelSpecialOffer = db.HotelSpecialOffers.Find(id);
            if (hotelSpecialOffer == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecialOfferId = new SelectList(db.SpecialOffers, "Id", "Name", hotelSpecialOffer.SpecialOfferId);
            return View(hotelSpecialOffer);
        }

        // POST: HotelSpecialOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,SpecialOfferId,stars,mainImage,image1,image2,image3,image4,image5,image6")] HotelSpecialOffer hotelSpecialOffer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotelSpecialOffer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecialOfferId = new SelectList(db.SpecialOffers, "Id", "Name", hotelSpecialOffer.SpecialOfferId);
            return View(hotelSpecialOffer);
        }

        // GET: HotelSpecialOffers/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelSpecialOffer hotelSpecialOffer = db.HotelSpecialOffers.Find(id);
            if (hotelSpecialOffer == null)
            {
                return HttpNotFound();
            }
            return View(hotelSpecialOffer);
        }

        // POST: HotelSpecialOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            HotelSpecialOffer hotelSpecialOffer = db.HotelSpecialOffers.Find(id);
            db.HotelSpecialOffers.Remove(hotelSpecialOffer);
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
