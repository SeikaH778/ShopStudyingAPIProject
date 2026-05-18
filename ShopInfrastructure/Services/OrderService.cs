using ShopDomain.Models;
using ShopDomain.Interfaces;

namespace ShopApplication.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository orderRepository;
    public OrderService(IOrderRepository _orderRepository  )
    {
        orderRepository = _orderRepository;
    }
    public async Task PurchaseAsync(int productId, int quantity)
    {
       await orderRepository.PurchaseAsync(productId, quantity);
    }
}