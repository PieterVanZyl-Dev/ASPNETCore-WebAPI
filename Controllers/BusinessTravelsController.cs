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
    public class BusinessTravelsController : ControllerBase
    {
        private readonly DimensionDataAPIContext _context;

        public BusinessTravelsController(DimensionDataAPIContext context)
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
        public async Task<ActionResult<IEnumerable<BusinessTravelResponse>>> GetBusinessTravel()
        {
            return await _context.BusinessTravel.Select(a =>new BusinessTravelResponse { BusinessTravel1 = a.BusinessTravel1, BusinessTravelId = a.BusinessTravelId }).ToListAsync();
        }

        /// <summary>
        /// Gets a single businessTravel item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <returns>return a businessTravel entry matching the table</returns>
        /// <response code="200">return a businessTravel entry matching the table</response>
        /// <response code="403">Throws forbidden if user is not authenticated</response>  
        // GET: api/BusinessTravels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessTravelResponse>> GetBusinessTravel(byte id)
        {
            var businessTravel = await _context.BusinessTravel
                .Where(a => a.BusinessTravelId == id)
                .Select(a => new BusinessTravelResponse { BusinessTravel1 = a.BusinessTravel1, BusinessTravelId = a.BusinessTravelId })
                .FirstOrDefaultAsync();

            if (businessTravel == null)
            {
                return NotFound();
            }

            return businessTravel;
        }


        /// <summary>
        /// Updates a single businessTravel item.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <param name="businessTravel"></param>
        /// <returns>return an Update of a businessTravel item</returns>
        /// <response code="200">return an Update of a businessTravel item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // PUT: api/businessTravel/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessTravel(byte id, BusinessTravel businessTravel)
        {
            if (id != businessTravel.BusinessTravelId)
            {
                return BadRequest();
            }

            _context.Entry(businessTravel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessTravelExists(id))
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
        /// Creates a single new businessTravel item.
        /// </summary>
        /// <param name="businessTravel"></param>
        /// <returns>return an Update of a businessTravel item</returns>
        /// <response code="200">return an Update of a businessTravel item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="403">Throws bad request if id doesn't exist</response>  
        // POST: api/BusinessTravels
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<BusinessTravel>> PostBusinessTravel(BusinessTravel businessTravel)
        {
            _context.BusinessTravel.Add(businessTravel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BusinessTravelExists(businessTravel.BusinessTravelId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBusinessTravel", new { id = businessTravel.BusinessTravelId }, businessTravel);
        }

        /// <summary>
        /// Deletes a Business Travel item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return an Update of a BusinessTravel item</returns>
        /// <response code="200">return an Update of a BusinessTravel item</response>
        /// <response code="403">Throws forbidden if user is not authenticated or is not admin</response>
        /// <response code="404">Throws not found if id doesn't exist</response>  
        // DELETE: api/Departments/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BusinessTravel>> DeleteBusinessTravel(byte id)
        {
            var businessTravel = await _context.BusinessTravel.FindAsync(id);
            if (businessTravel == null)
            {
                return NotFound();
            }

            _context.BusinessTravel.Remove(businessTravel);
            await _context.SaveChangesAsync();

            return businessTravel;
        }

        private bool BusinessTravelExists(byte id)
        {
            return _context.BusinessTravel.Any(e => e.BusinessTravelId == id);
        }
    }
}
