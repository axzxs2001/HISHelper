using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.Models.ProductRelease
{
    public partial class Researchers
    {
        public int ID { get; set; }
        public int ResearchProjectsID { get; set; }
        public int PersonID { get; set; }
        public string Personneltype { get; set; }
    }
}
