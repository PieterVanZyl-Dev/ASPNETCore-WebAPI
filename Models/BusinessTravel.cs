using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class BusinessTravel
    {
        public BusinessTravel()
        {
            Employee = new HashSet<Employee>();
        }

        public byte BusinessTravelId { get; set; }
        public string BusinessTravel1 { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
