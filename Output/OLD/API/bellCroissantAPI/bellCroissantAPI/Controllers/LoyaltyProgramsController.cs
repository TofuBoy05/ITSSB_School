using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bellCroissantAPI.Models;

namespace bellCroissantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyProgramsController : ControllerBase
    {
        private readonly BelleCroissantLyonnaisContext _context;

        public LoyaltyProgramsController(BelleCroissantLyonnaisContext context)
        {
            _context = context;
        }

        // GET: api/LoyaltyPrograms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoyaltyProgram>>> GetLoyaltyPrograms()
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }
            return await _context.LoyaltyPrograms.ToListAsync();
        }

        // GET: api/LoyaltyPrograms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoyaltyProgram>> GetLoyaltyProgram(int id)
        {

            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }

            var loyaltyProgram = await _context.LoyaltyPrograms.FindAsync(id);

            if (loyaltyProgram == null)
            {
                return NotFound();
            }

            return loyaltyProgram;
        }

        // PUT: api/LoyaltyPrograms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoyaltyProgram(int id, LoyaltyProgram loyaltyProgram)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }

            if (id != loyaltyProgram.CustomerId)
            {
                return BadRequest();
            }

            

            _context.Entry(loyaltyProgram).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoyaltyProgramExists(id))
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

        // POST: api/LoyaltyPrograms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoyaltyProgram>> PostLoyaltyProgram(LoyaltyProgram loyaltyProgram)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }
            _context.LoyaltyPrograms.Add(loyaltyProgram);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoyaltyProgramExists(loyaltyProgram.CustomerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLoyaltyProgram", new { id = loyaltyProgram.CustomerId }, loyaltyProgram);
        }

        // DELETE: api/LoyaltyPrograms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoyaltyProgram(int id)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }
            var loyaltyProgram = await _context.LoyaltyPrograms.FindAsync(id);
            if (loyaltyProgram == null)
            {
                return NotFound();
            }

            _context.LoyaltyPrograms.Remove(loyaltyProgram);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoyaltyProgramExists(int id)
        {
            return _context.LoyaltyPrograms.Any(e => e.CustomerId == id);
        }
    }
}
