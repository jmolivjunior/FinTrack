using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinTrack.API.Data;
using FinTrack.API.Models;
namespace FinTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]

        public async Task<ActionResult> Register(User user)
        {
            var exists = await _context.Users
                .AnyAsync(u => u.Email == user.Email);

            if (exists)
                return BadRequest("Email already exists");
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User registered sucessfully");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(User user)
        {
            var dbUser = await _context.Users
                 .FirstOrDefaultAsync(u => u.Email == user.Email);
            if (dbUser == null)
                return Unauthorized("Invalid email or password");

            bool validPassword = BCrypt.Net.BCrypt.Verify(user.PasswordHash, dbUser.PasswordHash);

            if (!validPassword)
                return Unauthorized("Invalid email or password");

            var token = GenerateToken(dbUser);
            return Ok(new { token });
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:key"]!));

            var Credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name)
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: Credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
