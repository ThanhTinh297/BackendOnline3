using Backend_Online_3.Irepository;
using Backend_Online_3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Online_3.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _context;
        public RoleController(IRoleService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var roles = await _context.GetRoles();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest("Vai trò không được để trống");
            }

            var createdRole = await _context.CreateRole(role);
            if (createdRole == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi tạo vai trò");
            }

            return CreatedAtAction(nameof(GetAsync), new { id = createdRole.RoleId }, createdRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Role role)
        {
            if (id != role.RoleId)
            {
                return BadRequest("ID vai trò không khớp");
            }

            var updated = await _context.UpdateRole(id, role);
            if (!updated)
            {
                return NotFound($"Không tìm thấy vai trò với ID: {id}");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _context.DeleteRole(id);
            if (!deleted)
            {
                return NotFound($"Không tìm thấy vai trò với ID: {id}");
            }

            return NoContent();
        }
    }
}
