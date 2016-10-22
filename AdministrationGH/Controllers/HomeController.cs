using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            var myLocalShelter = context.Shelters.Where(n => n.Name.StartsWith("St.")).ToList();


            var myReservations = context.Reservations.Where(r => r.ShelterId == myLocalShelter.First().Id);

            var youthShelters = context.Shelters.Where(ys => ys.MaxAge < 25 && ys.MaxAge != null);
            var youthSheltersBedsAvail = new Dictionary<Shelter,int>();
            foreach (var sh in youthShelters)
            {
                youthSheltersBedsAvail.Add(sh, (sh.Reservations.Any() ? sh.Reservations.Count + sh.Reservations.Sum(r => r.Person.NumChildren) : 0)); //beds avaliable);
            }

            var maleOnlyShelters = context.Shelters.Where(fs => fs.GenderRestriction == 1);
            var maleOnlySheltersBedsAvail = new Dictionary<Shelter, int>();
            foreach (var fs in maleOnlyShelters)
            {
                maleOnlySheltersBedsAvail.Add(fs, (fs.Reservations.Any() ? fs.Reservations.Count + fs.Reservations.Sum(r => r.Person.NumChildren) : 0)); //beds avaliable);
            }

            var pregnantOnlyShelters = context.Shelters.Where(fs => fs.PregnantOnly);
            var pregnantOnlySheltersBedsAvail = new Dictionary<Shelter, int>();
            foreach (var fs in pregnantOnlyShelters)
            {
                pregnantOnlySheltersBedsAvail.Add(fs, (fs.Reservations.Any() ? fs.Reservations.Count + fs.Reservations.Sum(r => r.Person.NumChildren) : 0)); //beds avaliable);
            }

            //I want a list of recent people and in my shelter
            //var allShelters = ;Where()
            ViewBag.allShelters = allShelters;
            ViewBag.myLocalShelter = myLocalShelter;
            ViewBag.myReservations = myReservations;
            ViewBag.bedsAvailable = 12;
            ViewBag.youthShelters = youthSheltersBedsAvail;
            ViewBag.maleOnlyShelters = maleOnlySheltersBedsAvail;
            ViewBag.pregnantOnlyShelters = pregnantOnlySheltersBedsAvail;

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
    }
}