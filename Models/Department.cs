using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }
        /// <summary>
        /// Id of the Department
        /// </summary>
        /// <example>9</example>
        public byte DepartmentId { get; set; }
        /// <summary>
        /// The string of the department(related to the id)
        /// </summary>
        /// <example>Other</example>
        public string Department1 { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}

