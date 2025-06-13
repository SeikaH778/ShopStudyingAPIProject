

using ShopDomain.Models;

namespace ShopPersistance
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(ShopDBContext context) : base(context) { }

        public async Task<User> GetByEmail(string email)
        {
            var user = context.Set<User>().FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }
            return user;
        }
    }
}
