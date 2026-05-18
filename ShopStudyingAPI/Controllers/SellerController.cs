using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopDomain.DTO;
using ShopDomain.Interfaces;
namespace ShopStudyingAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SellerController:ControllerBase
{
    private readonly ISellerService sellerService;

    public SellerController(ISellerService _sellerService)
    {
        sellerService = _sellerService;
    }
    [HttpPost("addProduct")]
    [Authorize]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        try
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("User ID не найден");
            await sellerService.CreateProductAsync(userId, request);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("getProducts")]
    [Authorize]
    public async Task<IActionResult> GetMyProductsAsync()
    {
        try
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("User ID не найден");
            var result = await sellerService.GetMyProductsAsync(userId);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("deleteProduct/{productId}")]
    [Authorize]
    public async Task<IActionResult> DeleteProductAsync(int productId)
    {
        try
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("User ID не найден");
            await sellerService.DeleteProductAsync(productId, userId);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPatch("updateProduct/")]
    [Authorize]
    public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductRequest request)
    {
        try
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("User ID не найден");
            await sellerService.UpdateProductAsync( userId,request.ProductId, request.Title, request.Description, request.Price, request.Count);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}