using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SPMS.Web.Models;

namespace SPMS.Web.ViewModels.Biography
{
    public class CreateBiographyViewModel : Models.Biography
    {
        public List<SelectListItem> Postings { get; set; }

    }

    public class EditBiographyViewModel : Models.Biography
    {
        public List<SelectListItem> Postings { get; set; }

    }
}
