using ShopDomain.Interfaces;
using ShopDomain.Models;

namespace ShopPersistance
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
