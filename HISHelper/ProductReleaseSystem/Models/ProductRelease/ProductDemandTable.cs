using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class ProductDemandTable
    {
        public int ID { get; set; }
        public int DemandID { get; set; }
        public string DemandName { get; set; }
        public string DemandModify { get; set; }
        public string DemandPurpose { get; set; }
        public string Priority { get; set; }
        public int ProductID { get; set; }
        public DateTime ChangeTime { get; set; }
        public string VersionNumber { get; set; }
    }
}
