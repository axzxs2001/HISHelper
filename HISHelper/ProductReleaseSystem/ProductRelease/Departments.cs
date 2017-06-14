using System;
using System.Collections.Generic;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class Departments
    {
        public Departments()
        {
            Developers = new HashSet<Developers>();
        }

        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public virtual ICollection<Developers> Developers { get; set; }
    }
}
