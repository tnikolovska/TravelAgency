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
    public class CruisesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cruises
        public ActionResult Index()
        {
            return View(db.Cruises.ToList());
        }

        // GET: Cruises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cruise cruise = db.Cruises.Include(m=>m.cruisePlans).Include(m=>m.cruiseArrangements).FirstOrDefault(m=>m.Id==id);
            if (cruise == null)
            {
                return HttpNotFound();
            }
            return View(cruise);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Agenda(int id)
        {
            Cruise cruise = db.Cruises.Include(c => c.cruiseArrangements).Include(c => c.cruisePlans).FirstOrDefault(c => c.Id == id);
            Agenda agenda = new Agenda();
            agenda.cruise = cruise;
            agenda.CruiseId = id;
            agenda.Day = cruise.cruisePlans.Count + 1;
            return View(agenda);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Agenda(Agenda model)
        {
            Cruise cruise = db.Cruises.Include(m => m.cruisePlans).Include(m => m.cruiseArrangements).FirstOrDefault(m => m.Id == model.CruiseId);
            CruisePlan krstPlan = new CruisePlan();
            krstPlan.Day = model.Day;
            krstPlan.Port = model.Port;
            krstPlan.Departure = model.Departure;
            krstPlan.Arrival = model.Arrival;
            krstPlan.cruise = cruise;
            krstPlan.CruiseId = model.CruiseId;
            db.SaveChanges();
            cruise.cruisePlans.Add(krstPlan);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = model.CruiseId });

        }
        [Authorize(Roles = "Administrator")]
        public ActionResult PriceList(int id)
        {
            Cruise cruise = db.Cruises.Include(c => c.cruisePlans).Include(c => c.cruiseArrangements).FirstOrDefault(c => c.Id == id);
            PriceList cenovnik = new PriceList();
            cenovnik.cruise = cruise;
            cenovnik.CruiseId = id;
            return View(cenovnik);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult PriceList(PriceList model)
        {
            Cruise cruise = db.Cruises.Include(c => c.cruiseArrangements).Include(c => c.cruisePlans).FirstOrDefault(c => c.Id == model.CruiseId);
            CruiseArrangement krstTermini = new CruiseArrangement();
            krstTermini.date = model.date;
            krstTermini.InsideCabin = model.InsideCabins;
            krstTermini.BalconyCabin = model.BalconyCabins;
            krstTermini.OceanViewCabin = model.OcenViewCabins;
            db.SaveChanges();
            cruise.cruiseArrangements.Add(krstTermini);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = model.CruiseId });
        }
        // GET: Cruises/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cruises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,image1,image2,image3,image4,image5,image6")] Cruise cruise)
        {
            if (ModelState.IsValid)
            {
                db.Cruises.Add(cruise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cruise);
        }

        // GET: Cruises/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cruise cruise = db.Cruises.Find(id);
            if (cruise == null)
            {
                return HttpNotFound();
            }
            return View(cruise);
        }

        // POST: Cruises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,image1,image2,image3,image4,image5,image6")] Cruise cruise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cruise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cruise);
        }

        // GET: Cruises/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cruise cruise = db.Cruises.Find(id);
            if (cruise == null)
            {
                return HttpNotFound();
            }
            return View(cruise);
        }

        // POST: Cruises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cruise cruise = db.Cruises.Find(id);
            db.Cruises.Remove(cruise);
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
