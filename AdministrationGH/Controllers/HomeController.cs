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
                //r => r.ShelterId = myLocalShelter.First().Id);

            //I want a list of recent people and in my shelter
            //var allShelters = ;Where()
            ViewBag.allShelters = allShelters;
            ViewBag.myLocalShelter = myLocalShelter;
            ViewBag.myReservations = myReservations;
            ViewBag.bedsAvailable = 12;
            ViewBag.youthShelters = youthShelters;
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