namespace ShopDomain.Interfaces;

public interface IOrderService
{
    Task PurchaseAsync(int productId, int quantity);
}