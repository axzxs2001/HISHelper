using System;
using System.Collections.Generic;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class Products
    {
        public Products()
        {
            Versions = new HashSet<Versions>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Versions> Versions { get; set; }
    }
}
