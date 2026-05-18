using ShopDomain.Models;
namespace ShopDomain.Interfaces;

public interface ICartService
{
    Task<List<CartItem>> AddToCartAsync(int buyerId, int productId, int quantity);
    Task<IEnumerable<CartItem>> GetCartAsync(int userId);
    Task RemoveFromCartAsync(int cartItemId, int userId);
    Task ClearCartAsync(int userId);

}