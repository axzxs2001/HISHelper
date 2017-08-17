using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class RequestForm
    {
        public int ID { get; set; }
        public string DemandNname { get; set; }
        public string RequirementsDescription { get; set; }
        public string Priority { get; set; }
        public string UserName { get; set; }
        public string Producer { get; set; }
        public string ContactInformation { get; set; }
        public int ImplementerID { get; set; }
        public DateTime MakeTime { get; set; }
        public string VersionNumber { get; set; }
        public int DeliveryDepartment { get; set; }
        public string Status { get; set; }
        public string ProductID { get; set; }
        public string Address { get; set; }
        public int DeleteStatus { get; set; }

    }
}
