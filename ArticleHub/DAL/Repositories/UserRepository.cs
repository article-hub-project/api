using BLL.Entities;
using BLL.Interfaces.Repositories;
using DAL.Context;
using MongoDB.Driver;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDBContext _dbContext;

        public UserRepository(MongoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _dbContext.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _dbContext.Users.InsertOneAsync(user);
        }
    }
}
