using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteGloss.Models;
using System.IO;
using System.Data;

namespace WhiteGloss.Controllers
{
    [Authorize(Users = "france")]
    public class EnterController : Controller
    {
        private WgArtists db = new WgArtists();

        //
        // GET: /Enter/

        public ActionResult Index()
        {
            if (db.EnterPage.Count() == 0)
            {
                return RedirectToAction("Create");
            }
            else return RedirectToAction("Edit");
        }

        //
        // GET: /Enter/Create

        public ActionResult Create()
        {
            EnterPage ep = new EnterPage();
            return View(ep);
        } 

        //
        // POST: /Enter/Create

        [HttpPost]
        public ActionResult Create(EnterPage page, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
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
                    page.Image = "/Content/uploads/" + newFileName;
                }

                db.EnterPage.Add(page);
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }

            return View(page);
        }
        
        //
        // GET: /Enter/Edit/5
 
        public ActionResult Edit()
        {
            var enterPage = db.EnterPage.First();
            return View(enterPage);
        }

        //
        // POST: /Enter/Edit/5

        [HttpPost]
        public ActionResult Edit(EnterPage page, HttpPostedFileBase file)
        {
            try
            {
                //EnterPage ep = db.EnterPage.First();
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    // extract only the fielname
                    var fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    string newFileName = DateTime.Now.ToFileTimeUtc().ToString() + "_" + fileName;
                    var path = Path.Combine(Server.MapPath("~/Content/uploads"), newFileName);
                    file.SaveAs(path);
                    page.Image = "/Content/uploads/" + newFileName;
                }
                db.Entry(page).State = EntityState.Modified;
                //ep.Text = page.Text;
                db.SaveChanges();
 
                return RedirectToAction("Index", "Admin");
            }
            catch
            {
                return View(db.EnterPage.First());
            }
        }
    }
}
