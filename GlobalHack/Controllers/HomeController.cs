using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalHack.Models;

namespace GlobalHack.Controllers
{
    public class HomeController : Controller
    {
        private Data db = new Data();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Username,Password")] Person person)
        {
            if (person.Username == null || person.Password == null)
            {
                return RedirectToAction("Create", "People");
            }
            var foundPerson = db.Persons.FirstOrDefault(p => p.Username == person.Username && p.Password == person.Password);
            if (foundPerson == null)
            {
                return RedirectToAction("Create", "People");
            }
            return RedirectToAction("Index", "People", new {id = foundPerson.Id });
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