using Microsoft.EntityFrameworkCore;
using ShopDomain.Models;
using ShopDomain.Interfaces;

namespace ShopPersistance
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(ShopDBContext context) : base(context) { }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
        public async Task<User?> GetByIdWithCartAsync(int id)
        {
            return await context.Users
                .Include(u => u.Cart)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

    }
}
