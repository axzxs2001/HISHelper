using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class BeingStudied
    {
        public int ID { get; set; }
        public int DemandID { get; set; } 
        public int DepartmentID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpectedTime { get; set; }
    }
}
