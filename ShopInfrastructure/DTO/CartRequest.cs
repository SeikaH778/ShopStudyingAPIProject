

namespace ShopApplication.DTO
{
    public class CartRequest
    {
        public int BuyerId { get; set; }
        public List<int> ProductIds { get; set; } = new List<int>();
        public int Quantity { get; set; } = 1; 
    }
}
