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

        public int BusinessTravelId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
