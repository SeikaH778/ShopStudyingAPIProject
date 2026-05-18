using ShopDomain.Models;
using ShopDomain.DTO;
namespace ShopDomain.Interfaces
{
    public interface IUserService
    {
        Task<RegisterResponse> RegisterUserAsync(string name, string email, string password);
        Task<string> LoginAsync(string email, string password);
        Task ChangeRole(int id, UserRole newRole);
        Task DeleteUserAsync(int id);
        Task BecomeSeller(int userId, CreateSellerRequest request);
    }
}
