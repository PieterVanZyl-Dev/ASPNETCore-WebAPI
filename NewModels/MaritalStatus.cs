using System;
using System.Collections.Generic;

namespace WebApi.NewModels
{
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            Employee = new HashSet<Employee>();
        }

        public byte MaritalStatusId { get; set; }
        public string MaritalStatus1 { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
