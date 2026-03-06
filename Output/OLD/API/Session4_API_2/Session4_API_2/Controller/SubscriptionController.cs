using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session4_API_2.Models;

namespace Session4_API_2.Controller
{
    [Route("api/")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly Session4BellCroissantContext _context;

        public SubscriptionController(Session4BellCroissantContext context)
        {
            _context = context;
        }

        [HttpPost("subscribe")]
        [ProducesResponseType(typeof(SubscriptionStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SubscribeToMailingList()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrWhiteSpace(userEmail)) return Unauthorized();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null) return Unauthorized();

            var sub = await _context.UserPreferences
                .FirstOrDefaultAsync(p => p.UserId == user.UserId);

            // Edge Case Fix: If they don't have a record yet, create one!
            if (sub == null)
            {
                sub = new UserPreference { UserId = user.UserId, MailingListSub = true };
                _context.UserPreferences.Add(sub);
            }
            else
            {
                sub.MailingListSub = true;
            }

            await _context.SaveChangesAsync();

            return Ok(new SubscriptionStatusResponse
            {
                Email = user.Email,
                IsSubscribed = true
            });
        }

        [HttpPost("unsubscribe")] // Fixed typo in method name
        [ProducesResponseType(typeof(SubscriptionStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UnsubscribeFromMailingList()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrWhiteSpace(userEmail)) return Unauthorized();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null) return Unauthorized();

            var sub = await _context.UserPreferences
                .FirstOrDefaultAsync(p => p.UserId == user.UserId);

            // Edge Case Fix: If they don't have a record yet, create one!
            if (sub == null)
            {
                sub = new UserPreference { UserId = user.UserId, MailingListSub = false };
                _context.UserPreferences.Add(sub);
            }
            else
            {
                sub.MailingListSub = false;
            }

            await _context.SaveChangesAsync();

            return Ok(new SubscriptionStatusResponse
            {
                Email = user.Email,
                IsSubscribed = false
            });
        }

        // --- RESPONSE DTOS (Added for Swagger/Scalar documentation) ---
        public class SubscriptionStatusResponse
        {
            public string Email { get; set; } = null!;
            public bool IsSubscribed { get; set; }
        }
    }
}