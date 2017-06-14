using System;
using System.Collections.Generic;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class RelatedPersonnels
    {
        public int Id { get; set; }
        public int VersionId { get; set; }
        public int PersonId { get; set; }
        public string Personneltype { get; set; }

        public virtual Developers Person { get; set; }
        public virtual Versions Version { get; set; }
    }
}
