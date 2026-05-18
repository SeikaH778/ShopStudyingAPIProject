using Microsoft.EntityFrameworkCore;
using ShopDomain.DTO;
using ShopDomain.Models;
using ShopDomain.Interfaces;
using ShopPersistance;
namespace ShopApplication.Services;

public class SellerService : ISellerService
{
    private readonly IRepository<Product> productRepository;
    private readonly ISellerInfoRepository  sellerInfoRepository;
    
    public SellerService(IRepository<Product> _productRepository,  ISellerInfoRepository _sellerInfoRepository)
    {
        productRepository = _productRepository;
        sellerInfoRepository = _sellerInfoRepository;
    }
    private async Task<SellerInfo> GetSellerOrThrowAsync(int userId)
    {
        var sellerInfo = await sellerInfoRepository.GetByUserIdAsync(userId);
        if (sellerInfo == null)
            throw new Exception("Пользователь не является продавцом");
        return sellerInfo;
    }
    public async Task<Product> CreateProductAsync(int userId, CreateProductRequest productRequest)
    {
        var sellerInfo = await GetSellerOrThrowAsync(userId);
        var product = new Product
        {
            Title = productRequest.Title,
            Description = productRequest.Description,
            Price = productRequest.Price,
            Count = productRequest.Count,
            SellerId = sellerInfo.Id
        };
        await productRepository.AddAsync(product);
        return product;
    }

    public async Task<IEnumerable<Product>> GetMyProductsAsync(int userId)
    {
        var sellerInfo = await sellerInfoRepository.GetByUserIdAsync(userId);
        if (sellerInfo == null)
            throw new Exception("Пользователь не является продавцом");
    
        return sellerInfo.Products;
    }

    public async Task DeleteProductAsync(int productId, int userId)
    {
        var sellerInfo = await GetSellerOrThrowAsync(userId);
        var product = sellerInfo.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
            throw new Exception("Продукт не найден или не принадлежит продавцу");
        await productRepository.DeleteAsync(product.Id);
        
    }

    public async Task UpdateProductAsync(int userId, int productId, string title, string description, decimal price, int count)
    {
        var sellerInfo = await GetSellerOrThrowAsync(userId);
        var product = sellerInfo.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
            throw new Exception("Продукт не найден или не принадлежит продавцу");
        product.Title = title;
        product.Description = description;
        product.Price = price;
        product.Count = count;
        await productRepository.UpdateAsync(product);
    }
}