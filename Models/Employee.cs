using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
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
        public int EmployeeNumber { get; set; }
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

    public class EmployeeResponseFull
    {
        /// <summary>
        /// The Age of the Employee
        /// </summary>
        /// <example>26</example>
        public byte Age { get; set; }
        /// <summary>
        /// The Attrition of the Employee
        /// </summary>
        /// <example>true</example>
        public bool Attrition { get; set; }
        /// <summary>
        /// The amount of Business Traveling the Employee does
        /// </summary>
        /// <example>Travel_Rarely</example>
        public string BusinessTravel { get; set; }
        /// <summary>
        /// The DailyRate that the employee is paid
        /// </summary>
        /// <example>1392</example>
        public short DailyRate { get; set; }
        /// <summary>
        /// The Deparment the Employee works in
        /// </summary>
        /// <example>Research and Development</example>
        public string Department { get; set; }
        /// <summary>
        /// The distance the work place is from the Employee's home
        /// </summary>
        /// <example>15</example>
        public byte DistanceFromHome { get; set; }
        /// <summary>
        /// The Education Level of the Employee
        /// </summary>
        /// <example>3</example>
        public byte Education { get; set; }
        /// <summary>
        /// The Education Field of the Employee
        /// </summary>
        /// <example>Life Sciences</example>
        public string EducationField { get; set; }
        /// <summary>
        /// The Employee Id/Number (Primary Key and Unqiue Identifier)
        /// </summary>
        /// <example>55</example>
        public int EmployeeNumber { get; set; }
        /// <summary>
        /// How Satisfied the employee is in the Environment out of 5
        /// </summary>
        /// <example>3</example>
        public byte EnvironmentSatisfaction { get; set; }
        /// <summary>
        /// The Gender of the Employee
        /// </summary>
        /// <example>Female</example>
        public string Gender { get; set; }
        /// <summary>
        /// The HourlyRate that the employee is paid
        /// </summary>
        /// <example>94</example>
        public byte HourlyRate { get; set; }
        /// <summary>
        /// The JobInvolvement of the employee
        /// </summary>
        /// <example>2</example>
        public byte JobInvolvement { get; set; }
        /// <summary>
        /// The JobLevel of the employee
        /// </summary>
        /// <example>1</example>
        public byte JobLevel { get; set; }
        /// <summary>
        /// The JobRole of the employee
        /// </summary>
        /// <example>Research Scientist</example>
        public string JobRole { get; set; }
        /// <summary>
        ///  How Satisfied the employee is with their job out of 5
        /// </summary>
        /// <example>Research Scientist</example>
        public byte JobSatisfaction { get; set; }
        /// <summary>
        ///  The Marital Status of Employee
        /// </summary>
        /// <example>Single</example>
        public string MaritalStatus { get; set; }
        /// <summary>
        ///  The Monthly income paid to the Employee
        /// </summary>
        /// <example>12000</example>
        public short MonthlyIncome { get; set; }
        /// <summary>
        ///  The monthly rate that the employee is paid.
        /// </summary>
        /// <example>9980</example>
        public short MonthlyRate { get; set; }
        /// <summary>
        ///  The Number of companies the Employee has worked at.
        /// </summary>
        /// <example>5</example>
        public byte NumCompaniesWorked { get; set; }
        /// <summary>
        ///  Does the Employee work over time (true/false)
        /// </summary>
        /// <example>true</example>
        public bool OverTime { get; set; }
        /// <summary>
        ///  Percentage Salary Hike for Employee
        /// </summary>
        /// <example>5</example>
        public byte PercentSalaryHike { get; set; }
        /// <summary>
        ///  The Performance Rating of the Employee out of 5
        /// </summary>
        /// <example>5</example>
        public byte PerformanceRating { get; set; }
        /// <summary>
        ///  How Satisfied the employee is with Relationship job out of 5
        /// </summary>
        /// <example>Research Scientist</example>
        public byte RelationshipSatisfaction { get; set; }
        /// <summary>
        ///  Standard hours of work for Employee
        /// </summary>
        /// <example>80</example>
        public byte StandardHours { get; set; }
        /// <summary>
        ///  StockOption Level provided to Employee
        /// </summary>
        /// <example>2</example>
        public byte StockOptionLevel { get; set; }
        /// <summary>
        ///  Total Years Working at Company
        /// </summary>
        /// <example>3</example>
        public byte TotalWorkingYears { get; set; }
        /// <summary>
        ///  Total Time Traning Last year 
        /// </summary>
        /// <example>4</example>
        public byte TrainingTimesLastYear { get; set; }
        /// <summary>
        ///  Work Life Balance Score out of 5
        /// </summary>
        /// <example>4</example>
        public byte WorkLifeBalance { get; set; }
        /// <summary>
        ///  Years Employee has been at Company
        /// </summary>
        /// <example>3</example>
        public byte YearsAtCompany { get; set; }
        /// <summary>
        ///  Years Employee has been at Company in current role
        /// </summary>
        /// <example>2</example>
        public byte YearsInCurrentRole { get; set; }
        /// <summary>
        ///  Years since employee was last promoted
        /// </summary>
        /// <example>2</example>
        public byte YearsSinceLastPromotion { get; set; }
        /// <summary>
        ///  Years that employee has been with current Manager
        /// </summary>
        /// <example>2</example>
        public byte YearsWithCurrManager { get; set; }
    }
    public class EmployeeResponsePartial
    {
        /// <summary>
        /// The Age of the Employee
        /// </summary>
        /// <example>26</example>
        public byte Age { get; set; }
        /// <summary>
        /// The Attrition of the Employee
        /// </summary>
        /// <example>true</example>
        public bool Attrition { get; set; }
        /// <summary>
        /// The amount of Business Traveling the Employee does
        /// </summary>
        /// <example>Travel_Rarely</example>
        public string BusinessTravel { get; set; }
        /// <summary>
        /// The Deparment the Employee works in
        /// </summary>
        /// <example>Research and Development</example>
        public string Department { get; set; }
        /// <summary>
        /// The distance the work place is from the Employee's home
        /// </summary>
        /// <example>15</example>
        public byte DistanceFromHome { get; set; }
        /// <summary>
        /// The Education Level of the Employee
        /// </summary>
        /// <example>3</example>
        public byte Education { get; set; }
        /// <summary>
        /// The Education Field of the Employee
        /// </summary>
        /// <example>Life Sciences</example>
        public string EducationField { get; set; }
        /// <summary>
        /// The Employee Id/Number (Primary Key and Unqiue Identifier)
        /// </summary>
        /// <example>55</example>
        public int EmployeeNumber { get; set; }
        /// <summary>
        /// The Gender of the Employee
        /// </summary>
        /// <example>Female</example>
        public string Gender { get; set; }
        /// <summary>
        /// The JobLevel of the employee
        /// </summary>
        /// <example>1</example>
        public byte JobLevel { get; set; }
        /// <summary>
        /// The JobRole of the employee
        /// </summary>
        /// <example>Research Scientist</example>
        public string JobRole { get; set; }
        /// <summary>
        ///  The Marital Status of Employee
        /// </summary>
        /// <example>Single</example>
        public string MaritalStatus { get; set; }
        /// <summary>
        ///  Does the Employee work over time (true/false)
        /// </summary>
        /// <example>true</example>
        public bool OverTime { get; set; }
        /// <summary>
        ///  Standard hours of work for Employee
        /// </summary>
        /// <example>80</example>
        public byte StandardHours { get; set; }
        /// <summary>
        ///  Total Years Working at Company
        /// </summary>
        /// <example>3</example>
        public byte TotalWorkingYears { get; set; }
        /// <summary>
        ///  Years Employee has been at Company
        /// </summary>
        /// <example>3</example>
        public byte YearsAtCompany { get; set; }
        /// <summary>
        ///  Years Employee has been at Company in current role
        /// </summary>
        /// <example>2</example>
        public byte YearsInCurrentRole { get; set; }
    }

    public class EmployeeUpdate
    {
        /// <summary>
        /// The Age of the Employee
        /// </summary>
        /// <example>26</example>
        public byte Age { get; set; }
        /// <summary>
        /// The Attrition of the Employee
        /// </summary>
        /// <example>true</example>
        public bool? Attrition { get; set; }
        /// <summary>
        /// The amount of Business Traveling the Employee does
        /// </summary>
        /// <example>Travel_Rarely</example>
        public string BusinessTravel { get; set; }
        /// <summary>
        /// The DailyRate that the employee is paid
        /// </summary>
        /// <example>1392</example>
        public short DailyRate { get; set; }
        /// <summary>
        /// The Deparment the Employee works in
        /// </summary>
        /// <example>Research and Development</example>
        public string Department { get; set; }
        /// <summary>
        /// The distance the work place is from the Employee's home
        /// </summary>
        /// <example>15</example>
        public byte DistanceFromHome { get; set; }
        /// <summary>
        /// The Education Level of the Employee
        /// </summary>
        /// <example>3</example>
        public byte Education { get; set; }
        /// <summary>
        /// The Education Field of the Employee
        /// </summary>
        /// <example>Life Sciences</example>
        public string EducationField { get; set; }
        /// <summary>
        /// The Employee Id/Number (Primary Key and Unqiue Identifier)
        /// </summary>
        /// <example>55</example>
        public int EmployeeNumber { get; set; }
        /// <summary>
        /// How Satisfied the employee is in the Environment out of 5
        /// </summary>
        /// <example>3</example>
        public byte EnvironmentSatisfaction { get; set; }
        /// <summary>
        /// The Gender of the Employee
        /// </summary>
        /// <example>Female</example>
        public string Gender { get; set; }
        /// <summary>
        /// The HourlyRate that the employee is paid
        /// </summary>
        /// <example>94</example>
        public byte HourlyRate { get; set; }
        /// <summary>
        /// The JobInvolvement of the employee
        /// </summary>
        /// <example>2</example>
        public byte JobInvolvement { get; set; }
        /// <summary>
        /// The JobLevel of the employee
        /// </summary>
        /// <example>1</example>
        public byte JobLevel { get; set; }
        /// <summary>
        /// The JobRole of the employee
        /// </summary>
        /// <example>Research Scientist</example>
        public string JobRole { get; set; }
        /// <summary>
        ///  How Satisfied the employee is with their job out of 5
        /// </summary>
        /// <example>Research Scientist</example>
        public byte JobSatisfaction { get; set; }
        /// <summary>
        ///  The Marital Status of Employee
        /// </summary>
        /// <example>Single</example>
        public string MaritalStatus { get; set; }
        /// <summary>
        ///  The Monthly income paid to the Employee
        /// </summary>
        /// <example>12000</example>
        public short MonthlyIncome { get; set; }
        /// <summary>
        ///  The monthly rate that the employee is paid.
        /// </summary>
        /// <example>9980</example>
        public short MonthlyRate { get; set; }
        /// <summary>
        ///  The Number of companies the Employee has worked at.
        /// </summary>
        /// <example>5</example>
        public byte NumCompaniesWorked { get; set; }
        /// <summary>
        ///  Does the Employee work over time (true/false)
        /// </summary>
        /// <example>true</example>
        public bool? OverTime { get; set; }
        /// <summary>
        ///  Percentage Salary Hike for Employee
        /// </summary>
        /// <example>5</example>
        public byte PercentSalaryHike { get; set; }
        /// <summary>
        ///  The Performance Rating of the Employee out of 5
        /// </summary>
        /// <example>5</example>
        public byte PerformanceRating { get; set; }
        /// <summary>
        ///  How Satisfied the employee is with Relationship job out of 5
        /// </summary>
        /// <example>Research Scientist</example>
        public byte RelationshipSatisfaction { get; set; }
        /// <summary>
        ///  Standard hours of work for Employee
        /// </summary>
        /// <example>80</example>
        public byte StandardHours { get; set; }
        /// <summary>
        ///  StockOption Level provided to Employee
        /// </summary>
        /// <example>2</example>
        public byte StockOptionLevel { get; set; }
        /// <summary>
        ///  Total Years Working at Company
        /// </summary>
        /// <example>3</example>
        public byte TotalWorkingYears { get; set; }
        /// <summary>
        ///  Total Time Traning Last year 
        /// </summary>
        /// <example>4</example>
        public byte TrainingTimesLastYear { get; set; }
        /// <summary>
        ///  Work Life Balance Score out of 5
        /// </summary>
        /// <example>4</example>
        public byte WorkLifeBalance { get; set; }
        /// <summary>
        ///  Years Employee has been at Company
        /// </summary>
        /// <example>3</example>
        public byte YearsAtCompany { get; set; }
        /// <summary>
        ///  Years Employee has been at Company in current role
        /// </summary>
        /// <example>2</example>
        public byte YearsInCurrentRole { get; set; }
        /// <summary>
        ///  Years since employee was last promoted
        /// </summary>
        /// <example>2</example>
        public byte YearsSinceLastPromotion { get; set; }
        /// <summary>
        ///  Years that employee has been with current Manager
        /// </summary>
        /// <example>2</example>
        public byte YearsWithCurrManager { get; set; }
    }

    public class EmployeeCreate
    {
        /// <summary>
        /// The Age of the Employee
        /// </summary>
        /// <example>26</example>
        [Required]
        public byte Age { get; set; }
        /// <summary>
        /// The Attrition of the Employee
        /// </summary>
        /// <example>true</example>
        [Required]
        public bool Attrition { get; set; }
        /// <summary>
        /// The amount of Business Traveling the Employee does
        /// </summary>
        /// <example>Travel_Rarely</example>
        [Required]
        public string BusinessTravel { get; set; }
        /// <summary>
        /// The DailyRate that the employee is paid
        /// </summary>
        /// <example>1392</example>
        [Required]
        public short DailyRate { get; set; }
        /// <summary>
        /// The Deparment the Employee works in
        /// </summary>
        /// <example>Research and Development</example>
        [Required]
        public string Department { get; set; }
        /// <summary>
        /// The distance the work place is from the Employee's home
        /// </summary>
        /// <example>15</example>
        [Required]
        public byte DistanceFromHome { get; set; }
        /// <summary>
        /// The Education Level of the Employee
        /// </summary>
        /// <example>3</example>
        [Required]
        public byte Education { get; set; }
        /// <summary>
        /// The Education Field of the Employee
        /// </summary>
        /// <example>Life Sciences</example>
        [Required]
        public string EducationField { get; set; }
        /// <summary>
        /// The Employee Id/Number (Primary Key and Unqiue Identifier)
        /// </summary>
        /// <example>55</example>
        [Required]
        public int EmployeeNumber { get; set; }
        /// <summary>
        /// How Satisfied the employee is in the Environment out of 5
        /// </summary>
        /// <example>3</example>
        [Required]
        public byte EnvironmentSatisfaction { get; set; }
        /// <summary>
        /// The Gender of the Employee
        /// </summary>
        /// <example>Female</example>
        [Required]
        public string Gender { get; set; }
        /// <summary>
        /// The HourlyRate that the employee is paid
        /// </summary>
        /// <example>94</example>
        [Required]
        public byte HourlyRate { get; set; }
        /// <summary>
        /// The JobInvolvement of the employee
        /// </summary>
        /// <example>2</example>
        [Required]
        public byte JobInvolvement { get; set; }
        /// <summary>
        /// The JobLevel of the employee
        /// </summary>
        /// <example>1</example>
        [Required]
        public byte JobLevel { get; set; }
        /// <summary>
        /// The JobRole of the employee
        /// </summary>
        /// <example>Research Scientist</example>
        [Required]
        public string JobRole { get; set; }
        /// <summary>
        ///  How Satisfied the employee is with their job out of 5
        /// </summary>
        /// <example>Research Scientist</example>
        [Required]
        public byte JobSatisfaction { get; set; }
        /// <summary>
        ///  The Marital Status of Employee
        /// </summary>
        /// <example>Single</example>
        [Required]
        public string MaritalStatus { get; set; }
        /// <summary>
        ///  The Monthly income paid to the Employee
        /// </summary>
        /// <example>12000</example>
        [Required]
        public short MonthlyIncome { get; set; }
        /// <summary>
        ///  The monthly rate that the employee is paid.
        /// </summary>
        /// <example>9980</example>
        [Required]
        public short MonthlyRate { get; set; }
        /// <summary>
        ///  The Number of companies the Employee has worked at.
        /// </summary>
        /// <example>5</example>
        [Required]
        public byte NumCompaniesWorked { get; set; }
        /// <summary>
        ///  Does the Employee work over time (true/false)
        /// </summary>
        /// <example>true</example>
        [Required]
        public bool OverTime { get; set; }
        /// <summary>
        ///  Percentage Salary Hike for Employee
        /// </summary>
        /// <example>5</example>
        [Required]
        public byte PercentSalaryHike { get; set; }
        /// <summary>
        ///  The Performance Rating of the Employee out of 5
        /// </summary>
        /// <example>5</example>
        [Required]
        public byte PerformanceRating { get; set; }
        /// <summary>
        ///  How Satisfied the employee is with Relationship job out of 5
        /// </summary>
        /// <example>Research Scientist</example>
        [Required]
        public byte RelationshipSatisfaction { get; set; }
        /// <summary>
        ///  Standard hours of work for Employee
        /// </summary>
        /// <example>80</example>
        [Required]
        public byte StandardHours { get; set; }
        /// <summary>
        ///  StockOption Level provided to Employee
        /// </summary>
        /// <example>2</example>
        [Required]
        public byte StockOptionLevel { get; set; }
        /// <summary>
        ///  Total Years Working at Company
        /// </summary>
        /// <example>3</example>
        [Required]
        public byte TotalWorkingYears { get; set; }
        /// <summary>
        ///  Total Time Traning Last year 
        /// </summary>
        /// <example>4</example>
        [Required]
        public byte TrainingTimesLastYear { get; set; }
        /// <summary>
        ///  Work Life Balance Score out of 5
        /// </summary>
        /// <example>4</example>
        [Required]
        public byte WorkLifeBalance { get; set; }
        /// <summary>
        ///  Years Employee has been at Company
        /// </summary>
        /// <example>3</example>
        [Required]
        public byte YearsAtCompany { get; set; }
        /// <summary>
        ///  Years Employee has been at Company in current role
        /// </summary>
        /// <example>2</example>
        [Required]
        public byte YearsInCurrentRole { get; set; }
        /// <summary>
        ///  Years since employee was last promoted
        /// </summary>
        /// <example>2</example>
        [Required]
        public byte YearsSinceLastPromotion { get; set; }
        /// <summary>
        ///  Years that employee has been with current Manager
        /// </summary>
        /// <example>2</example>
        [Required]
        public byte YearsWithCurrManager { get; set; }
    }

}
