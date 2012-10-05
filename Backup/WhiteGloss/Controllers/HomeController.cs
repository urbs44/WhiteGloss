using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteGloss.Models;

namespace WhiteGloss.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            WgArtists data = new WgArtists();
            EnterPage model = new EnterPage();
            model = (from a in data.EnterPage select a).FirstOrDefault();

            return View(model);
        }

        public ActionResult Gallery()
        {
            WgArtists data = new WgArtists();
            HomePage model = new HomePage();
            model.Artists = (from a in data.Artists where a.Active == true orderby a.DisplayOrder select a).ToList();

            return View(model);
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult Space()
        {
            return View();
        }

        public ActionResult Contact()
        {
            WgArtists data = new WgArtists();
            ContactPage model = (from a in data.ContactPages select a).FirstOrDefault();
            return View(model);
        }

        public ActionResult Artist(int id)
        {
            WgArtists data = new WgArtists();
            ArtistPage model = new ArtistPage();
            model.Artist = (from a in data.Artists where a.ArtistId == id select a).Single();
            model.Images = (from i in data.Images where i.ArtistId == id select i).OrderBy(i => i.DisplayOrder).ToList();
            model.SiteText = (from t in data.SiteText select t).ToList();
            return View(model);
        }

        public ActionResult Images(int id)
        {
            WgArtists data = new WgArtists();
            ArtistPage model = new ArtistPage();
            model.Artist = (from a in data.Artists where a.ArtistId == id select a).Single();
            model.Images = (from i in data.Images where i.ArtistId == id select i).OrderBy(i => i.DisplayOrder).ToList();
            return View(model);
        }

        public ActionResult Bio(int id)
        {
            WgArtists data = new WgArtists();
            ArtistPage model = new ArtistPage();
            model.Artist = (from a in data.Artists where a.ArtistId == id select a).Single();
            model.Images = (from i in data.Images where i.ArtistId == id select i).ToList();
            return View(model);
        }

        public ActionResult NavMenu()
        {
            WgArtists data = new WgArtists();
            Menu model = new Menu();
            model.MenuText = (from t in data.SiteText where t.Term == "Menu" select t).FirstOrDefault() == null ? "Menu" : (from t in data.SiteText where t.Term == "Menu" select t).FirstOrDefault().Value;
            model.MenuItems = (from m in data.MenuItems where m.Active == true orderby m.DisplayOrder select m).ToList();
            return PartialView(model);
        }
    }
}
