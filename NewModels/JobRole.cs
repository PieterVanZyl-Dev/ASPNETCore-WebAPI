using System;
using System.Collections.Generic;

namespace WebApi.NewModels
{
    public partial class JobRole
    {
        public JobRole()
        {
            Employee = new HashSet<Employee>();
        }

        public byte JobRoleId { get; set; }
        public string JobRole1 { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
