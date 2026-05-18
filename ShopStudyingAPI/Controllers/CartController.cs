using Microsoft.AspNetCore.Mvc;
using ShopDomain.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace ShopStudyingAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService cartService;
    private readonly IOrderService orderService;
    public CartController(ICartService _cartService, IOrderService _orderService)
    {
        cartService = _cartService;
        orderService = _orderService;
    }
    [HttpPost("addToCart/{productId}/{quantity}")]
    [Authorize]
    public async Task<IActionResult> AddToCartAsync(int productId, int quantity)
    {
        try
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("User ID не найден");
            var result = await cartService.AddToCartAsync(userId, productId, quantity);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("getCart")]
    [Authorize]
    public async Task<IActionResult> GetCartAsync()
    {
        try
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("User ID не найден");
            var result = await cartService.GetCartAsync(userId);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("removeFromCart/{cartItemId}")]
    [Authorize]
    public async Task<IActionResult> RemoveFromCartAsync(int cartItemId)
    {
        try
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("User ID не найден");
            await cartService.RemoveFromCartAsync(cartItemId, userId);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("clearCart")]
    [Authorize]
    public async Task<IActionResult> ClearCartAsync()
    {
        try
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("User ID не найден");
            await cartService.ClearCartAsync(userId);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{productId}/purchase")]
    [Authorize]
    public async Task<IActionResult> OrderAsync(int productId, int quantity)
    {
        try
        {
            await orderService.PurchaseAsync(productId, quantity);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}