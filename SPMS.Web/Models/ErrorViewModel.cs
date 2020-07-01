using System;
using SPMS.Application.ViewModels;

namespace SPMS.Web.Models
{
    public class ErrorViewModel : Common.ViewModels.ViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
