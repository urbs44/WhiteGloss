using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WhiteGloss.Models
{
    public class WgArtists : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<EnterPage> EnterPage { get; set; }
        public DbSet<ContactPage> ContactPages { get; set; }
        public DbSet<SiteText> SiteText { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
    }
}