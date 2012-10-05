﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteGloss.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }
    }
}