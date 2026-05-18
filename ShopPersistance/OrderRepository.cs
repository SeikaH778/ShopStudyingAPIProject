using Microsoft.EntityFrameworkCore;
using ShopDomain.Interfaces;
namespace ShopPersistance;

public class OrderRepository : IOrderRepository
{
    private readonly ShopDBContext context; 
    public OrderRepository(ShopDBContext _context)
    {
        context =  _context;
    }
    public async Task PurchaseAsync(int productId, int quantity)
    {
        var affected = await context.Products
            .Where(p => p.Id == productId && p.Count >= quantity)
            .ExecuteUpdateAsync(p => p.SetProperty(x => x.Count, x => x.Count - quantity));

        if (affected == 0)
            throw new InvalidOperationException("Товара недостаточно или он закончился");
    }
}