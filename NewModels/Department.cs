using System;
using System.Collections.Generic;

namespace WebApi.NewModels
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }

        public byte DepartmentId { get; set; }
        public string Department1 { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
