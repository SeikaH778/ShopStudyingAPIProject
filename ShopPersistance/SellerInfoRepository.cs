using ShopDomain.Interfaces;
using ShopDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace ShopPersistance
{
    public class SellerInfoRepository : Repository<SellerInfo>, ISellerInfoRepository
    {
        private readonly ShopDBContext _context;

        public SellerInfoRepository(ShopDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SellerInfo?> GetByUserIdAsync(int userId)
        {
            return await _context.SellerInfo
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }
    }
}
