using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using KarateData.Models;

namespace KarateData.Controllers.API
{
    public class CompetitorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Competitors
        public IQueryable<Competitor> GetCompetitors()
        {
            return db.Competitors;
        }

        // GET: api/Competitors/5
        [ResponseType(typeof(Competitor))]
        public async Task<IHttpActionResult> GetCompetitor(int id)
        {
            Competitor competitor = await db.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return NotFound();
            }

            return Ok(competitor);
        }

        // PUT: api/Competitors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCompetitor(int id, Competitor competitor)
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
                await db.SaveChangesAsync();
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
        public async Task<IHttpActionResult> PostCompetitor(Competitor competitor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Competitors.Add(competitor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = competitor.CompetitorId }, competitor);
        }

        // DELETE: api/Competitors/5
        [ResponseType(typeof(Competitor))]
        public async Task<IHttpActionResult> DeleteCompetitor(int id)
        {
            Competitor competitor = await db.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return NotFound();
            }

            db.Competitors.Remove(competitor);
            await db.SaveChangesAsync();

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