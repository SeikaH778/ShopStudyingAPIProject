using ShopDomain.Models;
using ShopDomain.DTO;
namespace ShopDomain.Interfaces;

public interface ISellerService
{
    Task<Product> CreateProductAsync(int userId, CreateProductRequest productRequest);
    Task<IEnumerable<Product>> GetMyProductsAsync(int userId);
    Task DeleteProductAsync(int productId, int userId);
    Task UpdateProductAsync(int userId, int productId, string title, string description, decimal price, int count);
}