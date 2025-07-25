using Backend_Online_3.Irepository;
using Backend_Online_3.Service;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Online_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _jwtService = new JwtService(config);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var users = await _userService.GetUsers();
            var user = users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
                return Unauthorized("Email hoặc mật khẩu không đúng");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }

    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
