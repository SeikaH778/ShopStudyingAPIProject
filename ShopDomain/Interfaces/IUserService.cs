using ShopDomain.Models;
namespace ShopDomain.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string name, string email, string password);
        Task<string> LoginAsync(string email, string password);
        Task CreateProduct(int userId, string _title, string _description, decimal _price, int _count);
        Task ChangeRole(int id, UserRole newRole);
    }
}
