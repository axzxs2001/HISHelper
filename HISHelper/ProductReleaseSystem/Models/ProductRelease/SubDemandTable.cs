using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Models.ProductRelease
{
    public partial class SubDemandTable
    {
        public int ID { get; set; }
        public int DemandID { get; set; }
        public string SecondaryDemand { get; set; }
        public string Priority { get; set; }
    }
}
