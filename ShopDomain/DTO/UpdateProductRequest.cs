namespace ShopDomain.DTO;

public class UpdateProductRequest
{
    public int ProductId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }
}