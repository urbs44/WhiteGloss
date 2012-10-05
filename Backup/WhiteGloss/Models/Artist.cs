using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteGloss.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string HomePageName { get; set; }
        public string Bio { get; set; }
        public string HomePageImage { get; set; }
        public string BioImage { get; set; }
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}