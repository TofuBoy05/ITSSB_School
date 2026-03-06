using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session4_API_2.Models;

namespace Session4_API_2.Controller
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Session4BellCroissantContext _context;

        public AuthController(Session4BellCroissantContext context)
        {
            _context = context;
        }

        // POST: /api/register
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register(RegistrationDto dto)
        {
            var newUser = new User
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ProfilePicture = dto.ProfilePicture,
            };

            var hasher = new PasswordHasher<AuthCredential>();

            var credentials = new AuthCredential
            {
                SecurityQuestion = dto.SecurityQuestion,
                User = newUser,
            };

            credentials.Password = hasher.HashPassword(credentials, dto.Password);
            credentials.SecurityAnswer = hasher.HashPassword(credentials, dto.SecurityAnswer);

            _context.Users.Add(newUser);
            _context.AuthCredentials.Add(credentials);
            await _context.SaveChangesAsync();

            // get user id
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return NotFound();

            var newUserPreference = new UserPreference();

            newUserPreference.MailingListSub = dto.MailingListSub;
            newUserPreference.UserId = user.UserId;

            _context.UserPreferences.Add(newUserPreference);
            await _context.SaveChangesAsync();

            return Ok(new RegisterResponse
            {
                Message = "Registration successful!",
                User = new UserDto
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            });
        }

        // POST /api/login
        [HttpPost("login")]
        [ProducesResponseType(typeof(SimpleMessageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var user = await _context.Users
                .Include(u => u.AuthCredential)
                .FirstOrDefaultAsync(u => u.Email == login.Email);

            if (user == null) return Unauthorized("Invalid email or password");

            var hasher = new PasswordHasher<AuthCredential>();

            var result = hasher.VerifyHashedPassword(
                user.AuthCredential,
                user.AuthCredential.Password,
                login.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return NotFound("User not found.");
            }

            HttpContext.Session.SetString("UserEmail", user.Email);

            // Fixed a tiny typo here: "Wrlcome" -> "Welcome"
            return Ok(new SimpleMessageResponse { Message = "Welcome back, " + user.FirstName });
        }

        // POST /api/forgot-password
        [HttpPost("forgot-password")]
        [ProducesResponseType(typeof(ForgotPasswordResponseObj), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPW)
        {
            var user = await _context.Users
                .Include(u => u.AuthCredential)
                .FirstOrDefaultAsync(u => u.Email == forgotPW.Email);

            if (user == null || user.AuthCredential == null)
            {
                return Unauthorized("User does not exist.");
            }

            // return the security question:
            return Ok(new ForgotPasswordResponseObj { Question = user.AuthCredential.SecurityQuestion });
        }

        // POST /api/reset-password
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(SimpleMessageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ResetPassword(PasswordResetRequest reset)
        {
            var user = await _context.Users
                .Include(u => u.AuthCredential)
                .FirstOrDefaultAsync(u => u.Email == reset.Email);

            var hasher = new PasswordHasher<AuthCredential>();

            var res = hasher.VerifyHashedPassword(user.AuthCredential, user.AuthCredential.SecurityAnswer, reset.SecurityAnswer);

            if (res == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Incorrect Security Answer");
            }

            // update password with new hash
            var hashedNewPassword = hasher.HashPassword(user.AuthCredential, reset.NewPassword);
            user.AuthCredential.Password = hashedNewPassword;

            await _context.SaveChangesAsync();

            return Ok(new SimpleMessageResponse { Message = "Password changed successfully." });
        }


        // --- REQUEST DTOS ---
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class ForgotPasswordRequest
        {
            public string Email { get; set; }
        }

        public class PasswordResetRequest
        {
            public string Email { get; set; }
            public string SecurityAnswer { get; set; }
            public string NewPassword { get; set; }
        }
    }

    public class RegistrationDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public string SecurityQuestion { get; set; } = null!;
        public string SecurityAnswer { get; set; } = null!;
        public bool MailingListSub { get; set; }
    }

    // --- RESPONSE DTOS (Added for Swagger/Scalar documentation) ---
    public class SimpleMessageResponse
    {
        public string Message { get; set; }
    }

    public class ForgotPasswordResponseObj
    {
        public string Question { get; set; }
    }

    public class RegisterResponse
    {
        public string Message { get; set; }
        public UserDto User { get; set; }
    }

    public class UserDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}