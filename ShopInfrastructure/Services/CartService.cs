using ShopDomain.Interfaces;
using ShopDomain.Models;
namespace ShopApplication.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<CartItem> cartItemRepository;
        private readonly IUserRepository userRepository;
        private readonly IRepository<Product>productRepository;
        public CartService(IRepository<CartItem> _cartItemRepository, IUserRepository _userRepository, IRepository<Product> _productRepository)
        {
            cartItemRepository = _cartItemRepository;
            userRepository = _userRepository;
            productRepository = _productRepository;
        }
        public async Task<List<CartItem>> AddToCartAsync(int buyerId, int productId, int quantity)
        {
            var user = await userRepository.GetByIdWithCartAsync(buyerId);
            var product = await productRepository.GetByIdAsync(productId);
            var cartItem = new CartItem
            {
                User =  user,
                UserId = buyerId,
                ProductId = productId,
                Quantity = quantity,
                Product = product
            };
            await cartItemRepository.AddAsync(cartItem);
            var cartItems = await cartItemRepository.GetAllAsync();
            return cartItems.Where(c => c.UserId == buyerId).ToList();
        }

        public async Task<IEnumerable<CartItem>> GetCartAsync(int userId)
        {
            var user = await userRepository.GetByIdWithCartAsync(userId);
            return user.Cart;
        }

        public async Task RemoveFromCartAsync(int cartItemId, int userId)
        { 
            var user = await userRepository.GetByIdWithCartAsync(userId);
            var result = user.Cart.FirstOrDefault(x => x.ProductId == cartItemId);
            if(result == null)
                throw new ("Товар отсутствует в корзине");
            await cartItemRepository.DeleteAsync(result.Id);
            
        }

        public async Task ClearCartAsync(int userId)
        {
            var user = await userRepository.GetByIdWithCartAsync(userId);
            if (!user.Cart.Any()) throw new Exception("Корзина пуста");
            var itemIds = user.Cart.Select(x => x.Id).ToList();

            foreach(var itemId in itemIds)
            {
                await cartItemRepository.DeleteAsync(itemId);
            }
        }
    }
}
