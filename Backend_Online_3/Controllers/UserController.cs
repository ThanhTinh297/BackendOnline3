using Backend_Online_3.Irepository;
using Backend_Online_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Online_3.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _context;
        public UserController(IUserService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _context.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null");
            }
            var createdUser = await _context.CreateUser(user);
            if (createdUser == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating user");
            }
            return CreatedAtAction(nameof(GetAsync), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("User ID mismatch");
            }
            var updated = await _context.UpdateUser(id, user);
            if (!updated)
            {
                return NotFound($"User with ID {id} not found");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _context.DeleteUser(id);
            if (!deleted)
            {
                return NotFound($"User with ID {id} not found");
            }
            return NoContent();
        }
    }
}
