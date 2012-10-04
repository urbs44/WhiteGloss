using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteGloss.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public int ArtistId { get; set; }
        public string Thumb { get; set; }
        public string Normal { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DisplayOrder { get; set; }

        public virtual Artist Artist { get; set; }
    }
}