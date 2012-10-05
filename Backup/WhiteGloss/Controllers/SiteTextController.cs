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
    public class SiteTextController : Controller
    {
        private WgArtists db = new WgArtists();

        //
        // GET: /SiteText/

        public ViewResult Index()
        {
            return View(db.SiteText.ToList());
        }

        //
        // GET: /SiteText/Details/5

        public ViewResult Details(int id)
        {
            SiteText sitetext = db.SiteText.Find(id);
            return View(sitetext);
        }

        //
        // GET: /SiteText/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /SiteText/Create

        [HttpPost]
        public ActionResult Create(SiteText sitetext)
        {
            if (ModelState.IsValid)
            {
                db.SiteText.Add(sitetext);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(sitetext);
        }
        
        //
        // GET: /SiteText/Edit/5
 
        public ActionResult Edit(int id)
        {
            SiteText sitetext = db.SiteText.Find(id);
            return View(sitetext);
        }

        //
        // POST: /SiteText/Edit/5

        [HttpPost]
        public ActionResult Edit(SiteText sitetext)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sitetext).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sitetext);
        }

        //
        // GET: /SiteText/Delete/5
 
        public ActionResult Delete(int id)
        {
            SiteText sitetext = db.SiteText.Find(id);
            return View(sitetext);
        }

        //
        // POST: /SiteText/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SiteText sitetext = db.SiteText.Find(id);
            db.SiteText.Remove(sitetext);
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