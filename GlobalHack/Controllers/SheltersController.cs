using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GlobalHack;
using GlobalHack.Models;

namespace GlobalHack.Controllers
{
    public class SheltersController : Controller
    {
        private Data db = new Data();

        // GET: Shelters
        public ActionResult Index(int personId)
        {
            var person = db.Persons.Find(personId);
            ViewBag.PersonId = personId;



            var asdasdasd = db.Shelters.Where(asdf => asdf.Reservations.Any())
                .Select(shelter =>  shelter.Reservations.Sum(x => x.Person.NumChildren) ).ToList();



            var personAge = DateTime.Now.Year - person.BirthYear;
            var dbShelters = db.Shelters.Where(shelter => (shelter.MaxAge >= personAge)
                && (shelter.MinAge <= personAge) //min age
                && (shelter.GenderRestriction == 0 || shelter.GenderRestriction == person.Gender) //gender restriction
                && (!shelter.PregnantOnly || person.Pregnant) //pregnant only
                && (!shelter.SexOffenderRestriction || !person.SexOffender) //sex offender
                && shelter.Beds > (shelter.Reservations.Any() ? 
                    shelter.Reservations.Count(r => !r.NoShow) + shelter.Reservations.Where(r => !r.NoShow).Sum(r => r.Person.NumChildren) : 0) + person.NumChildren); //beds avaliable
            
            return View(dbShelters.ToList());
        }

        // GET: Shelters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelter shelter = db.Shelters.Find(id);
            if (shelter == null)
            {
                return HttpNotFound();
            }
            return View(shelter);
        }

        // GET: Shelters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shelters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Phone,Location,Beds,MinAge,MaxAge,GenderRestriction,PregnantOnly,SexOffenderRestriction")] Shelter shelter)
        {
            if (ModelState.IsValid)
            {
                db.Shelters.Add(shelter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shelter);
        }

        // GET: Shelters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelter shelter = db.Shelters.Find(id);
            if (shelter == null)
            {
                return HttpNotFound();
            }
            return View(shelter);
        }

        // POST: Shelters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Phone,Location,Beds,MinAge,MaxAge,GenderRestriction,PregnantOnly,SexOffenderRestriction")] Shelter shelter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shelter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shelter);
        }

        // GET: Shelters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelter shelter = db.Shelters.Find(id);
            if (shelter == null)
            {
                return HttpNotFound();
            }
            return View(shelter);
        }

        // POST: Shelters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shelter shelter = db.Shelters.Find(id);
            db.Shelters.Remove(shelter);
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
