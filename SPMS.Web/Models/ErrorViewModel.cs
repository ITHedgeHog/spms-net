using System;
using SPMS.Application.ViewModels;

namespace SPMS.Web.Models
{
    public class ErrorViewModel : ViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
