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
    public class QuantityBasedRuleDetailsController : ControllerBase
    {
        private readonly BelleCroissantLyonnaisContext _context;

        public QuantityBasedRuleDetailsController(BelleCroissantLyonnaisContext context)
        {
            _context = context;
        }

        // GET: api/QuantityBasedRuleDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuantityBasedRuleDetail>>> GetQuantityBasedRuleDetails()
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }
            return await _context.QuantityBasedRuleDetails.ToListAsync();
        }

        // GET: api/QuantityBasedRuleDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuantityBasedRuleDetail>> GetQuantityBasedRuleDetail(int id)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }
            var quantityBasedRuleDetail = await _context.QuantityBasedRuleDetails.FindAsync(id);

            if (quantityBasedRuleDetail == null)
            {
                return NotFound();
            }

            return quantityBasedRuleDetail;
        }

        // PUT: api/QuantityBasedRuleDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuantityBasedRuleDetail(int id, QuantityBasedRuleDetail quantityBasedRuleDetail)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }
            if (id != quantityBasedRuleDetail.RuleId)
            {
                return BadRequest();
            }

            _context.Entry(quantityBasedRuleDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuantityBasedRuleDetailExists(id))
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

        // POST: api/QuantityBasedRuleDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuantityBasedRuleDetail>> PostQuantityBasedRuleDetail(QuantityBasedRuleDetail quantityBasedRuleDetail)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }
            _context.QuantityBasedRuleDetails.Add(quantityBasedRuleDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuantityBasedRuleDetail", new { id = quantityBasedRuleDetail.RuleId }, quantityBasedRuleDetail);
        }

        // DELETE: api/QuantityBasedRuleDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuantityBasedRuleDetail(int id)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }
            var quantityBasedRuleDetail = await _context.QuantityBasedRuleDetails.FindAsync(id);
            if (quantityBasedRuleDetail == null)
            {
                return NotFound();
            }

            _context.QuantityBasedRuleDetails.Remove(quantityBasedRuleDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuantityBasedRuleDetailExists(int id)
        {
            return _context.QuantityBasedRuleDetails.Any(e => e.RuleId == id);
        }
    }
}
