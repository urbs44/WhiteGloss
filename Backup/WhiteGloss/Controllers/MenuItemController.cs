using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteGloss.Models;

namespace WhiteGloss.Controllers
{
    [Authorize(Users = "france")]
    public class MenuItemController : Controller
    {
        private WgArtists db = new WgArtists();

        //
        // GET: /MenuItem/

        public ViewResult Index()
        {
            return View(db.MenuItems.ToList());
        }

        //
        // GET: /MenuItem/Details/5

        public ViewResult Details(int id)
        {
            MenuItem menuitem = db.MenuItems.Find(id);
            return View(menuitem);
        }

        //
        // GET: /MenuItem/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /MenuItem/Create

        [HttpPost]
        public ActionResult Create(MenuItem menuitem)
        {
            if (ModelState.IsValid)
            {
                db.MenuItems.Add(menuitem);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(menuitem);
        }
        
        //
        // GET: /MenuItem/Edit/5
 
        public ActionResult Edit(int id)
        {
            MenuItem menuitem = db.MenuItems.Find(id);
            return View(menuitem);
        }

        //
        // POST: /MenuItem/Edit/5

        [HttpPost]
        public ActionResult Edit(MenuItem menuitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menuitem);
        }

        //
        // GET: /MenuItem/Delete/5
 
        public ActionResult Delete(int id)
        {
            MenuItem menuitem = db.MenuItems.Find(id);
            return View(menuitem);
        }

        //
        // POST: /MenuItem/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            MenuItem menuitem = db.MenuItems.Find(id);
            db.MenuItems.Remove(menuitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}