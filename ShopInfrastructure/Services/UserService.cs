using ShopDomain.DTO;
using ShopApplication.SideServices;
using ShopDomain.Interfaces;
using ShopDomain.Models;
namespace ShopApplication.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtProvider _jwtProvider;
        private readonly ISellerInfoRepository _sellerInfoRepository;
        public UserService(IUserRepository userRepository,
            PasswordHasher passwordHasher,
            JwtProvider jwtProvider,
            ISellerInfoRepository sellerInfoRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
            _sellerInfoRepository = sellerInfoRepository ?? throw new ArgumentNullException(nameof(sellerInfoRepository));
        }
        public async Task<RegisterResponse> RegisterUserAsync (string name, string email, string password)
        {
            if (await IsEmailRegisterd(email))
            {
                throw new Exception("Email уже занят");
            }
            else
            {
                var hashedPassword = _passwordHasher.HashPassword(password);
                var user = new User
                {
                    Name = name,
                    Email = email,
                    Password = hashedPassword
                };
                await _userRepository.AddAsync(user);
                var result = new RegisterResponse
                {
                    Id = user.Id,
                    Name = name,
                    Email = email,
                    Role = user.Role
                };
                return result;
            }
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }
            if (_passwordHasher.Verify(user.Password, password) == false)
            {
                throw new Exception("Неверный пароль");
            }

            var token = _jwtProvider.GenerateToken(user);
            return token;
        }
        public async Task ChangeRole(int id, UserRole newRole)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }
            user.Role = newRole;
            await _userRepository.UpdateAsync(user);
        }
        private async Task<bool> IsEmailRegisterd(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user != null;    
        }
        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Пользователь не найден");
            await _userRepository.DeleteAsync(id);
        }
        public async Task BecomeSeller(int userId, CreateSellerRequest request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new ("Пользователь не найден");
            
            var sellerInfo = new SellerInfo
            {
                UserId = userId,
                StoreName = request.StoreName,
                StoreDescription = request.StoreDescription,
                Products = new List<Product>()
            };
            
            user.Role = UserRole.Seller;
            await _sellerInfoRepository.AddAsync(sellerInfo);
            await _userRepository.UpdateAsync(user); 
        }
        
    }
}
