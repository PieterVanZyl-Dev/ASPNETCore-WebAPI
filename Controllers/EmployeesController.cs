using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    //[Authorize]
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

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetEmployee()
        {
            var employee = await _context.Employee
            .Include("GenderNavigation")
            .Include("BusinessTravelNavigation")
            .Include("DepartmentNavigation")
            .Include("EducationFieldNavigation")
            .Include("JobRoleNavigation")
            .Include("MaritalStatusNavigation")
            .Include("EmployeeSurveyNavigation")
            .Select(a => new EmployeeResponse
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
                WorkLifeBalance = a.EmployeeSurveyNavigation.WorkLifeBalance})
                .ToListAsync();

            return employee;
                 
                }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployee(int id)
        {


            var employee = await _context.Employee
                .Include("GenderNavigation")
                .Include("BusinessTravelNavigation")
                .Include("DepartmentNavigation")
                .Include("EducationFieldNavigation")
                .Include("JobRoleNavigation")
                .Include("MaritalStatusNavigation")
                .Include("EmployeeSurveyNavigation")
                .Where(emp => emp.EmployeeNumber == id)
                .Select(a => new EmployeeResponse
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
                }).
                FirstOrDefaultAsync();



            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }






        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeNumber)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

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

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
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

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeNumber }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeNumber == id);
        }
    }
}
