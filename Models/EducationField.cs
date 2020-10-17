using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class EducationField
    {
        public EducationField()
        {
            Employee = new HashSet<Employee>();
        }

        public int EducationFieldId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
