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
    public class GendersController : ControllerBase
    {
        private readonly DimensionDataAPIContext _context;

        public GendersController(DimensionDataAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of Genders in Table.
        /// </summary>
        /// <returns>return list of Gender items</returns>
        /// <response code="200">return list of Gender items</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/Genders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGender()
        {
            return await _context.Gender.ToListAsync();
        }
        /// <summary>
        /// Gets a single Gender item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <returns>return a Gender entry matching the table</returns>
        /// <response code="200">return a Gender entry matching the table</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/Genders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gender>> GetGender(byte id)
        {
            var gender = await _context.Gender.FindAsync(id);

            if (gender == null)
            {
                return NotFound();
            }

            return gender;
        }

        /// <summary>
        /// Updates a single Gender item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <param name="gender"></param>
        /// <returns>return an Update of a gender item</returns>
        /// <response code="200">return an Update of a gender item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // GET: api/Genders/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGender(byte id, Gender gender)
        {
            if (id != gender.GenderId)
            {
                return BadRequest();
            }

            _context.Entry(gender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(id))
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
        /// Creates a single new Gender item.
        /// </summary>
        /// <param name="gender"></param>
        /// <returns>return an Update of a gender item</returns>
        /// <response code="200">return an Update of a gender item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Gender>> PostGender(Gender gender)
        {
            _context.Gender.Add(gender);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GenderExists(gender.GenderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGender", new { id = gender.GenderId }, gender);
        }

        /// <summary>
        /// Deletes a Gender item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return an Update of a gender item</returns>
        /// <response code="200">return an Update of a gender item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="404">Throws not found if id doesn't exist</response>  
        // DELETE: api/Genders/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gender>> DeleteGender(byte id)
        {
            var gender = await _context.Gender.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }

            _context.Gender.Remove(gender);
            await _context.SaveChangesAsync();

            return gender;
        }

        private bool GenderExists(byte id)
        {
            return _context.Gender.Any(e => e.GenderId == id);
        }
    }
}
