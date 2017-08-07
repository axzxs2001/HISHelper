using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Models.ProductRelease
{
    public partial class DevelopmentRequirementsTable
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public int SubDemandID { get; set; }
        public string State { get; set; }
        public int WorkingHours { get; set; }
        public DateTime AddTime { get; set; }
    }
}
