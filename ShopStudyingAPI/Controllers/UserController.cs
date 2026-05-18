using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ShopDomain.DTO;
using ShopDomain.Interfaces;
using ShopDomain.Models;
namespace ShopStudyingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController( IUserService userRegistration)
        {
            _userService = userRegistration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            var user = await _userService.RegisterUserAsync(
                request.Name, request.Email, request.Password);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            HttpContext context = HttpContext;
            var userToken = await _userService.LoginAsync(request.Email, request.Password);
            if (userToken == null)
            {
                return Unauthorized("Неверный email или пароль");
            }

            context.Response.Cookies.Append("jwtToken", userToken);
            return Ok(userToken);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok("Пользователь успешно удален");
        }

        [HttpPatch("{id}/{newRole}")]
        public async Task<IActionResult> Update(int id, UserRole newRole)
        {
            try
            {
                await _userService.ChangeRole(id, newRole);
                return Ok("Роль пользователя успешно обновлена");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Пользователь не найден");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("become-seller")]
        [Authorize]
        public async Task<IActionResult> BecomeSeller([FromBody] CreateSellerRequest request)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("User ID не найден");
            await _userService.BecomeSeller(userId, request);
            return Ok("Теперь вы продавец");
        }
    }
}
