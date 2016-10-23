using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalHack;
using GlobalHack.Models;

namespace AdministrationGH.Controllers
{
    public class HomeController : Controller
    {
        Data context = new Data();
        public ActionResult Index()
        {
            // Reservations for tonight at 'My' shelter and others within my Continuum of Care
            // by type
            var allShelters = context.Shelters.ToList();
            var myLocalShelter = context.Shelters.Where(n => n.Name.StartsWith("St.")).ToList().First();

            
            var myReservations = context.Reservations.Where(x => x.ShelterId == myLocalShelter.Id && !x.NoShow).Include(p => p.Person).ToList();
            int reservationsCount = myReservations.Any() ? myReservations.Count(r => !r.NoShow) + myReservations.Where(rs => !rs.NoShow).Sum(r => context.Persons.Find(r.PersonId).NumChildren) : 0;

            //myReservations = context.Reservations.Where(r => r.ShelterId == myLocalShelter.First().Id).ToList();
            
            //foreach (var fs in pregnantOnlyShelters)
            //{
            //    pregnantOnlySheltersBedsAvail.Add(fs, fs.Reservations.Any(r => r.ShelterId == fs.Id) ? fs.Reservations.Count + fs.Reservations.Sum(r => r.Person.NumChildren) : 0); //beds avaliable);
            //}

            //I want a list of recent people and in my shelter
            //var allShelters = ;Where()
            ViewBag.allShelters = allShelters;
            ViewBag.myLocalShelter = myLocalShelter;
            ViewBag.myReservations = myReservations;
            ViewBag.bedsAvailable = myLocalShelter.Beds;
            ViewBag.reservationsCount = reservationsCount;


            // List of those staying tonight

            // Recent Referrals

            return View();
        }

        public ActionResult CheckDefer(int? id)
        {
            var reservation = context.Reservations.Find(id);
            var reservation2 = context.Reservations.Where(r => r.Id ==id).Include(x => x.Shelter).ToList().First();
            var person = context.Persons.Find(reservation.PersonId);
            ViewBag.PersonId = person.Id;

            var personAge = DateTime.Now.Year - person.BirthYear;
            var dbShelters = context.Shelters.Where(shelter => (shelter.MaxAge >= personAge)
                && (shelter.MinAge <= personAge) //min age
                && (shelter.GenderRestriction == 0 || shelter.GenderRestriction == person.Gender) //gender restriction
                && (!shelter.PregnantOnly || person.Pregnant) //pregnant only
                && (!shelter.SexOffenderRestriction || !person.SexOffender) //sex offender
                && (shelter.Id != reservation.ShelterId)
                && shelter.Beds > (shelter.Reservations.Any() ?
                    shelter.Reservations.Count(r => !r.NoShow) + shelter.Reservations.Where(r => !r.NoShow).Sum(r => r.Person.NumChildren) : 0) + person.NumChildren); //beds avaliable

            ViewBag.curRevId = id;
            ViewBag.currentReservation = reservation2;
            ViewBag.possibleShelters = dbShelters;

            return View(dbShelters.ToList());

        }

        public ActionResult ConfirmDefer(int? reservationId , int? newShelterId )
        {
            var reservation = context.Reservations.Find(reservationId);
            var newShelter = context.Shelters.Find(newShelterId);
            reservation.ShelterId = newShelter.Id;
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CommonShelters()
        {
            var youthShelters = context.Shelters.Where(ys => ys.MaxAge < 25 && ys.MaxAge != null).Include(x => x.Reservations);
            var youthSheltersBedsAvail = new Dictionary<Shelter, int>();
            youthSheltersBedsAvail = youthShelters.ToDictionary(ys => ys, ys => ys.Reservations.Any(r => !r.NoShow) ? ys.Reservations.Count(r => !r.NoShow) + ys.Reservations.Where(rs => !rs.NoShow).Sum(r => context.Persons.Find(r.PersonId).NumChildren) : 0);
            //foreach (var sh in youthShelters)
            //{
            //    youthSheltersBedsAvail.Add(sh, sh.Reservations.Any(r => r.ShelterId == sh.Id) ? sh.Reservations.Count + sh.Reservations.Sum(r => r.Person.NumChildren) : 0); //beds avaliable);
            //}

            var maleOnlyShelters = context.Shelters.Where(fs => fs.GenderRestriction == 1).Include(x => x.Reservations);
            var maleOnlySheltersBedsAvail = new Dictionary<Shelter, int>();
            maleOnlySheltersBedsAvail = maleOnlyShelters.ToDictionary(ys => ys, ys => ys.Reservations.Any(r => !r.NoShow) ? ys.Reservations.Count(r => !r.NoShow) + ys.Reservations.Where(rs => !rs.NoShow).Sum(r => context.Persons.Find(r.PersonId).NumChildren) : 0);

            //foreach (var fs in maleOnlyShelters)
            //{
            //    maleOnlySheltersBedsAvail.Add(fs, fs.Reservations.Any(r => r.ShelterId == fs.Id) ? fs.Reservations.Count + fs.Reservations.Sum(r => r.Person.NumChildren) : 0); //beds avaliable);
            //}

            var pregnantOnlyShelters = context.Shelters.Where(fs => fs.PregnantOnly).Include(x => x.Reservations);
            var pregnantOnlySheltersBedsAvail = new Dictionary<Shelter, int>();
            pregnantOnlySheltersBedsAvail = pregnantOnlyShelters.ToDictionary(ys => ys, ys => ys.Reservations.Any(r => !r.NoShow) ? ys.Reservations.Count(r => !r.NoShow) + ys.Reservations.Where(r => !r.NoShow).Sum(r => context.Persons.Find(r.PersonId).NumChildren) : 0);

            ViewBag.youthShelters = youthSheltersBedsAvail;
            ViewBag.maleOnlyShelters = maleOnlySheltersBedsAvail;
            ViewBag.pregnantOnlyShelters = pregnantOnlySheltersBedsAvail;
            return View();
        }
    }
}