using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class SmallVersions
    {
        public SmallVersions()
        {
            SmallFiles = new HashSet<SmallFiles>();
        }

        public int Id { get; set; }
        public string SmallVersionNumber { get; set; }
        public DateTime ReleaseTime { get; set; }
        public string Publisher { get; set; }
        public int VersionID { get; set; }

        public string Description { get; set; }

        public virtual ICollection<SmallFiles> SmallFiles { get; set; }
        public virtual Versions version { get; set; }
    }
}
