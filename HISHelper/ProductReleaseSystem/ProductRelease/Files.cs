using System;
using System.Collections.Generic;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class Files
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime UploadTime { get; set; }
        public int UploadPeople { get; set; }
        public int VersionsId { get; set; }

        public virtual Developers UploadPeopleNavigation { get; set; }
        public virtual Versions Versions { get; set; }
    }
}
