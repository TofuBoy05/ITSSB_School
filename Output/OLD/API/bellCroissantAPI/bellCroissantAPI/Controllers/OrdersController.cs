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
    public class OrdersController : ControllerBase
    {
        private readonly BelleCroissantLyonnaisContext _context;

        public OrdersController(BelleCroissantLyonnaisContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }

            if (id != order.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteOrder(int id, OrderItem orderItem)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            order.Status = "Completed";
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id, OrderItem orderItem)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            order.Status = "Cancel";
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.TransactionId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            string auth = Request.Headers["Authorization"];
            if (auth != "Basic c3RhZmY6QkNMeW9uMjAyNA==")
            {
                return Unauthorized();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.TransactionId == id);
        }
    }
}
