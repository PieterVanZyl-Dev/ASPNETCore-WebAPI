using System;
using System.Collections.Generic;

namespace WebApi.NewModels
{
    public partial class EmployeeSurveyStaging
    {
        public short EmployeeNumber { get; set; }
        public byte EnvironmentSatisfaction { get; set; }
        public byte RelationshipSatisfaction { get; set; }
        public byte JobSatisfaction { get; set; }
        public byte WorkLifeBalance { get; set; }
    }
}
