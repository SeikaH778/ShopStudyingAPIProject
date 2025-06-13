
namespace ShopApplication.DTO
{
    public class CreatePorductRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; } 
    }
}
