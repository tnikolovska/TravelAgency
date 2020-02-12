using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TravelAgency.Models;

namespace TravelAgency.Controllers
{
    public class DistantDestinations1Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DistantDestinations1
        public IQueryable<DistantDestination> GetDistantDestinations()
        {
            return db.DistantDestinations;
        }

        // GET: api/DistantDestinations1/5
        [ResponseType(typeof(DistantDestination))]
        public IHttpActionResult GetDistantDestination(int id)
        {
            DistantDestination distantDestination = db.DistantDestinations.Find(id);
            if (distantDestination == null)
            {
                return NotFound();
            }

            return Ok(distantDestination);
        }

        // PUT: api/DistantDestinations1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDistantDestination(int id, DistantDestination distantDestination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != distantDestination.Id)
            {
                return BadRequest();
            }

            db.Entry(distantDestination).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistantDestinationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DistantDestinations1
        [ResponseType(typeof(DistantDestination))]
        public IHttpActionResult PostDistantDestination(DistantDestination distantDestination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DistantDestinations.Add(distantDestination);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = distantDestination.Id }, distantDestination);
        }

        // DELETE: api/DistantDestinations1/5
        [ResponseType(typeof(DistantDestination))]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult DeleteDistantDestination(int id)
        {
            DistantDestination distantDestination = db.DistantDestinations.Find(id);
            if (distantDestination == null)
            {
                return NotFound();
            }

            db.DistantDestinations.Remove(distantDestination);
            db.SaveChanges();

            return Ok(distantDestination);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DistantDestinationExists(int id)
        {
            return db.DistantDestinations.Count(e => e.Id == id) > 0;
        }
    }
}