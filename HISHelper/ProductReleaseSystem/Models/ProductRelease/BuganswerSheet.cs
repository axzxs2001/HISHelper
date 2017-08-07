using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class BuganswerSheet
    {
        public int ID { get; set; }
        public int BugID { get; set; }
        public int DeveloperID { get; set; }
        public string BugReply { get; set; }
        public DateTime ResponseTime { get; set; }
    }
}
