using System;
using System.Collections.Generic;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class Versions
    {
        public Versions()
        {
            Files = new HashSet<Files>();
            RelatedPersonnels = new HashSet<RelatedPersonnels>();
        }

        public int Id { get; set; }
        public string VersionNumber { get; set; }
        public DateTime ReleaseTime { get; set; }
        public string Publisher { get; set; }
        public int ProductId { get; set; }

        public virtual ICollection<Files> Files { get; set; }
        public virtual ICollection<RelatedPersonnels> RelatedPersonnels { get; set; }
        public virtual Products Product { get; set; }
    }
}
