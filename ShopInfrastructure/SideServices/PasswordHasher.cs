
namespace ShopApplication.SideServices
{
    public class PasswordHasher 
    {

        public string HashPassword(string password)=>BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        
        public bool Verify(string hashedPassword, string password)=> BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        
    }
}
