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
    public class MaritalStatusController : ControllerBase
    {
        private readonly DimensionDataAPIContext _context;

        public MaritalStatusController(DimensionDataAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of Marital Status in Table.
        /// </summary>
        /// <returns>return list of MaritalStatus items</returns>
        /// <response code="200">return list of Marital Status items</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/MaritalStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaritalStatus>>> GetMaritalStatus()
        {
            return await _context.MaritalStatus.ToListAsync();
        }

        /// <summary>
        /// Gets a single maritalStatus item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <returns>return a maritalStatus entry matching the table</returns>
        /// <response code="200">return a maritalStatus entry matching the table</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/MaritalStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaritalStatus>> GetMaritalStatus(byte id)
        {
            var maritalStatus = await _context.MaritalStatus.FindAsync(id);

            if (maritalStatus == null)
            {
                return NotFound();
            }

            return maritalStatus;
        }

        /// <summary>
        /// Updates a single maritalStatus item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <param name="maritalStatus"></param>
        /// <returns>return an Update of a maritalStatus item</returns>
        /// <response code="200">return an Update of a maritalStatus item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // PUT: api/MaritalStatus/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutMaritalStatus(byte id, MaritalStatus maritalStatus)
        {
            if (id != maritalStatus.MaritalStatusId)
            {
                return BadRequest();
            }

            _context.Entry(maritalStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaritalStatusExists(id))
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
        /// Creates a single new maritalStatus item.
        /// </summary>
        /// <param name="maritalStatus"></param>
        /// <returns>return an Update of a maritalStatus item</returns>
        /// <response code="200">return an Update of a maritalStatus item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // POST: api/MaritalStatus
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<MaritalStatus>> PostMaritalStatus(MaritalStatus maritalStatus)
        {
            _context.MaritalStatus.Add(maritalStatus);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MaritalStatusExists(maritalStatus.MaritalStatusId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMaritalStatus", new { id = maritalStatus.MaritalStatusId }, maritalStatus);
        }

        /// <summary>
        /// Deletes a maritalStatus item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return an Update of a maritalStatus item</returns>
        /// <response code="200">return an Update of a maritalStatus item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="404">Throws not found if id doesn't exist</response>  
        // DELETE: api/MaritalStatus/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<MaritalStatus>> DeleteMaritalStatus(byte id)
        {
            var maritalStatus = await _context.MaritalStatus.FindAsync(id);
            if (maritalStatus == null)
            {
                return NotFound();
            }

            _context.MaritalStatus.Remove(maritalStatus);
            await _context.SaveChangesAsync();

            return maritalStatus;
        }

        private bool MaritalStatusExists(byte id)
        {
            return _context.MaritalStatus.Any(e => e.MaritalStatusId == id);
        }
    }
}
