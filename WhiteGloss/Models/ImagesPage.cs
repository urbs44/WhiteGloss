using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhiteGloss.Models
{
    public class ImagesPage
    {
        public int ArtistId { get; set; }
        public IEnumerable<WhiteGloss.Models.Image> Images { get; set; }
        public IEnumerable<SelectListItem> Artists { get; set; }
    }
}