using GlobalNewsAPI.Models;
using GlobalNewsAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GlobalNewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {

        private readonly RepositoryContext _context;

        public UsersController(RepositoryContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _context.Users.ToList();
                return Ok(users);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult GetOneUser([FromRoute(Name = "id")] int id)
        {
            try
            {
                var users = _context.Users.SingleOrDefault(n => n.UserId == id);

                if (users is null)
                {
                    return NotFound(); // 404
                }

                return Ok(users); // Return the retrieved user item

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneUser(int id, [FromBody] Users updatedUser)
        {
            try
            {
                // Retrieve the existing user from the database
                var existingUser = _context.Users.SingleOrDefault(u => u.UserId == id);

                if (existingUser == null)
                {
                    return NotFound(); // User not found
                }

                // Update only the allowed properties
                existingUser.Username = updatedUser.Username;
                existingUser.UserEmail = updatedUser.UserEmail;
                existingUser.Role = updatedUser.Role;

                // Update password if provided
                if (!string.IsNullOrEmpty(updatedUser.Password))
                {
                    existingUser.Password = updatedUser.Password; // Update password
                                                                  
                }

                // Save changes to the database
                _context.SaveChanges();

                return Ok(existingUser); // Return the updated user

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating user: {ex.Message}");
            }
        }



    }
}
