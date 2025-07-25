using Backend_Online_3.Irepository;
using Backend_Online_3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Online_3.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
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
                return BadRequest("Người dùng không được để trống");
            }
            var createdUser = await _context.CreateUser(user);
            if (createdUser == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi tạo người dùng");
            }
            return CreatedAtAction(nameof(GetAsync), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("ID người dùng không khớp");
            }
            var updated = await _context.UpdateUser(id, user);
            if (!updated)
            {
                return NotFound($"Không tìm thấy người dùng với ID: {id}");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _context.DeleteUser(id);
            if (!deleted)
            {
                return NotFound($"Không tìm thấy người dùng với ID: {id}");
            }
            return NoContent();
        }
    }
}
