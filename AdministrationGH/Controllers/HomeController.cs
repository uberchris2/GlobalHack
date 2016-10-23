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

            
            var myReservations = context.Reservations.Where(x => x.ShelterId == myLocalShelter.Id).Include(p => p.Person).ToList();
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
            ViewBag.bedsAvailable = 12;


            // List of those staying tonight

            // Recent Referrals

            return View();
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
            youthSheltersBedsAvail = youthShelters.ToDictionary(ys => ys, ys => ys.Reservations.Any() ? ys.Reservations.Count + ys.Reservations.Sum(r => context.Persons.Find(r.PersonId).NumChildren) : 0);
            //foreach (var sh in youthShelters)
            //{
            //    youthSheltersBedsAvail.Add(sh, sh.Reservations.Any(r => r.ShelterId == sh.Id) ? sh.Reservations.Count + sh.Reservations.Sum(r => r.Person.NumChildren) : 0); //beds avaliable);
            //}

            var maleOnlyShelters = context.Shelters.Where(fs => fs.GenderRestriction == 1).Include(x => x.Reservations);
            var maleOnlySheltersBedsAvail = new Dictionary<Shelter, int>();
            maleOnlySheltersBedsAvail = maleOnlyShelters.ToDictionary(ys => ys, ys => ys.Reservations.Any() ? ys.Reservations.Count + ys.Reservations.Sum(r => context.Persons.Find(r.PersonId).NumChildren) : 0);

            //foreach (var fs in maleOnlyShelters)
            //{
            //    maleOnlySheltersBedsAvail.Add(fs, fs.Reservations.Any(r => r.ShelterId == fs.Id) ? fs.Reservations.Count + fs.Reservations.Sum(r => r.Person.NumChildren) : 0); //beds avaliable);
            //}

            var pregnantOnlyShelters = context.Shelters.Where(fs => fs.PregnantOnly).Include(x => x.Reservations);
            var pregnantOnlySheltersBedsAvail = new Dictionary<Shelter, int>();
            pregnantOnlySheltersBedsAvail = pregnantOnlyShelters.ToDictionary(ys => ys, ys => ys.Reservations.Any() ? ys.Reservations.Count + ys.Reservations.Sum(r => context.Persons.Find(r.PersonId).NumChildren) : 0);

            ViewBag.youthShelters = youthSheltersBedsAvail;
            ViewBag.maleOnlyShelters = maleOnlySheltersBedsAvail;
            ViewBag.pregnantOnlyShelters = pregnantOnlySheltersBedsAvail;
            return View();
        }
    }
}