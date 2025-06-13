using ShopDomain.Models;

namespace ShopDomain.Interfaces
{
    public interface ISellerInfoRepository : IRepository<SellerInfo>
    {
        Task<SellerInfo?> GetByUserIdAsync(int userId);
    }
}
