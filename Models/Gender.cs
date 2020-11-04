using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Employee = new HashSet<Employee>();
        }
        /// <summary>
        /// The Gender Id
        /// </summary>
        /// <example>2</example>
        public byte GenderId { get; set; }
        /// <summary>
        /// The String Text of what that Gender ID relates to
        /// </summary>
        /// <example>Other</example>
        public string Gender1 { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
