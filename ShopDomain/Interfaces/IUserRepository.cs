using ShopDomain.Interfaces;
using ShopDomain.Models;

namespace ShopDomain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User?> GetByIdWithCartAsync(int id);
    }
}
