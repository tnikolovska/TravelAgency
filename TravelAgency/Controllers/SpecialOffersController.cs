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
    public class SpecialOffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SpecialOffers
        public ActionResult Index()
        {
            return View(db.SpecialOffers.ToList());
        }

        // GET: SpecialOffers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialOffer specialOffer = db.SpecialOffers.Include(o=>o.hotels).FirstOrDefault(o=>o.Id==id);
            if (specialOffer == null)
            {
                return HttpNotFound();
            }
            return View(specialOffer);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult AddHotelToDestination(int id)
        {
            SpecialOffer specialOffer = db.SpecialOffers.Include(s => s.hotels).FirstOrDefault(s => s.Id == id);
            AddHotelSpecialOffer model = new AddHotelSpecialOffer();
            model.specialOffer = specialOffer;
            model.SpecialOfferId = id;
            model.hotelNames = new List<HotelSpecialOffer>();
            model.hotelNames = db.HotelSpecialOffers.Include(s => s.specialOffer).Include(s => s.arrangementPrices).Where(s => s.SpecialOfferId == id).ToList();
            return View(model);

        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddHotelToDestination(AddHotelSpecialOffer model)
        {
            SpecialOffer specialOffer = db.SpecialOffers.Include(s => s.hotels).FirstOrDefault(s => s.Id == model.SpecialOfferId);
            HotelSpecialOffer hotel = db.HotelSpecialOffers.Include(s => s.arrangementPrices).Include(s => s.specialOffer).FirstOrDefault(h => h.Id == model.HotelId);
            specialOffer.hotels.Add(hotel);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = model.SpecialOfferId });
        }

        // GET: SpecialOffers/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpecialOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,MainImage")] SpecialOffer specialOffer)
        {
            if (ModelState.IsValid)
            {
                db.SpecialOffers.Add(specialOffer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specialOffer);
        }

        // GET: SpecialOffers/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialOffer specialOffer = db.SpecialOffers.Find(id);
            if (specialOffer == null)
            {
                return HttpNotFound();
            }
            return View(specialOffer);
        }

        // POST: SpecialOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,MainImage")] SpecialOffer specialOffer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialOffer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(specialOffer);
        }

        // GET: SpecialOffers/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialOffer specialOffer = db.SpecialOffers.Find(id);
            if (specialOffer == null)
            {
                return HttpNotFound();
            }
            return View(specialOffer);
        }

        // POST: SpecialOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            SpecialOffer specialOffer = db.SpecialOffers.Find(id);
            db.SpecialOffers.Remove(specialOffer);
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
