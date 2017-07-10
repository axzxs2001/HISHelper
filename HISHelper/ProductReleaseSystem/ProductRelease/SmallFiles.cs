using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class SmallFiles
    {
        public int ID { get; set; }
        public string SmallFileName { get; set; }
        public DateTime UploadTime { get; set; }
        public string SmallFilePath { get; set; }
        public int SmallVersionsID { get; set; }


        public virtual SmallVersions SmallVersions { get; set; }
    }
}
