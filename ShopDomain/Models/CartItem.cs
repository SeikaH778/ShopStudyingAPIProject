using ShopDomain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopDomain.Models
{
    public class CartItem : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int BuyerId { get; set; }
        public BuyerInfo Buyer { get; set; }
        [NotMapped]
        public decimal TotalPrice => Product.Price * Quantity;
    }
}
