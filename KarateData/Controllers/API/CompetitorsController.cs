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
using KarateData.Models;

namespace KarateData.Controllers.API
{
    public class CompetitorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Competitors
        public IEnumerable<Competitor> GetCompetitors()
        {
            return db.Competitors.Include(a => a.ApplicationUser).ToList();
        }

        // GET: api/Competitors/5
        [ResponseType(typeof(Competitor))]
        public IHttpActionResult GetCompetitor(int id)
        {
            Competitor competitor = db.Competitors.Find(id);
            if (competitor == null)
            {
                return NotFound();
            }

            return Ok(competitor);
        }

        // PUT: api/Competitors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompetitor(int id, Competitor competitor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != competitor.CompetitorId)
            {
                return BadRequest();
            }

            db.Entry(competitor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitorExists(id))
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

        // POST: api/Competitors
        [ResponseType(typeof(Competitor))]
        public IHttpActionResult PostCompetitor(Competitor competitor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Competitors.Add(competitor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = competitor.CompetitorId }, competitor);
        }

        // DELETE: api/Competitors/5
        [ResponseType(typeof(Competitor))]
        public IHttpActionResult DeleteCompetitor(int id)
        {
            Competitor competitor = db.Competitors.Find(id);
            if (competitor == null)
            {
                return NotFound();
            }

            db.Competitors.Remove(competitor);
            db.SaveChanges();

            return Ok(competitor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompetitorExists(int id)
        {
            return db.Competitors.Count(e => e.CompetitorId == id) > 0;
        }
    }
}