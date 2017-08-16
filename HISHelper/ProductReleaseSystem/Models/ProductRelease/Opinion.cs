using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class Opinion
    {
        public int ID { get; set; }
        public int DemandID { get; set; }
        public int proposerID { get; set; }
        public string purpose { get; set; }
        public string detailed { get; set; }
        public DateTime Initiatedtime { get; set; }
    }
}
