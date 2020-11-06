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
    public class JobRolesController : ControllerBase
    {
        private readonly DimensionDataAPIContext _context;

        public JobRolesController(DimensionDataAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of Job Role in Table.
        /// </summary>
        /// <returns>return list of Job Role items</returns>
        /// <response code="200">return list of Job Role items</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/JobRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobRole>>> GetJobRole()
        {
            return await _context.JobRole.ToListAsync();
        }
        /// <summary>
        /// Gets a single job Role item.
        /// </summary>
        /// <param name="id" jobRole="1"></param>
        /// <returns>return a job Role entry matching the table</returns>
        /// <response code="200">return a job Role entry matching the table</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/JobRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobRole>> GetJobRole(byte id)
        {
            var jobRole = await _context.JobRole.FindAsync(id);

            if (jobRole == null)
            {
                return NotFound();
            }

            return jobRole;
        }



        /// <summary>
        /// Updates a single job Role item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <param name="jobRole"></param>
        /// <returns>return an Update of a job Role item</returns>
        /// <response code="200">return an Update of a job Role item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // PUT: api/JobRoles/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobRole(byte id, JobRole jobRole)
        {
            if (id != jobRole.JobRoleId)
            {
                return BadRequest();
            }

            _context.Entry(jobRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobRoleExists(id))
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
        /// Creates a single new jobRole item.
        /// </summary>
        /// <param name="jobRole"></param>
        /// <returns>return an Update of a jobRole item</returns>
        /// <response code="200">return an Update of a jobRole item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // POST: api/JobRoles
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<JobRole>> PostJobRole(JobRole jobRole)
        {
            _context.JobRole.Add(jobRole);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobRoleExists(jobRole.JobRoleId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJobRole", new { id = jobRole.JobRoleId }, jobRole);
        }

        /// <summary>
        /// Deletes a JobRole item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return an Update of a JobRole item</returns>
        /// <response code="200">return an Update of a JobRole item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="404">Throws not found if id doesn't exist</response>  
        // DELETE: api/JobRoles/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobRole>> DeleteJobRole(byte id)
        {
            var jobRole = await _context.JobRole.FindAsync(id);
            if (jobRole == null)
            {
                return NotFound();
            }

            _context.JobRole.Remove(jobRole);
            await _context.SaveChangesAsync();

            return jobRole;
        }

        private bool JobRoleExists(byte id)
        {
            return _context.JobRole.Any(e => e.JobRoleId == id);
        }
    }
}
