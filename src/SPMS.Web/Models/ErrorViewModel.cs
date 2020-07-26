using System;

namespace SPMS.Web.Models
{
    public class ErrorViewModel : Common.ViewModels.BaseViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
