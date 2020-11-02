using System;
using System.Collections.Generic;

namespace WebApi.NewModels
{
    public partial class Gender
    {
        public Gender()
        {
            Employee = new HashSet<Employee>();
        }

        public byte GenderId { get; set; }
        public string Gender1 { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
