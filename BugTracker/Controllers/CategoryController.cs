using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult AddorEdit(int id = 0) 
        {
            SelectedCategory emp = new SelectedCategory();
            using (BugDBEntities db = new BugDBEntities())
            {
                
                if (id != 0)
                {
                    emp = db.SelectedCategories.Where(x => x.ID == id).FirstOrDefault();
                    //Multi select dropdown
                    emp.SelectedIDArray = emp.SelectedCategoryIDs.Split(',').ToArray();
                
                }
                emp.CategoryCollection = db.Categories.ToList();
            }
            return View(emp);
        }
        [HttpPost]
        public ActionResult AddorEdit(SelectedCategory emp)
        {
            //multi select dropdown
            emp.SelectedCategoryIDs = string.Join(",", emp.SelectedIDArray);
            using (BugDBEntities db = new BugDBEntities())
            {
                if (emp.ID == 0)
                {

                    db.SelectedCategories.Add(emp);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            return RedirectToAction("AddOrEdit", new { id = 0 });
        }
    }
}