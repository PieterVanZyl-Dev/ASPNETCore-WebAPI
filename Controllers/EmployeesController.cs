using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DimensionDataAPIContext _context;

        public EmployeesController(DimensionDataAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of all Employee Details (Varying by Role)
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Returns the List of Employees Partial/Full item</response>
        /// <response code="400">Token Provider User Doesn't exist any more</response>  
        /// <response code="401">If the User is Unauthorized</response>  
        // GET: api/Employees
        [ProducesResponseType(typeof(EmployeeResponseFull), 200)]
        [ProducesResponseType(typeof(EmployeeResponsePartial), 200)]
        [HttpGet]
        public async Task<ActionResult> GetEmployee()
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            IEnumerable<Claim> claim = identity.Claims;

            var RoleClaim = claim
                .Where(x => x.Type == ClaimTypes.Role)
                .FirstOrDefault();


            if(RoleClaim == null)
            {
                BadRequest();

            }
            bool isadmin = RoleClaim.Value == "admin";

            if (isadmin)
            {


               var employee = await _context.Employee
               .Include("GenderNavigation")
               .Include("BusinessTravelNavigation")
               .Include("DepartmentNavigation")
               .Include("EducationFieldNavigation")
               .Include("JobRoleNavigation")
               .Include("MaritalStatusNavigation")
               .Include("EmployeeSurveyNavigation")
               .Select(a => new EmployeeResponseFull
               {
                   Age = a.Age,
                   Attrition = a.Attrition,
                   BusinessTravel = a.BusinessTravelNavigation.BusinessTravel1,
                   DailyRate = a.DailyRate,
                   Department = a.DepartmentNavigation.Department1,
                   DistanceFromHome = a.DistanceFromHome,
                   Education = a.Education,
                   EducationField = a.EducationFieldNavigation.EducationField1,
                   EmployeeNumber = a.EmployeeNumber,
                   Gender = a.GenderNavigation.Gender1,
                   HourlyRate = a.HourlyRate,
                   JobInvolvement = a.JobInvolvement,
                   JobLevel = a.JobLevel,
                   JobRole = a.JobRoleNavigation.JobRole1,
                   MaritalStatus = a.MaritalStatusNavigation.MaritalStatus1,
                   MonthlyIncome = a.MonthlyIncome,
                   MonthlyRate = a.MonthlyRate,
                   NumCompaniesWorked = a.NumCompaniesWorked,
                   OverTime = a.OverTime,
                   PercentSalaryHike = a.PercentSalaryHike,
                   PerformanceRating = a.PerformanceRating,
                   StandardHours = a.StandardHours,
                   StockOptionLevel = a.StockOptionLevel,
                   TotalWorkingYears = a.TotalWorkingYears,
                   TrainingTimesLastYear = a.TrainingTimesLastYear,
                   YearsAtCompany = a.YearsAtCompany,
                   YearsInCurrentRole = a.YearsInCurrentRole,
                   YearsSinceLastPromotion = a.YearsSinceLastPromotion,
                   YearsWithCurrManager = a.YearsWithCurrManager,
                   EnvironmentSatisfaction = a.EmployeeSurveyNavigation.EnvironmentSatisfaction,
                   RelationshipSatisfaction = a.EmployeeSurveyNavigation.RelationshipSatisfaction,
                   JobSatisfaction = a.EmployeeSurveyNavigation.JobSatisfaction,
                   WorkLifeBalance = a.EmployeeSurveyNavigation.WorkLifeBalance
               })
               .ToListAsync();

                return Ok(employee);


            }
            else //if not admin 
            {
                var employee = await _context.Employee
                  .Include("GenderNavigation")
                  .Include("BusinessTravelNavigation")
                  .Include("DepartmentNavigation")
                  .Include("EducationFieldNavigation")
                  .Include("JobRoleNavigation")
                  .Include("MaritalStatusNavigation")
                  .Select(a => new EmployeeResponsePartial
                  {
                      Age = a.Age,
                      Attrition = a.Attrition,
                      BusinessTravel = a.BusinessTravelNavigation.BusinessTravel1,
                      Department = a.DepartmentNavigation.Department1,
                      DistanceFromHome = a.DistanceFromHome,
                      Education = a.Education,
                      EducationField = a.EducationFieldNavigation.EducationField1,
                      EmployeeNumber = a.EmployeeNumber,
                      Gender = a.GenderNavigation.Gender1,
                      JobLevel = a.JobLevel,
                      JobRole = a.JobRoleNavigation.JobRole1,
                      MaritalStatus = a.MaritalStatusNavigation.MaritalStatus1,
                      OverTime = a.OverTime,
                      StandardHours = a.StandardHours,
                      TotalWorkingYears = a.TotalWorkingYears,
                      YearsAtCompany = a.YearsAtCompany,
                      YearsInCurrentRole = a.YearsInCurrentRole,
                  })
                  .ToListAsync();

                return Ok(employee);

            }



                 
                }

        /// <summary>
        /// Gets an Employee Details.
        /// </summary>
        /// <param name="id" example="10"></param>
        /// <returns>Employee Details with varying detail</returns>
        /// <response code="200">Returns the Employee item</response>
        /// <response code="400">Token Provider User Doesn't exist any more</response>  
        /// <response code="401">If the User is Unauthorized</response>  
        /// <response code="404">If Username Not found</response>  
        /// 
        // GET: api/Employees/55
        [ProducesResponseType(typeof(EmployeeResponseFull), 200)]
        [ProducesResponseType(typeof(EmployeeResponsePartial), 200)]
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetEmployee(int id)
        {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                IEnumerable<Claim> claim = identity.Claims;

                var RoleClaim = claim
                    .Where(x => x.Type == ClaimTypes.Role)
                    .FirstOrDefault();

                var usernameClaim = claim
                    .Where(x => x.Type == ClaimTypes.Name)
                    .FirstOrDefault();

            var userName = await _context.User
                    .Where(x => x.Id == Int32.Parse(usernameClaim.Value))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

            if (userName == null)
            {
                return BadRequest(new { message = "Woops" });
            }

            bool isadmin = RoleClaim.Value == "admin";
            bool isSelf = userName.EmployeeNumber == id;




            object employee = null;
            //if user is admin or user is requesting their own information
            if (isadmin || isSelf)
            { 


             employee = await _context.Employee
                .Include("GenderNavigation")
                .Include("BusinessTravelNavigation")
                .Include("DepartmentNavigation")
                .Include("EducationFieldNavigation")
                .Include("JobRoleNavigation")
                .Include("MaritalStatusNavigation")
                .Include("EmployeeSurveyNavigation")
                .Where(emp => emp.EmployeeNumber == id)
                .Select(a => new EmployeeResponseFull
                {
                    Age = a.Age,
                    Attrition = a.Attrition,
                    BusinessTravel = a.BusinessTravelNavigation.BusinessTravel1,
                    DailyRate = a.DailyRate,
                    Department = a.DepartmentNavigation.Department1,
                    DistanceFromHome = a.DistanceFromHome,
                    Education = a.Education,
                    EducationField = a.EducationFieldNavigation.EducationField1,
                    EmployeeNumber = a.EmployeeNumber,
                    Gender = a.GenderNavigation.Gender1,
                    HourlyRate = a.HourlyRate,
                    JobInvolvement = a.JobInvolvement,
                    JobLevel = a.JobLevel,
                    JobRole = a.JobRoleNavigation.JobRole1,
                    MaritalStatus = a.MaritalStatusNavigation.MaritalStatus1,
                    MonthlyIncome = a.MonthlyIncome,
                    MonthlyRate = a.MonthlyRate,
                    NumCompaniesWorked = a.NumCompaniesWorked,
                    OverTime = a.OverTime,
                    PercentSalaryHike = a.PercentSalaryHike,
                    PerformanceRating = a.PerformanceRating,
                    StandardHours = a.StandardHours,
                    StockOptionLevel = a.StockOptionLevel,
                    TotalWorkingYears = a.TotalWorkingYears,
                    TrainingTimesLastYear = a.TrainingTimesLastYear,
                    YearsAtCompany = a.YearsAtCompany,
                    YearsInCurrentRole = a.YearsInCurrentRole,
                    YearsSinceLastPromotion = a.YearsSinceLastPromotion,
                    YearsWithCurrManager = a.YearsWithCurrManager,
                    EnvironmentSatisfaction = a.EmployeeSurveyNavigation.EnvironmentSatisfaction,
                    RelationshipSatisfaction = a.EmployeeSurveyNavigation.RelationshipSatisfaction,
                    JobSatisfaction = a.EmployeeSurveyNavigation.JobSatisfaction,
                    WorkLifeBalance = a.EmployeeSurveyNavigation.WorkLifeBalance
                })
                .FirstOrDefaultAsync();
            }
            else //if not admin or not getting your own information then return partial info.
            {
              employee = await _context.Employee
                .Include("GenderNavigation")
                .Include("BusinessTravelNavigation")
                .Include("DepartmentNavigation")
                .Include("EducationFieldNavigation")
                .Include("JobRoleNavigation")
                .Include("MaritalStatusNavigation")
                .Where(emp => emp.EmployeeNumber == id)
                .Select(a => new EmployeeResponsePartial
                {
                    Age = a.Age,
                    Attrition = a.Attrition,
                    BusinessTravel = a.BusinessTravelNavigation.BusinessTravel1,
                    Department = a.DepartmentNavigation.Department1,
                    DistanceFromHome = a.DistanceFromHome,
                    Education = a.Education,
                    EducationField = a.EducationFieldNavigation.EducationField1,
                    EmployeeNumber = a.EmployeeNumber,
                    Gender = a.GenderNavigation.Gender1,
                    JobLevel = a.JobLevel,
                    JobRole = a.JobRoleNavigation.JobRole1,
                    MaritalStatus = a.MaritalStatusNavigation.MaritalStatus1,
                    OverTime = a.OverTime,
                    StandardHours = a.StandardHours,
                    TotalWorkingYears = a.TotalWorkingYears,
                    YearsAtCompany = a.YearsAtCompany,
                    YearsInCurrentRole = a.YearsInCurrentRole,
                }).
                FirstOrDefaultAsync();


            }

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }





        /// <summary>
        /// Updates an Employee's Details.
        /// </summary>
        /// <param name="id" example="10"></param>
        /// <param name="employee"></param>
        /// <returns>Employee Details with varying detail</returns>
        /// <response code="200">Returns if successful </response>  
        /// <response code="400">Token Provider User Doesn't exist any more</response>  
        /// <response code="401">If the User is Unauthorized</response>  
        /// <response code="404">If Username Not found</response>  
        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeUpdate employee)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            IEnumerable<Claim> claim = identity.Claims;

            var RoleClaim = claim
                .Where(x => x.Type == ClaimTypes.Role)
                .FirstOrDefault();

            var usernameClaim = claim
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();

            var userName = await _context.User
                    .Where(x => x.Id == Int32.Parse(usernameClaim.Value))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

            var employeeobj = await _context.Employee
                .Where(x => x.EmployeeNumber == id)
                .FirstOrDefaultAsync();

            EmployeeSurvey employeessurveyobj = new EmployeeSurvey();

            if (userName == null)
            {
                return BadRequest(new { message = "Woops, that user doesn't exist" });
            }

            bool isAdmin = RoleClaim.Value == "admin";
            bool isSelf = userName.EmployeeNumber == id;

            if (isAdmin || isSelf)
            {


                if (employee.Age > 0)
                {
                    employeeobj.Age = employee.Age;
                }

                if (employee.Attrition != null)
                {
                    employeeobj.Attrition = employee.Attrition ?? default(bool);
                }

                if (employee.BusinessTravel != null)
                {
                    var BusinessTravel = await _context.BusinessTravel
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.BusinessTravel1 == employee.BusinessTravel);

                    if (BusinessTravel != null)
                    {
                        employeeobj.BusinessTravel = BusinessTravel.BusinessTravelId;
                    }else
                    {
                        return BadRequest(new { message = "Business Travel Value needs to match the table's value or you need to add a new value" });
                    }
                }

                if (employee.DailyRate > 0)
                {
                    employeeobj.DailyRate = employee.DailyRate;
                }

                if (employee.Department != null)
                {
                    var Department = await _context.Department
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Department1 == employee.Department);

                    if (Department != null)
                    {
                        employeeobj.Department = Department.DepartmentId;
                    }
                    else
                    {
                        return BadRequest(new { message = "Department Value needs to match the table's value or you need to add a new department" });
                    }
                }

                if (employee.DistanceFromHome > 0)
                {
                    employeeobj.DistanceFromHome = employee.DistanceFromHome;
                }

                if (employee.Education > 0)
                {
                    employeeobj.Education = employee.Education;
                }

                if (employee.Education > 0)
                {
                    employeeobj.Education = employee.Education;
                }

                if (employee.EducationField != null)
                {
                    var EducationField = await _context.EducationField
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.EducationField1 == employee.EducationField);

                    if (EducationField != null)
                    {
                        employeeobj.EducationField = EducationField.EducationFieldId;
                    }
                    else
                    {
                        return BadRequest(new { message = "Education Field Value needs to match the table's value or you need to add a new Education Field" });
                    }
                }

                if (employee.Gender != null)
                {
                    var Gender = await _context.Gender
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Gender1 == employee.Gender);

                    if (Gender != null)
                    {
                        employeeobj.EducationField = Gender.GenderId;
                    }
                    else
                    {
                        return BadRequest(new { message = "That Gender Value needs to match the table's value or you need to add a new Gender" });
                    }
                }

                if (employee.HourlyRate > 0 && !isAdmin)
                {
                    return Forbid();
                }
                else if(employee.HourlyRate > 0 && isAdmin)
                {
                    employeeobj.HourlyRate = employee.HourlyRate;
                }

                if (employee.JobInvolvement > 0)
                {
                    employeeobj.JobInvolvement = employee.JobInvolvement;
                }

                if (employee.JobLevel > 0)
                {
                    employeeobj.JobLevel = employee.JobLevel;
                }

                if (employee.MaritalStatus != null)
                {
                    var MaritalStatus = await _context.MaritalStatus
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.MaritalStatus1 == employee.MaritalStatus);

                    if (MaritalStatus != null)
                    {
                        employeeobj.MaritalStatus = MaritalStatus.MaritalStatusId;
                    }
                    else
                    {
                        return BadRequest(new { message = "That Marital Status needs to match the table's value or you need to add a new Marital Status" });
                    }
                }

                if (employee.MonthlyIncome > 0 && !isAdmin)
                {
                    return Forbid();
                }
                else if (employee.MonthlyIncome > 0 && isAdmin)
                {
                    employeeobj.MonthlyIncome = employee.MonthlyIncome;
                }

                if (employee.MonthlyRate > 0 && !isAdmin)
                {
                    return Forbid();
                }
                else if (employee.MonthlyRate > 0 && isAdmin)
                {
                    employeeobj.MonthlyRate = employee.MonthlyRate;
                }

                if (employee.NumCompaniesWorked > 0)
                {
                    employeeobj.NumCompaniesWorked = employee.NumCompaniesWorked;
                }

                if (employee.OverTime != null)
                {
                    employeeobj.OverTime = employee.OverTime ?? default(bool);
                }

                if (employee.PercentSalaryHike > 0 && !isAdmin)
                {
                    return Forbid();
                }
                else if (employee.PercentSalaryHike > 0 && isAdmin)
                {
                    employeeobj.PercentSalaryHike = employee.PercentSalaryHike;
                }


                if (employee.PerformanceRating > 0 && !isAdmin)
                {
                    return Forbid();
                }
                else if (employee.PerformanceRating > 0 && isAdmin)
                {
                    employeeobj.PerformanceRating = employee.PerformanceRating;
                }

                if (employee.StandardHours > 0)
                {
                    employeeobj.StandardHours = employee.StandardHours;
                }

                if (employee.StockOptionLevel > 0)
                {
                    employeeobj.StockOptionLevel = employee.StockOptionLevel;
                }

                if (employee.TotalWorkingYears > 0)
                {
                    employeeobj.TotalWorkingYears = employee.TotalWorkingYears;
                }

                if (employee.TrainingTimesLastYear > 0)
                {
                    employeeobj.TrainingTimesLastYear = employee.TrainingTimesLastYear;
                }

                if (employee.YearsAtCompany > 0)
                {
                    employeeobj.YearsAtCompany = employee.YearsAtCompany;
                }

                if (employee.YearsInCurrentRole > 0)
                {
                    employeeobj.YearsInCurrentRole = employee.YearsInCurrentRole;
                }

                if (employee.YearsSinceLastPromotion > 0)
                {
                    employeeobj.YearsSinceLastPromotion = employee.YearsSinceLastPromotion;
                }

                if (employee.YearsWithCurrManager > 0)
                {
                    employeeobj.YearsWithCurrManager = employee.YearsWithCurrManager;
                }

                int surveycount = 0;

                if(employee.EnvironmentSatisfaction > 0)
                {
                    employeessurveyobj.EnvironmentSatisfaction = employee.EnvironmentSatisfaction;
                    surveycount++;
                }
                if (employee.RelationshipSatisfaction > 0)
                {
                    employeessurveyobj.RelationshipSatisfaction = employee.RelationshipSatisfaction;
                    surveycount++;
                }
                if (employee.JobSatisfaction > 0)
                {
                    employeessurveyobj.JobSatisfaction = employee.JobSatisfaction;
                    surveycount++;
                }
                if (employee.WorkLifeBalance > 0)
                {
                    employeessurveyobj.WorkLifeBalance = employee.WorkLifeBalance;
                    surveycount++;
                }
                if(surveycount == 4) //if all values added then add new survey
                {

                    employeessurveyobj.EmployeeId = id;
                    _context.EmployeeSurvey.Add(employeessurveyobj);

                }
                else if(surveycount > 0 && surveycount != 4) //if some values added, I assume you're fixing a survey mistake.
                {
                    var employeessurveyobjoriginal = await _context.EmployeeSurvey
                        .OrderByDescending(x => x.EmployeeId == id)
                        .FirstOrDefaultAsync();

                    employeessurveyobjoriginal.EnvironmentSatisfaction = employeessurveyobj.EnvironmentSatisfaction > 0 ? default(byte) : employeessurveyobj.EnvironmentSatisfaction;
                    employeessurveyobjoriginal.EnvironmentSatisfaction = employeessurveyobj.RelationshipSatisfaction > 0 ? default(byte) : employeessurveyobj.RelationshipSatisfaction;
                    employeessurveyobjoriginal.EnvironmentSatisfaction = employeessurveyobj.JobSatisfaction > 0 ? default(byte) : employeessurveyobj.JobSatisfaction;
                    employeessurveyobjoriginal.EnvironmentSatisfaction = employeessurveyobj.WorkLifeBalance > 0 ? default(byte) : employeessurveyobj.WorkLifeBalance;

                    _context.Entry(employeessurveyobjoriginal).State = EntityState.Modified;
                }




                var latestemployeesurvey = await _context.EmployeeSurvey
                    .AsNoTracking()
                    .OrderByDescending(x => x.EmployeeId == id)
                    .FirstOrDefaultAsync();

                employeeobj.EmployeeSurvey = latestemployeesurvey.SurveyId;

                _context.Entry(employeeobj).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return Forbid();
            }

            return Ok(new { message = "Modified Employee: " + id +" with provided values.\n" + employee });
        }

        /// <summary>
        /// Creates a new Employee.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee Details with varying detail</returns>
        /// <response code="200">Returns if successful </response>  
        /// <response code="400">Token Provider User Doesn't exist any more</response>  
        /// <response code="401">If the User is Unauthorized</response>  
        /// <response code="404">If Username Not found</response>  
        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeCreate employee)
        {

                        var identity = HttpContext.User.Identity as ClaimsIdentity;

            IEnumerable<Claim> claim = identity.Claims;

            var RoleClaim = claim
                .Where(x => x.Type == ClaimTypes.Role)
                .FirstOrDefault();

            var usernameClaim = claim
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();

            var userName = await _context.User
                    .Where(x => x.Id == Int32.Parse(usernameClaim.Value))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

            Employee employeeobj = new Employee();

            EmployeeSurvey employeessurveyobj = new EmployeeSurvey();

            if (userName == null)
            {
                return BadRequest(new { message = "Woops" });
            }

            bool isAdmin = RoleClaim.Value == "admin";

            if (isAdmin)
            {

                if (employee.Age > 0)
                {
                    employeeobj.Age = employee.Age;
                }

                if (employee.Attrition != null)
                {
                    employeeobj.Attrition = employee.Attrition;
                }

                if (employee.BusinessTravel != null)
                {
                    var BusinessTravel = await _context.BusinessTravel
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.BusinessTravel1 == employee.BusinessTravel);

                    if (BusinessTravel != null)
                    {
                        employeeobj.BusinessTravel = BusinessTravel.BusinessTravelId;
                    }
                    else
                    {
                        return BadRequest(new { message = "Business Travel Value needs to match the table's value or you need to add a new value" });
                    }
                }

                if (employee.DailyRate > 0)
                {
                    employeeobj.DailyRate = employee.DailyRate;
                }

                if (employee.Department != null)
                {
                    var Department = await _context.Department
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Department1 == employee.Department);

                    if (Department != null)
                    {
                        employeeobj.Department = Department.DepartmentId;
                    }
                    else
                    {
                        return BadRequest(new { message = "Department Value needs to match the table's value or you need to add a new department" });
                    }
                }

                if (employee.DistanceFromHome > 0)
                {
                    employeeobj.DistanceFromHome = employee.DistanceFromHome;
                }

                if (employee.Education > 0)
                {
                    employeeobj.Education = employee.Education;
                }

                if (employee.Education > 0)
                {
                    employeeobj.Education = employee.Education;
                }

                if (employee.EducationField != null)
                {
                    var EducationField = await _context.EducationField
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.EducationField1 == employee.EducationField);

                    if (EducationField != null)
                    {
                        employeeobj.EducationField = EducationField.EducationFieldId;
                    }
                    else
                    {
                        return BadRequest(new { message = "Education Field Value needs to match the table's value or you need to add a new Education Field" });
                    }
                }

                if (employee.Gender != null)
                {
                    var Gender = await _context.Gender
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Gender1 == employee.Gender);

                    if (Gender != null)
                    {
                        employeeobj.EducationField = Gender.GenderId;
                    }
                    else
                    {
                        return BadRequest(new { message = "That Gender Value needs to match the table's value or you need to add a new Gender" });
                    }
                }

                if (employee.HourlyRate > 0 && !isAdmin)
                {
                    return Forbid();
                }
                else if (employee.HourlyRate > 0 && isAdmin)
                {
                    employeeobj.HourlyRate = employee.HourlyRate;
                }

                if (employee.JobInvolvement > 0)
                {
                    employeeobj.JobInvolvement = employee.JobInvolvement;
                }

                if (employee.JobLevel > 0)
                {
                    employeeobj.JobLevel = employee.JobLevel;
                }

                if (employee.MaritalStatus != null)
                {
                    var MaritalStatus = await _context.MaritalStatus
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.MaritalStatus1 == employee.MaritalStatus);

                    if (MaritalStatus != null)
                    {
                        employeeobj.MaritalStatus = MaritalStatus.MaritalStatusId;
                    }
                    else
                    {
                        return BadRequest(new { message = "That Marital Status needs to match the table's value or you need to add a new Marital Status" });
                    }
                }

                if (employee.MonthlyIncome > 0 && !isAdmin)
                {
                    return Forbid();
                }
                else if (employee.MonthlyIncome > 0 && isAdmin)
                {
                    employeeobj.MonthlyIncome = employee.MonthlyIncome;
                }

                if (employee.MonthlyRate > 0 && !isAdmin)
                {
                    return Forbid();
                }
                else if (employee.MonthlyRate > 0 && isAdmin)
                {
                    employeeobj.MonthlyRate = employee.MonthlyRate;
                }

                if (employee.NumCompaniesWorked > 0)
                {
                    employeeobj.NumCompaniesWorked = employee.NumCompaniesWorked;
                }

                if (employee.OverTime != null)
                {
                    employeeobj.OverTime = employee.OverTime;
                }

                if (employee.PercentSalaryHike > 0 && !isAdmin)
                {
                    return Forbid();
                }
                else if (employee.PercentSalaryHike > 0 && isAdmin)
                {
                    employeeobj.PercentSalaryHike = employee.PercentSalaryHike;
                }


                if (employee.PerformanceRating > 0 && !isAdmin)
                {
                    return Forbid();
                }
                else if (employee.PerformanceRating > 0 && isAdmin)
                {
                    employeeobj.PerformanceRating = employee.PerformanceRating;
                }

                if (employee.StandardHours > 0)
                {
                    employeeobj.StandardHours = employee.StandardHours;
                }

                if (employee.StockOptionLevel > 0)
                {
                    employeeobj.StockOptionLevel = employee.StockOptionLevel;
                }

                if (employee.TotalWorkingYears > 0)
                {
                    employeeobj.TotalWorkingYears = employee.TotalWorkingYears;
                }

                if (employee.TrainingTimesLastYear > 0)
                {
                    employeeobj.TrainingTimesLastYear = employee.TrainingTimesLastYear;
                }

                if (employee.YearsAtCompany > 0)
                {
                    employeeobj.YearsAtCompany = employee.YearsAtCompany;
                }

                if (employee.YearsInCurrentRole > 0)
                {
                    employeeobj.YearsInCurrentRole = employee.YearsInCurrentRole;
                }

                if (employee.YearsSinceLastPromotion > 0)
                {
                    employeeobj.YearsSinceLastPromotion = employee.YearsSinceLastPromotion;
                }

                if (employee.YearsWithCurrManager > 0)
                {
                    employeeobj.YearsWithCurrManager = employee.YearsWithCurrManager;
                }

                employeeobj.EmployeeSurvey = 101;

                _context.Employee.Add(employeeobj);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (EmployeeExists(employee.EmployeeNumber))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

            

            int surveycount = 0;

                var getemployeenumber = await _context.Employee
                    .AsNoTracking()
                    .Where(x => x.EmployeeNumber == employeeobj.EmployeeNumber)
                    .Select(a => new { EmployeeNumber = a.EmployeeNumber })
                    .FirstOrDefaultAsync();


                if (employee.EnvironmentSatisfaction > 0)
            {
                employeessurveyobj.EnvironmentSatisfaction = employee.EnvironmentSatisfaction;
                surveycount++;
            }
            if (employee.RelationshipSatisfaction > 0)
            {
                employeessurveyobj.RelationshipSatisfaction = employee.RelationshipSatisfaction;
                surveycount++;
            }
            if (employee.JobSatisfaction > 0)
            {
                employeessurveyobj.JobSatisfaction = employee.JobSatisfaction;
                surveycount++;
            }
            if (employee.WorkLifeBalance > 0)
            {
                employeessurveyobj.WorkLifeBalance = employee.WorkLifeBalance;
                surveycount++;
            }
            if (surveycount == 4) //if all values added then add new survey
            {

                employeessurveyobj.EmployeeId = getemployeenumber.EmployeeNumber;
                _context.EmployeeSurvey.Add(employeessurveyobj);

            }
            else if (surveycount > 0 && surveycount != 4) //if some values added, I assume you're fixing a survey mistake.
            {
                var employeessurveyobjoriginal = await _context.EmployeeSurvey
                    .OrderByDescending(x => x.EmployeeId == getemployeenumber.EmployeeNumber)
                    .FirstOrDefaultAsync();

                employeessurveyobjoriginal.EnvironmentSatisfaction = employeessurveyobj.EnvironmentSatisfaction > 0 ? default(byte) : employeessurveyobj.EnvironmentSatisfaction;
                employeessurveyobjoriginal.EnvironmentSatisfaction = employeessurveyobj.RelationshipSatisfaction > 0 ? default(byte) : employeessurveyobj.RelationshipSatisfaction;
                employeessurveyobjoriginal.EnvironmentSatisfaction = employeessurveyobj.JobSatisfaction > 0 ? default(byte) : employeessurveyobj.JobSatisfaction;
                employeessurveyobjoriginal.EnvironmentSatisfaction = employeessurveyobj.WorkLifeBalance > 0 ? default(byte) : employeessurveyobj.WorkLifeBalance;

                _context.Entry(employeessurveyobjoriginal).State = EntityState.Modified;
            }


                var latestemployeesurvey = await _context.EmployeeSurvey
                        .AsNoTracking()
                        .OrderByDescending(x => x.EmployeeId == getemployeenumber.EmployeeNumber)
                        .FirstOrDefaultAsync();

                    employeeobj.EmployeeSurvey = latestemployeesurvey.SurveyId;

                _context.Entry(employeeobj).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    throw;
                }
             var createdEmployeeObj = await _context.Employee
                .AsNoTracking()
                .Where(x => x.EmployeeNumber == employeeobj.EmployeeNumber)
                .Select(a => new { EmployeeNumber = a.EmployeeNumber })
                .FirstOrDefaultAsync();

                return Ok(new { message = "Created Employee: " + createdEmployeeObj.EmployeeNumber + " with provided values.\n" + employee });



            }else
            {
                return BadRequest(new { message = "You are not an admin only admin's can create employees"});
            }

        }


        /// <summary>
        /// Deletes an Employee if user is Admin
        /// </summary>
        /// <returns>Employee Details with varying detail</returns>
        /// <response code="200">Returns if successful</response>  
        /// <response code="400">Token Provider User Doesn't exist any more</response>  
        /// <response code="401">If the User is Unauthorized</response>  
        /// <response code="403">If a user attempts to delete</response>  
        /// <response code="404">If Username Not found</response>  
        // DELETE: api/Employees/5

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var employeesurvey = await _context.EmployeeSurvey
                .Where(x => x.EmployeeId == id)
                .ToListAsync();
            if (employeesurvey == null)
            {
                return NotFound();
            }


            _context.EmployeeSurvey.RemoveRange(employeesurvey);
            await _context.SaveChangesAsync();

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Delete Employee & Surveys.\n" + employee });
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeNumber == id);
        }
    }
}
