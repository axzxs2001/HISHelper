using System;
using System.Collections.Generic;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class Developers
    {
        public Developers()
        {
            Files = new HashSet<Files>();
            RelatedPersonnels = new HashSet<RelatedPersonnels>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public string Qq { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
        public string Remarks { get; set; }

        public virtual ICollection<Files> Files { get; set; }
        public virtual ICollection<RelatedPersonnels> RelatedPersonnels { get; set; }
        public virtual Departments Department { get; set; }
    }
}
