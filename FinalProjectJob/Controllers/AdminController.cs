using FinalProjectJob.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectJob.Controllers
{
    public class AdminController : Controller
    {

        jobsEntities db = new jobsEntities();
        private object[] Id;
        // GET: Admin

        public ActionResult Index()
        {
            var model = db.categories.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult NewCategory()
        {
            return View("UpdateForm");
        }

        [HttpPost]
        public ActionResult Save(category category)
        {

            if (category.categoryId == 0)
            {
                db.categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                var saveCategory = db.categories.Find(category.categoryId);
                if (saveCategory == null)
                {
                    return HttpNotFound();
                }
                saveCategory.categoryId = category.categoryId;
                saveCategory.categoryName = category.categoryName;
                saveCategory.categoryIcon = category.categoryIcon;
            }
            db.SaveChanges();
            return RedirectToAction("Update/"+ category.categoryId, "Admin");
        }

        public ActionResult Update(int Id)
        {
              var updateCategory = db.categories.Find(Id);
            // var model = db.categories.Where(s => s.categoryId == Id).FirstOrDefault<category>();
          //  var model = db.categories.ToList();
            if (updateCategory == null)
            {
                return HttpNotFound();
            }

            return View("UpdateForm", updateCategory);
         // return Content(updateCategory.categoryId.ToString());
        }


        public ActionResult Delete(int Id)
        {
            var deleteCategory = db.categories.Find(Id);
            if (deleteCategory == null)
            {
                return HttpNotFound();
            }
            db.categories.Remove(deleteCategory);
            db.SaveChanges();
            return RedirectToAction("");

        }

        public ActionResult IndexVak()
        {
            var model = db.vakansiyalars.ToList();
            return View(model);
        }

  
        public ActionResult YeniVak()
        {
            var model = db.categories.ToList();

            return View(model);
        //   return Content(rows);
        }

        [HttpPost]
        public ActionResult SaveVak(vakansiyalar vakansiyalar)
        {

            if (vakansiyalar.vakId == 0)
            {
                db.vakansiyalars.Add(vakansiyalar);
                db.SaveChanges();
                return RedirectToAction("IndexVak", "Admin");
            }
            else
            {
                var saveVak = db.vakansiyalars.Find(vakansiyalar.vakId);
                if (saveVak == null)
                {
                    return HttpNotFound();
                }
                saveVak.vakId = vakansiyalar.vakId;
                saveVak.vakAd = vakansiyalar.vakAd;
                saveVak.vakSeher = vakansiyalar.vakSeher;
                saveVak.vakSirket = vakansiyalar.vakSirket;
                saveVak.VakMaas = vakansiyalar.VakMaas;
                saveVak.vakDate = vakansiyalar.vakDate;
                saveVak.vakHaqqinda = vakansiyalar.vakHaqqinda;
                saveVak.vakToCategory = vakansiyalar.vakToCategory;
            }
            db.SaveChanges();
            return RedirectToAction("UpdateVak/" + vakansiyalar.vakId, "Admin");
        }

        public ActionResult UpdateVak(int Id)
        {
             var model = db.vakansiyalars.Find(Id);
            dynamic mymodel = new ExpandoObject();
            mymodel.Categories = GetCategories();
            mymodel.Vakansiyalars = GetVakansiyalars();

            // var model = db.categories.Where(s => s.categoryId == Id).FirstOrDefault<category>();
            //  var model = db.categories.ToList();
            if (mymodel == null)
            {
                return HttpNotFound();
            }

            return View("NewVak", mymodel);
            // return Content(model.vakAd.ToString());
        }
        public List<category> GetCategories()
        {
            List<category> categorys = new List<category>();
            categorys = db.categories.ToList();
            return categorys;

        }
        public List<vakansiyalar> GetVakansiyalars()
        {
            List<vakansiyalar> vakansiyalars = new List<vakansiyalar>();
            vakansiyalars = db.vakansiyalars.ToList();
            return vakansiyalars;

        }
        public ActionResult DeleteVak(int Id)
        {
            var deleteVak = db.vakansiyalars.Find(Id);
            if (deleteVak == null)
            {
                return HttpNotFound();
            }
            db.vakansiyalars.Remove(deleteVak);
            db.SaveChanges();
            return RedirectToAction("IndexVak");

        }
    }
}