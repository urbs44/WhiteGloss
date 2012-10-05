using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteGloss.Models;
using System.IO;

namespace WhiteGloss.Controllers
{
    [Authorize(Users = "france")]
    public class ContactController : Controller
    {
        private WgArtists db = new WgArtists();

        //
        // GET: /Contact/

        public ActionResult Index()
        {
            if (db.ContactPages.Count() == 0)
            {
                return RedirectToAction("Create");
            }
            else return RedirectToAction("Edit");
        }

        //
        // GET: /Contact/Details/5

        public ViewResult Details(int id)
        {
            ContactPage contactpage = db.ContactPages.Find(id);
            return View(contactpage);
        }

        //
        // GET: /Contact/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Contact/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ContactPage contactpage, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                int i = 1;
                foreach (var file in files)
                {
                    // Verify that the user selected a file
                    if (file != null && file.ContentLength > 0)
                    {
                        // extract only the fielname
                        var fileName = Path.GetFileName(file.FileName);
                        // store the file inside ~/App_Data/uploads folder
                        string newFileName = DateTime.Now.ToFileTimeUtc().ToString() + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Content/uploads"), "contact_" + newFileName);
                        file.SaveAs(path);
                        if (i == 1)
                        {
                            contactpage.MapImage = "/Content/uploads/contact_" + newFileName;
                        }
                        else
                        {
                            contactpage.GalleryImage = "/Content/uploads/contact_" + newFileName;
                        }
                    }
                    i++;
                }
                db.ContactPages.Add(contactpage);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(contactpage);
        }
        
        //
        // GET: /Contact/Edit/5
 
        public ActionResult Edit()
        {
            ContactPage contactpage = db.ContactPages.First();
            return View(contactpage);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(ContactPage contactpage, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                int i = 1;
                foreach (var file in files)
                {
                    // Verify that the user selected a file
                    if (file != null && file.ContentLength > 0)
                    {
                        // extract only the fielname
                        var fileName = Path.GetFileName(file.FileName);
                        // store the file inside ~/App_Data/uploads folder
                        string newFileName = DateTime.Now.ToFileTimeUtc().ToString() + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Content/uploads"), "contact_" + newFileName);
                        file.SaveAs(path);
                        if (i == 1)
                        {
                            contactpage.MapImage = "/Content/uploads/contact_" + newFileName;
                        }
                        else
                        {
                            contactpage.GalleryImage = "/Content/uploads/contact_" + newFileName;
                        }
                    }
                    i++;
                }
                db.Entry(contactpage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactpage);
        }

        //
        // GET: /Contact/Delete/5
 
        public ActionResult Delete(int id)
        {
            ContactPage contactpage = db.ContactPages.Find(id);
            return View(contactpage);
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ContactPage contactpage = db.ContactPages.Find(id);
            db.ContactPages.Remove(contactpage);
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