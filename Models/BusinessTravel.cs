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

    public class BusinessTravelResponse
    {
        /// <summary>
        /// The BusinessTravelId
        /// </summary>
        /// <example>1</example>
        public byte BusinessTravelId { get; set; }
        /// <summary>
        /// The string related to the ID (Rarely/Often etc.)
        /// </summary>
        /// <example>rarely</example>
        public string BusinessTravel1 { get; set; }
    }


}
