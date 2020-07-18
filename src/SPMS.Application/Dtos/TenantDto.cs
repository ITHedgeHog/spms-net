using System;
using System.Collections.Generic;
using System.Text;

namespace SPMS.Application.Dtos
{
    public class TenantDto
    {
        public TenantDto()
        {
            IsReadOnly = false;
            UseAnalytics = false;
        }

        public Guid Uuid { get; set; }
        public string gravatar { get; set; }

        public bool IsReadOnly { get; set; }

        public string SiteTitle { get; set; }

        public string GameName { get; set; }

        public string SiteDisclaimer { get; set; }
        public string SiteAnalytics { get; set; }

        public bool UseAnalytics { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsPlayer { get; set; }
        public string CommitShaLink { get; set; }
        public string CommitSha { get; set; }
        public bool IsSpiderable { get; set; }
        public string RobotsText { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Author { get; set; }
        public byte[] GameKey { get; set; }
        public int Id { get; set; }
        public string Theme { get; set; }
    }
}
