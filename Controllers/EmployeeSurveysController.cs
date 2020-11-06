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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeSurveysController : ControllerBase
    {
        private readonly DimensionDataAPIContext _context;

        public EmployeeSurveysController(DimensionDataAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of Employee Survey in Table.
        /// </summary>
        /// <returns>return list of Employee Survey items</returns>
        /// <response code="200">return list of Employee Survey items</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/EmployeeSurveys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeSurvey>>> GetEmployeeSurvey()
        {
            return await _context.EmployeeSurvey.ToListAsync();
        }

        /// <summary>
        /// Gets a single Employee Survey item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <returns>return a Employee Survey entry matching the table</returns>
        /// <response code="200">return a Employee Survey entry matching the table</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/EmployeeSurveys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeSurvey>> GetEmployeeSurvey(int id)
        {
            var employeeSurvey = await _context.EmployeeSurvey.FindAsync(id);

            if (employeeSurvey == null)
            {
                return NotFound();
            }

            return employeeSurvey;
        }

        /// <summary>
        /// Updates a single employee Survey item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <param name="employeeSurvey"></param>
        /// <returns>return an Update of a employeeSurvey item</returns>
        /// <response code="200">return an Update of a employee Survey item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // PUT: api/EmployeeSurveys/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeSurvey(int id, EmployeeSurvey employeeSurvey)
        {
            if (id != employeeSurvey.SurveyId)
            {
                return BadRequest();
            }

            _context.Entry(employeeSurvey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeSurveyExists(id))
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

        /// <summary>
        /// Creates a single new Employee Survey item.
        /// </summary>
        /// <param name="employeeSurvey"></param>
        /// <returns>return an Update of a Employee Survey item</returns>
        /// <response code="200">return an Update of a Employee Survey item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // POST: api/Departments
        // POST: api/EmployeeSurveys
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<EmployeeSurvey>> PostEmployeeSurvey(EmployeeSurvey employeeSurvey)
        {
            _context.EmployeeSurvey.Add(employeeSurvey);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeSurvey", new { id = employeeSurvey.SurveyId }, employeeSurvey);
        }


        /// <summary>
        /// Deletes a Employee Survey item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return an Update of a Employee Survey item</returns>
        /// <response code="200">return an Update of a Employee Survey item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="404">Throws not found if id doesn't exist</response>  
        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<EmployeeSurvey>> DeleteEmployeeSurvey(int id)
        {
            var employeeSurvey = await _context.EmployeeSurvey.FindAsync(id);
            if (employeeSurvey == null)
            {
                return NotFound();
            }

            _context.EmployeeSurvey.Remove(employeeSurvey);
            await _context.SaveChangesAsync();

            return employeeSurvey;
        }

        private bool EmployeeSurveyExists(int id)
        {
            return _context.EmployeeSurvey.Any(e => e.SurveyId == id);
        }
    }
}
