using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public partial class EmployeeSurvey
    {
        public EmployeeSurvey()
        {
            Employee = new HashSet<Employee>();
        }

        [Key]
        public int SurveyId { get; set; }
        public int EmployeeId { get; set; }
        public byte EnvironmentSatisfaction { get; set; }
        public byte RelationshipSatisfaction { get; set; }
        public byte JobSatisfaction { get; set; }
        public byte WorkLifeBalance { get; set; }

        public virtual Employee EmployeeNavigation { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
