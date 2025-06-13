using ShopDomain.Interfaces;
using ShopDomain.Models;

namespace ShopPersistance
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly ShopDBContext context;
        public Repository(ShopDBContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(T entity)
        {

            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();


        }
        public async Task UpdateAsync(T entity)
        {

            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {

            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }


        }
        public async Task<T?> GetByIdAsync(int id)
        {

            return await context.Set<T>().FindAsync(id);

        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return context.Set<T>().ToList();
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            var user =  context.Set<User>().ToList().FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }
            return user;
        }
    }
}
