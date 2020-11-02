using System;
using System.Collections.Generic;

namespace WebApi.NewModels
{
    public partial class EducationField
    {
        public EducationField()
        {
            Employee = new HashSet<Employee>();
        }

        public byte EducationFieldId { get; set; }
        public string EducationField1 { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
