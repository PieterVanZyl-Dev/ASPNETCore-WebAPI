using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class EmployeeSurvey
    {
        public EmployeeSurvey()
        {
            Employee = new HashSet<Employee>();
        }

        public int SurveyId { get; set; }
        public short EmployeeId { get; set; }
        public byte EnvironmentSatisfaction { get; set; }
        public byte RelationshipSatisfaction { get; set; }
        public byte JobSatisfaction { get; set; }
        public byte WorkLifeBalance { get; set; }

        public virtual Employee EmployeeNavigation { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
