namespace ShopDomain.Interfaces;

public interface IOrderRepository
{
    Task PurchaseAsync(int productId, int quantity);
}