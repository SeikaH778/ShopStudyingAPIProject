using ShopDomain.Interfaces;
namespace ShopDomain.Models
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public decimal Price { get; set; }
        public int count { get; set; }
        public int SellerId { get; set; }
        public SellerInfo Seller { get; set; }
    }
}
