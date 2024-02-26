using BLL.Entities;
using BLL.Interfaces.Repositories;
using DAL.Context;
using MongoDB.Driver;

namespace DAL.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly MongoDBContext _dbContext;

        public ArticleRepository(MongoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Article>> GetAsync()
        {
            return await _dbContext.Articles.Find(_ => true).ToListAsync();
        }

        public async Task CreateAsync(Article article)
        {
            await _dbContext.Articles.InsertOneAsync(article);
        }
    }
}
