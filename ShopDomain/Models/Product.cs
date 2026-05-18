using ShopDomain.Interfaces;
using System.Text.Json.Serialization;
namespace ShopDomain.Models
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int SellerId { get; set; }
        [JsonIgnore]
        public SellerInfo Seller { get; set; }
    }
}
