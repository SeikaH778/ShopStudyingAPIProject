using Microsoft.EntityFrameworkCore;
using ShopApplication.SideServices;
using ShopDomain.Interfaces;
using ShopDomain.Models;
using ShopPersistance;
namespace ShopApplication.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Product> _productrepository;
        private readonly ISellerInfoRepository _sellerInfoRepository;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtProvider _jwtProvider;
        public UserService(IUserRepository userRepository,
            IRepository<Product> repository,
            PasswordHasher passwordHasher,
            JwtProvider jwtProvider,
            ISellerInfoRepository sellerInfoRepository)
        {
            _productrepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
            _sellerInfoRepository = sellerInfoRepository;
        }
        public async Task<User> RegisterUserAsync (string name, string email, string password)
        {
            if (IsEmailRegisterd(email) == false)
            {
                var hashedPassword = _passwordHasher.HashPassword(password);
                var user = new User
                {
                    Name = name,
                    Email = email,
                    Password = hashedPassword
                };
                await _userRepository.AddAsync(user);
                return user;
            }
            else
            {
                throw new Exception("Email уже занят");
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

        public async Task CreateProduct(int userId, string _title, string _description, decimal _price, int _count)
        {
            var sellerInfo = await _sellerInfoRepository.GetByUserIdAsync(userId);
            if (sellerInfo == null)
                throw new Exception("Пользователь не является продавцом");

            await _productrepository.AddAsync(new Product
            {
                SellerId = sellerInfo.Id, 
                Title = _title,
                Description = _description,
                Price = _price,
                count = _count
            });
        }
        private bool IsEmailRegisterd(string email)
        {
            var existingUsers =  _userRepository.GetAllAsync();
            var existingUser = existingUsers.Result.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                throw new Exception("Email уже занят");
            }
            else
            {
                return false;
            }
        }
    }
}
