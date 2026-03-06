using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session4_API_2.Models;

namespace Session4_API_2.Controller
{
    [Route("api/")]
    [ApiController]
    public class ProfileManagement : ControllerBase
    {
        private readonly Session4BellCroissantContext _context;

        public ProfileManagement(Session4BellCroissantContext context)
        {
            _context = context;
        }

        // GET /api/profile
        [HttpGet("profile")]
        [ProducesResponseType(typeof(UserProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProfile()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return Unauthorized("Must be logged in to continue.");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return Unauthorized();
            }

            var preference = await _context.UserPreferences
                .FirstOrDefaultAsync(p => p.UserId == user.UserId);

            return Ok(new UserProfileResponse
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = user.ProfilePicture,
                MailingListSubscribed = preference?.MailingListSub ?? false,
                PreferredDeliveryAddressId = preference?.PreferredDeliveryAddressId
            });
        }

        // PUT /api/profile
        [HttpPut("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditProfile(EditProfileRequest req)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrWhiteSpace(userEmail)) return Unauthorized();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null) return NotFound();

            user.FirstName = req.FirstName;
            user.LastName = req.LastName;
            user.ProfilePicture = req.ProfilePicture;

            // Update or create preferences
            var preference = await _context.UserPreferences
                .FirstOrDefaultAsync(p => p.UserId == user.UserId);

            // If a preferred delivery address was provided, validate it belongs to the user
            if (req.PreferredDeliveryAddressId.HasValue)
            {
                var address = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.AddressId == req.PreferredDeliveryAddressId.Value);

                if (address == null)
                {
                    return NotFound("Preferred delivery address not found.");
                }

                if (address.UserId != user.UserId)
                {
                    return Forbid();
                }
            }

            if (preference == null)
            {
                preference = new Models.UserPreference
                {
                    UserId = user.UserId,
                    MailingListSub = req.MailingListSubscribed ?? false,
                    PreferredDeliveryAddressId = req.PreferredDeliveryAddressId
                };

                _context.UserPreferences.Add(preference);
            }
            else
            {
                if (req.MailingListSubscribed.HasValue)
                    preference.MailingListSub = req.MailingListSubscribed.Value;

                if (req.PreferredDeliveryAddressId.HasValue)
                    preference.PreferredDeliveryAddressId = req.PreferredDeliveryAddressId;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET /api/addresses
        [HttpGet("addresses")]
        [ProducesResponseType(typeof(List<AddressResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAddresses()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrWhiteSpace(userEmail)) return Unauthorized();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null) return NotFound();

            var preference = await _context.UserPreferences
                .FirstOrDefaultAsync(p => p.UserId == user.UserId);

            var addressEntities = await _context.Addresses
                .Where(a => a.UserId == user.UserId)
                .ToListAsync();

            var addresses = addressEntities
                .Select(a => new AddressResponseDto
                {
                    AddressId = a.AddressId,
                    StreetAddress = a.StreetAddress,
                    City = a.City,
                    PostalCode = a.PostalCode,
                    Country = a.Country,
                    UserId = a.UserId,
                    IsPreferred = preference?.PreferredDeliveryAddressId == a.AddressId
                })
                .ToList();

            return Ok(addresses);
        }

        // POST /api/addresses
        [HttpPost("addresses")]
        public async Task<IActionResult> AddAddress(AddressPayload req)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrWhiteSpace(userEmail)) return Unauthorized();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return Unauthorized();

            // 1. Save the Address to the Address Table
            Address newAddress = new Address
            {
                StreetAddress = req.StreetAddress,
                City = req.City,
                PostalCode = req.PostalCode,
                Country = req.Country,
                UserId = user.UserId
            };

            _context.Addresses.Add(newAddress);
            await _context.SaveChangesAsync(); // We MUST save here to generate the AddressId

            // 2. Update the UserPreferences Table (where the actual "Preferred" column lives)
            if (req.SetAsPreferred == true)
            {
                var preference = await _context.UserPreferences
                    .FirstOrDefaultAsync(p => p.UserId == user.UserId);

                if (preference == null)
                {
                    // Create new preference record if it doesn't exist
                    _context.UserPreferences.Add(new Models.UserPreference
                    {
                        UserId = user.UserId,
                        MailingListSub = false,
                        PreferredDeliveryAddressId = newAddress.AddressId // Point to the new ID
                    });
                }
                else
                {
                    // Update the existing FK to the new AddressId
                    preference.PreferredDeliveryAddressId = newAddress.AddressId;
                }

                await _context.SaveChangesAsync();
            }

            return Ok(new AddressResponseDto
            {
                AddressId = newAddress.AddressId,
                StreetAddress = newAddress.StreetAddress,
                City = newAddress.City,
                PostalCode = newAddress.PostalCode,
                Country = newAddress.Country,
                UserId = newAddress.UserId,
                IsPreferred = req.SetAsPreferred ?? false
            });
        }

