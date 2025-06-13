using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DTO;
using ShopApplication.Services;
using ShopDomain.Interfaces;
using ShopDomain.Models;
namespace ShopStudyingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUserService _userService;
        private readonly ISellerInfoRepository _sellerInfoRepository;
        public UserController(IRepository<User> userRepository, IUserService userRegistration, ISellerInfoRepository sellerInfoRepository)
        {
            _userRepository = userRepository;
            _userService = userRegistration;
            _sellerInfoRepository = sellerInfoRepository;
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
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("Пользователь не найден");
            }
            await _userRepository.DeleteAsync(id);
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
        [HttpPost("addProduct")]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] CreatePorductRequest request)
        {
            try
            {
                
                var userIdClaim = User.FindFirst("userId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return BadRequest("User ID не найден в токене");
                }
                if (!int.TryParse(userIdClaim, out int userId))
                {
                    return BadRequest("Неверный формат User ID");
                }
                else
                {
                    await _userService.CreateProduct(userId, request.Title, request.Description, request.Price, request.Count);
                }
                return Ok(new
                {
                    Message = "Продукт добавлен"
                });
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

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return NotFound("Пользователь не найден");

            var sellerInfo = new SellerInfo
            {
                UserId = userId,
                StoreName = request.StoreName,
                StoreDescription = request.StoreDescription,
                Products = new List<Product>()
            };

            user.Role = UserRole.Seller;

            await _sellerInfoRepository.AddAsync(sellerInfo);
            await _userRepository.UpdateAsync(user); 

            return Ok("Теперь вы продавец");
        }
    }
}
