using Backend_Online_3.Irepository;
using Backend_Online_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Online_3.Controllers
{
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
        public async Task<IActionResult> createAsync([FromBody] AllowAccess allowAccess)
        {
            if (allowAccess == null)
            {
                return BadRequest("AllowAccess cannot be null");
            }
            var createdAllowAccess = await _context.CreateAllowAccess(allowAccess);
            if (createdAllowAccess == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating AllowAccess");
            }
            return CreatedAtAction(nameof(GetAsync), new { id = createdAllowAccess.AllowAccessId }, createdAllowAccess);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateAsync(int id, [FromBody] AllowAccess allowAccess)
        {
            if (id != allowAccess.AllowAccessId)
            {
                return BadRequest("AllowAccess ID mismatch");
            }
            var updated = await _context.UpdateAllowAccess(id, allowAccess);
            if (!updated)
            {
                return NotFound($"AllowAccess with ID {id} not found");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteAsync(int id)
        {
            var deleted = await _context.DeleteAllowAccess(id);
            if (!deleted)
            {
                return NotFound($"AllowAccess with ID {id} not found");
            }
            return NoContent();
        }
    }
}
