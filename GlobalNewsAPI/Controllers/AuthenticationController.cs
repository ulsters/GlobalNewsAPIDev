using GlobalNewsAPI.Models;
using GlobalNewsAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GlobalNewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public static Users user = new Users();
        private readonly IConfiguration _configuration;
        private readonly RepositoryContext _context;

        public AuthenticationController(IConfiguration configuration, RepositoryContext context)
        {
            _configuration = configuration;
            _context = context;
            
        }

        [HttpPost("register")]
        public ActionResult<Users> Register(UserDto request)
        {

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            string userRegisterDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

            var newUser = new Users
            {
                Username = request.Username,
                Password = request.Password,
                UserEmail = request.UserEmail,
                PasswordHash = passwordHash
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(newUser);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserDto request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong Password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
              );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
