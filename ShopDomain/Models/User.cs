using ShopDomain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ShopDomain.Models
{
    public enum UserRole
    {
        Admin =1,
        Buyer =2 ,
        Seller =3
    }
    public class User : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public UserRole Role { get; set; } = UserRole.Buyer;
        public SellerInfo? SellerInfo { get; set; }
        public List<CartItem> Cart { get; set; } = new();
    }

    public class SellerInfo
    {
        public int Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string StoreDescription { get; set; } = string.Empty;
        public List<Product> Products { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
    
}
