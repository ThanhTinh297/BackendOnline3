using Backend_Online_3.Irepository;
using Backend_Online_3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Online_3.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AllowAccessController : ControllerBase
    {
        private readonly IAllowAccessService _context;
        public AllowAccessController(IAllowAccessService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var allowAccesses = await _context.GetAllowAccesses();
            return Ok(allowAccesses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AllowAccess allowAccess)
        {
            if (allowAccess == null)
            {
                return BadRequest("Quyền truy cập không được để trống");
            }

            var createdAllowAccess = await _context.CreateAllowAccess(allowAccess);
            if (createdAllowAccess == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi tạo quyền truy cập");
            }

            return CreatedAtAction(nameof(GetAsync), new { id = createdAllowAccess.AllowAccessId }, createdAllowAccess);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AllowAccess allowAccess)
        {
            if (id != allowAccess.AllowAccessId)
            {
                return BadRequest("ID quyền truy cập không khớp");
            }

            var updated = await _context.UpdateAllowAccess(id, allowAccess);
            if (!updated)
            {
                return NotFound($"Không tìm thấy quyền truy cập với ID {id}");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _context.DeleteAllowAccess(id);
            if (!deleted)
            {
                return NotFound($"Không tìm thấy quyền truy cập với ID {id}");
            }

            return NoContent();
        }
    }
}
