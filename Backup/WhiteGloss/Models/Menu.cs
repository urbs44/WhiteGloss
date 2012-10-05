using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteGloss.Models
{
    public class Menu
    {
        public string MenuText { get; set; }
        public List<MenuItem> MenuItems { get; set; }
    }
}