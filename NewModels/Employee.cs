using System;
using System.Collections.Generic;

namespace WebApi.NewModels
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeSurvey1 = new HashSet<EmployeeSurvey>();
        }

        public byte Age { get; set; }
        public bool Attrition { get; set; }
        public byte BusinessTravel { get; set; }
        public short DailyRate { get; set; }
        public byte Department { get; set; }
        public byte DistanceFromHome { get; set; }
        public byte Education { get; set; }
        public byte EducationField { get; set; }
        public short EmployeeNumber { get; set; }
        public byte Gender { get; set; }
        public byte HourlyRate { get; set; }
        public byte JobInvolvement { get; set; }
        public byte JobLevel { get; set; }
        public byte JobRole { get; set; }
        public byte MaritalStatus { get; set; }
        public short MonthlyIncome { get; set; }
        public short MonthlyRate { get; set; }
        public byte NumCompaniesWorked { get; set; }
        public bool OverTime { get; set; }
        public byte PercentSalaryHike { get; set; }
        public byte PerformanceRating { get; set; }
        public byte StandardHours { get; set; }
        public byte StockOptionLevel { get; set; }
        public byte TotalWorkingYears { get; set; }
        public byte TrainingTimesLastYear { get; set; }
        public byte YearsAtCompany { get; set; }
        public byte YearsInCurrentRole { get; set; }
        public byte YearsSinceLastPromotion { get; set; }
        public byte YearsWithCurrManager { get; set; }
        public int EmployeeSurvey { get; set; }

        public virtual BusinessTravel BusinessTravelNavigation { get; set; }
        public virtual Department DepartmentNavigation { get; set; }
        public virtual EducationField EducationFieldNavigation { get; set; }
        public virtual EmployeeSurvey EmployeeSurveyNavigation { get; set; }
        public virtual Gender GenderNavigation { get; set; }
        public virtual JobRole JobRoleNavigation { get; set; }
        public virtual MaritalStatus MaritalStatusNavigation { get; set; }
        public virtual ICollection<EmployeeSurvey> EmployeeSurvey1 { get; set; }
    }
}
