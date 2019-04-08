using FinalProjectJob.Models.EntityFramework;
using FinalProjectJob.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectJob.Controllers
{
    public class HomeController : Controller
    {
        jobsEntities db = new jobsEntities();
        // GET: Home
        public ActionResult Index()
        {
            var categories = db.categories.
                SqlQuery("select categoryId,categoryName,categoryIcon,count(vakansiyalar.vakId) as say " +
                " from category inner join vakansiyalar on vakansiyalar.vakToCategory=category.categoryId" +
                " group by category.categoryId,category.categoryName,category.categoryIcon").
                ToList();

            var elanlar = db.elans.
                SqlQuery("select * from elan").
                ToList();

            var vm = new HomeIndexViewModel
            {
                Elan = elanlar,
                Category = categories
            };
            return View(vm);
        }

        public ActionResult Category()
        {
            var model = db.categories.ToList();
            return View(model);
        }

        public ActionResult NewPostJob()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPostJob(elan elan)
        {
            db.elans.Add(elan);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Vacancies(int id)
        {
            var vakansiya = db.vakansiyalars.FirstOrDefault(v => v.vakToCategory == id);

            return View(vakansiya);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string jobTitle, string city)
        {
            var elan = db.elans.Where(v => v.Title.ToUpper().Contains(jobTitle.ToUpper())
                                           || v.Location.ToUpper().Contains(city.ToUpper())).ToList();

            return View(elan);
        }
    }
}