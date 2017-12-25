using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KarateData.Models;

namespace KarateData.Controllers
{
    public class CompetitorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Competitors
        public async Task<ActionResult> Index()
        {
            var competitors = db.Competitors.Include(c => c.ApplicationUser);
            return View(await competitors.ToListAsync());
        }

        // GET: Competitors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competitor competitor = await db.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return HttpNotFound();
            }
            return View(competitor);
        }

        // GET: Competitors/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "City");
            return View();
        }

        // POST: Competitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompetitorId,FirstName,LastName,BirthDate,City,Country,ApplicationUserId")] Competitor competitor)
        {
            if (ModelState.IsValid)
            {
                db.Competitors.Add(competitor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "City", competitor.ApplicationUserId);
            return View(competitor);
        }

        // GET: Competitors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competitor competitor = await db.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "City", competitor.ApplicationUserId);
            return View(competitor);
        }

        // POST: Competitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompetitorId,FirstName,LastName,BirthDate,City,Country,ApplicationUserId")] Competitor competitor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(competitor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "City", competitor.ApplicationUserId);
            return View(competitor);
        }

        // GET: Competitors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competitor competitor = await db.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return HttpNotFound();
            }
            return View(competitor);
        }

        // POST: Competitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Competitor competitor = await db.Competitors.FindAsync(id);
            db.Competitors.Remove(competitor);
            await db.SaveChangesAsync();
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