        // PUT /api/addresses/{id}
        [HttpPut("addresses/{id}")]
        [ProducesResponseType(typeof(AddressResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditAddress(int id, AddressPayload req)
        {
            Console.WriteLine("RECEIVED ADDRESS EDIT REQUEST");
            Console.WriteLine($"Set as preferred: {req.SetAsPreferred}");
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrWhiteSpace(userEmail)) return Unauthorized();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return Unauthorized();

            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.AddressId == id);

            if (address == null) return NotFound();

            if (address.UserId != user.UserId) return Forbid();

            address.StreetAddress = req.StreetAddress;
            address.City = req.City;
            address.PostalCode = req.PostalCode;
            address.Country = req.Country;

            // Handle preference update if requested
            var preference = await _context.UserPreferences
                .FirstOrDefaultAsync(p => p.UserId == user.UserId);

            if (req.SetAsPreferred.HasValue)
            {
                if (req.SetAsPreferred.Value)
                {
                    if (preference == null)
                    {
                        preference = new Models.UserPreference
                        {
                            UserId = user.UserId,
                            MailingListSub = false,
                            PreferredDeliveryAddressId = address.AddressId
                        };

                        _context.UserPreferences.Add(preference);
                    }
                    else
                    {
                        preference.PreferredDeliveryAddressId = address.AddressId;
                    }
                }
                else
                {
                    if (preference != null && preference.PreferredDeliveryAddressId == address.AddressId)
                    {
                        preference.PreferredDeliveryAddressId = null;
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new AddressResponseDto
            {
                AddressId = address.AddressId,
                StreetAddress = address.StreetAddress,
                City = address.City,
                PostalCode = address.PostalCode,
                Country = address.Country,
                UserId = address.UserId,
                IsPreferred = (preference?.PreferredDeliveryAddressId == address.AddressId)
            });
        }

        // DELETE /api/addresses/{id}
        [HttpDelete("addresses/{id}")] // Fixed typo: 'addressses' -> 'addresses'
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrWhiteSpace(userEmail)) return Unauthorized();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return Unauthorized();

            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.AddressId == id);

            if (address == null) return NotFound();

            if (address.UserId != user.UserId) return Forbid();

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // --- REQUEST DTOS ---
        public class EditProfileRequest
        {
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string? ProfilePicture { get; set; }
            public bool? MailingListSubscribed { get; set; }
            public int? PreferredDeliveryAddressId { get; set; }
        }

        public class AddressPayload
        {
            public string StreetAddress { get; set; } = null!;
            public string City { get; set; } = null!;
            public string PostalCode { get; set; } = null!;
            public string Country { get; set; } = null!;
            public bool? SetAsPreferred { get; set; }
        }

        // --- RESPONSE DTOS (Added for Swagger/Scalar documentation) ---
        public class UserProfileResponse
        {
            public string Email { get; set; } = null!;
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string? ProfilePicture { get; set; }
            public bool MailingListSubscribed { get; set; }
            public int? PreferredDeliveryAddressId { get; set; }
        }

        public class AddressResponseDto
        {
            public int AddressId { get; set; }
            public string StreetAddress { get; set; } = null!;
            public string City { get; set; } = null!;
            public string PostalCode { get; set; } = null!;
            public string Country { get; set; } = null!;
            public int UserId { get; set; }
            public bool IsPreferred { get; set; }
        }
    }
}