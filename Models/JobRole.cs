using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class JobRole
    {
        public JobRole()
        {
            Employee = new HashSet<Employee>();
        }

        public int JobRoleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
