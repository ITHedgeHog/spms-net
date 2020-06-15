using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPMS.Web.Models;

namespace SPMS.Web.ViewModels
{
    public class BiographiesViewModel
    {
        public List<Posting> Postings { get; set; }

        public List<Models.Biography> Biographies { get; set; }
    }
}
