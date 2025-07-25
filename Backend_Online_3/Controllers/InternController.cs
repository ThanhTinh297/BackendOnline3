using Backend_Online_3.Data;
using Backend_Online_3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend_Online_3
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InternController : ControllerBase
    {
        private readonly BackendOnline3DbContext _context;

        public InternController(BackendOnline3DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetInternsWithAccess()
        {
            // 1. Lấy user ID từ token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();

            int userId = int.Parse(userIdClaim);

            // 2. Lấy user và role
            var user = await _context.User.FindAsync(userId);
            if (user == null || user.RoleId == null) return Unauthorized();

            var roleId = user.RoleId.Value;

            // 3. Lấy quyền từ bảng AllowAccess cho bảng "Intern"
            var allowAccess = _context.AllowAccess.FirstOrDefault(a => a.RoleId == roleId && a.TableName == "Intern");
            if (allowAccess == null || string.IsNullOrEmpty(allowAccess.AccessProperties))
                return Forbid("Không có quyền truy cập thông tin thực tập sinh.");

            var allowedProps = allowAccess.AccessProperties.Split(',').Select(p => p.Trim()).ToHashSet();

            // 4. Lấy toàn bộ dữ liệu Intern
            var interns = _context.Intern.ToList();

            // 5. Trả về chỉ các property được phép
            var result = interns.Select(intern =>
            {
                var dict = new Dictionary<string, object?>();
                var props = typeof(Intern).GetProperties();

                foreach (var prop in props)
                {
                    if (allowedProps.Contains(prop.Name))
                    {
                        dict[prop.Name] = prop.GetValue(intern);
                    }
                }

                return new DynamicInternDTO { Properties = dict };
            });

            return Ok(result);
        }
    }
    public class DynamicInternDTO
    {
        public Dictionary<string, object?> Properties { get; set; } = new();
    }
}
