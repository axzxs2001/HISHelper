using System;
using System.Collections.Generic;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class Researchers
    {
        public int ID { get; set; }
        public int ResearchProjectsID { get; set; }
        public int PersonID { get; set; }
        public string Personneltype { get; set; }
    }
}
