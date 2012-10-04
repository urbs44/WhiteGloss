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
    public class ImagesController : Controller
    {
        private WgArtists db = new WgArtists();

        //
        // GET: /Image/

        public ViewResult Index(string id)
        {
            ImagesPage model = new ImagesPage();
            if (String.IsNullOrEmpty(id))
            {
                model.Images = db.Images.Include(i => i.Artist).OrderBy(i => i.DisplayOrder);
                model.Artists = new SelectList(db.Artists.ToList(), "ArtistID", "Name");
            }
            else
            {
                int artistId = Convert.ToInt32(id);
                if (artistId == 0) { model.Images = db.Images.Include(i => i.Artist).OrderBy(i => i.DisplayOrder); }
                else { model.Images = db.Images.Where(i => i.ArtistId == artistId).OrderBy(i => i.DisplayOrder); }
                model.Artists = new SelectList(db.Artists.ToList(), "ArtistID", "Name", artistId);  
            }

            return View(model);
        }
        
        [HttpPost]
        public ViewResult Index(FormCollection form)
        {
            ImagesPage model = new ImagesPage();
            int artistId = form[0] == "" ? 0 : Convert.ToInt32(form[0]);
            if (artistId == 0) { model.Images = db.Images.Include(i => i.Artist).OrderBy(i => i.DisplayOrder); }
            else { model.Images = db.Images.Where(i => i.ArtistId == artistId).OrderBy(i => i.DisplayOrder); }
            model.Artists = new SelectList(db.Artists.ToList(), "ArtistID", "Name", artistId);            

            return View(model);
        }

        //
        // GET: /Image/Details/5

        public ViewResult Details(int id)
        {
            Image image = db.Images.Find(id);
            return View(image);
        }

        //
        // GET: /Image/Create

        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            return View();
        } 

        //
        // POST: /Image/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Image image, IEnumerable<HttpPostedFileBase> files)
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
                        var path = Path.Combine(Server.MapPath("~/Content/uploads"), newFileName);
                        file.SaveAs(path);
                        if (i == 1)
                        {
                            image.Thumb = "/Content/uploads/" + newFileName;
                        }
                        else
                        {
                            image.Normal = "/Content/uploads/" + newFileName;
                        }
                    }
                    i++;
                }

                image.CreatedDate = DateTime.Now;
                db.Images.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", image.ArtistId);
            return View(image);
        }
        
        //
        // GET: /Image/Edit/5
 
        public ActionResult Edit(int id)
        {
            Image image = db.Images.Find(id);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", image.ArtistId);
            return View(image);
        }

        //
        // POST: /Image/Edit/5

        [HttpPost]
        public ActionResult Edit(Image image, IEnumerable<HttpPostedFileBase> files)
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
                        var path = Path.Combine(Server.MapPath("~/Content/uploads"), newFileName);
                        file.SaveAs(path);
                        if (i == 1)
                        {
                            image.Thumb = "/Content/uploads/" + newFileName;
                        }
                        else
                        {
                            image.Normal = "/Content/uploads/" + newFileName;
                        }
                    }
                    i++;
                }
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = image.ArtistId });
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", image.ArtistId);
            return View(image);
        }

        //
        // GET: /Image/Delete/5
 
        public ActionResult Delete(int id)
        {
            Image image = db.Images.Find(id);
            return View(image);
        }

        //
        // POST: /Image/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
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