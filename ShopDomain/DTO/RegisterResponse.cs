using ShopDomain.Interfaces;
using ShopDomain.Models;

namespace ShopDomain.Models;

public class RegisterResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; } = UserRole.Buyer;
}