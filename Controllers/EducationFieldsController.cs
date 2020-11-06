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
    public class EducationFieldsController : ControllerBase
    {
        private readonly DimensionDataAPIContext _context;

        public EducationFieldsController(DimensionDataAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of Education Field in Table.
        /// </summary>
        /// <returns>return list of Education Field items</returns>
        /// <response code="200">return list of Education Field items</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/EducationFields
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationField>>> GetEducationField()
        {
            return await _context.EducationField.ToListAsync();
        }



        /// <summary>
        /// Gets a single EducationField item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <returns>return a EducationField entry matching the table</returns>
        /// <response code="200">return a EducationField entry matching the table</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/EducationFields/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationField>> GetEducationField(byte id)
        {
            var educationField = await _context.EducationField.FindAsync(id);

            if (educationField == null)
            {
                return NotFound();
            }

            return educationField;
        }


        /// <summary>
        /// Updates a single education Field item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <param name="educationField"></param>
        /// <returns>return an Update of a businessTravel item</returns>
        /// <response code="200">return an Update of a education Field item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // PUT: api/EducationFields/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducationField(byte id, EducationField educationField)
        {
            if (id != educationField.EducationFieldId)
            {
                return BadRequest();
            }

            _context.Entry(educationField).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationFieldExists(id))
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
        /// Creates a single new education Field item.
        /// </summary>
        /// <param name="educationField"></param>
        /// <returns>return an Update of a education Field item</returns>
        /// <response code="200">return an Update of a education Field item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // POST: api/EducationFields
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<EducationField>> PostEducationField(EducationField educationField)
        {
            _context.EducationField.Add(educationField);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EducationFieldExists(educationField.EducationFieldId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEducationField", new { id = educationField.EducationFieldId }, educationField);
        }

        /// <summary>
        /// Deletes a Education Field item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return an Update of a Education Field item</returns>
        /// <response code="200">return an Update of a Education Field item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="404">Throws not found if id doesn't exist</response>  
        // DELETE: api/EducationFields/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<EducationField>> DeleteEducationField(byte id)
        {
            var educationField = await _context.EducationField.FindAsync(id);
            if (educationField == null)
            {
                return NotFound();
            }

            _context.EducationField.Remove(educationField);
            await _context.SaveChangesAsync();

            return educationField;
        }

        private bool EducationFieldExists(byte id)
        {
            return _context.EducationField.Any(e => e.EducationFieldId == id);
        }
    }
}
