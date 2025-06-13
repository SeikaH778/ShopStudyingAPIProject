using ShopDomain.Interfaces;
using ShopDomain.Models;
namespace ShopApplication.Services
{
    public class CartService 
    {
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<BuyerInfo> _buyerInfoRepository;
        public CartService(IRepository<CartItem> cartItemRepository, IRepository<BuyerInfo> buyerInfoRepository)
        {
            _cartItemRepository = cartItemRepository;
            _buyerInfoRepository = buyerInfoRepository;
        }
        public async Task<List<CartItem>> Cart(int buyerId, int productId, int quantity)
        {
            var cartItem = new CartItem
            {
                BuyerId = buyerId,
                ProductId = productId,
                Quantity = quantity
            };
            await _cartItemRepository.AddAsync(cartItem);
            var cartItems = await _cartItemRepository.GetAllAsync();
            return cartItems.ToList();
        }
    }
}
