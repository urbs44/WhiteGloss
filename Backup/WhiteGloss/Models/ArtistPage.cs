using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteGloss.Models
{
    public class ArtistPage
    {
        public Artist Artist { get; set; }
        public List<Image> Images { get; set; }
        public List<SiteText> SiteText { get; set; }
    }
}