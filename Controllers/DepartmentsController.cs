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
    public class DepartmentsController : ControllerBase
    {
        private readonly DimensionDataAPIContext _context;

        public DepartmentsController(DimensionDataAPIContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets a list of Departments in Table.
        /// </summary>
        /// <returns>return list of Departments items</returns>
        /// <response code="200">return list of Departments items</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            return await _context.Department.ToListAsync();
        }
        /// <summary>
        /// Gets a single Department item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <returns>return a Department entry matching the table</returns>
        /// <response code="200">return a Department entry matching the table</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(byte id)
        {
            var department = await _context.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        /// <summary>
        /// Updates a single department item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <param name="department"></param>
        /// <returns>return an Update of a department item</returns>
        /// <response code="200">return an Update of a department item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // PUT: api/Departments/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(byte id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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
        /// Creates a single new department item.
        /// </summary>
        /// <param name="department"></param>
        /// <returns>return an Update of a department item</returns>
        /// <response code="200">return an Update of a department item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // POST: api/Departments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            _context.Department.Add(department);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DepartmentExists(department.DepartmentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
        }

        /// <summary>
        /// Deletes a department item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return an Update of a department item</returns>
        /// <response code="200">return an Update of a department item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="404">Throws not found if id doesn't exist</response>  
        // DELETE: api/Departments/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(byte id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();

            return department;
        }

        private bool DepartmentExists(byte id)
        {
            return _context.Department.Any(e => e.DepartmentId == id);
        }
    }
}
