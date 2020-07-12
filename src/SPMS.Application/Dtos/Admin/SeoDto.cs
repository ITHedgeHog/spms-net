using System;
using System.Collections.Generic;
using System.Text;

namespace SPMS.Application.Dtos.Admin
{
    public class SeoDto
    { 
        public bool IsSpiderable { get; set; }
        public string RobotsText { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Author { get; set; }

    }
}