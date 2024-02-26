using BLL.Entities;

namespace BLL.Interfaces.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAsync();
        Task<IEnumerable<Article>> GetTopAsync(int top);
        Task CreateAsync(Article article);
    }
}
