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
    public class ArtistsController : Controller
    {
        private WgArtists db = new WgArtists();

        //
        // GET: /Admin/

        public ViewResult Index()
        {
            return View(db.Artists.ToList());
        }

        //
        // GET: /Admin/Details/5

        public ViewResult Details(int id)
        {
            Artist artist = db.Artists.Find(id);
            return View(artist);
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Artist artist, IEnumerable<HttpPostedFileBase> files)
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
                        var path = Path.Combine(Server.MapPath("~/Content/uploads"), "artist_" + newFileName);
                        file.SaveAs(path);
                        if (i == 1)
                        {
                            artist.BioImage = "/Content/uploads/artist_" + newFileName;
                        }
                        else
                        {
                            artist.HomePageImage = "/Content/uploads/artist_" + newFileName;
                        }
                    }
                    i++;
                }
                artist.CreatedDate = DateTime.Now;
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artist);
        }
        
        //
        // GET: /Admin/Edit/5
 
        public ActionResult Edit(int id)
        {
            Artist artist = db.Artists.Find(id);
            return View(artist);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Artist artist, IEnumerable<HttpPostedFileBase> files)
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
                        var path = Path.Combine(Server.MapPath("~/Content/uploads"), "artist_" + newFileName);
                        file.SaveAs(path);
                        if (i == 1)
                        {
                            artist.BioImage = "/Content/uploads/artist_" + newFileName;
                        }
                        else
                        {
                            artist.HomePageImage = "/Content/uploads/artist_" + newFileName;
                        }
                    }
                    i++;
                }
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        //
        // GET: /Admin/Delete/5
 
        public ActionResult Delete(int id)
        {
            Artist artist = db.Artists.Find(id);
            return View(artist);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Artist artist = db.Artists.Find(id);
            db.Artists.Remove(artist);
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