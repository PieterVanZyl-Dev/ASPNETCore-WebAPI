using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class MartialStatus
    {
        public MartialStatus()
        {
            Employee = new HashSet<Employee>();
        }

        public int MaritalStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
