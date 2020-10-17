using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public int Age { get; set; }
        public bool Attrition { get; set; }
        public int BusinessTravel { get; set; }
        public int DailyRate { get; set; }
        public int Department { get; set; }
        public int DistanceFromHome { get; set; }
        public int Education { get; set; }
        public int EducationField { get; set; }
        public int EnvironmentSatisfaction { get; set; }
        public int Gender { get; set; }
        public int HourlyRate { get; set; }
        public int JobInvolvement { get; set; }
        public int JobLevel { get; set; }
        public int JobRole { get; set; }
        public int JobSatisfaction { get; set; }
        public int MaritalStatus { get; set; }
        public int MonthlyIncome { get; set; }
        public int MonthlyRate { get; set; }
        public int NumCompaniesWorked { get; set; }
        public bool OverTime { get; set; }
        public int PercentSalaryHike { get; set; }
        public int PerformanceRating { get; set; }
        public int RelationshipSatisfaction { get; set; }
        public int StockOptionLevel { get; set; }
        public int TotalWorkingYears { get; set; }
        public int TrainingTimesLastYear { get; set; }
        public int WorkLifeBalance { get; set; }
        public int YearsAtCompany { get; set; }
        public int YearsInCurrentRole { get; set; }
        public int YearsSinceLastPromotion { get; set; }
        public int YearsWithCurrManager { get; set; }

        public virtual BusinessTravel BusinessTravelNavigation { get; set; }
        public virtual Department DepartmentNavigation { get; set; }
        public virtual EducationField EducationFieldNavigation { get; set; }
        public virtual Gender GenderNavigation { get; set; }
        public virtual JobRole JobRoleNavigation { get; set; }
        public virtual MartialStatus MaritalStatusNavigation { get; set; }
    }
}
