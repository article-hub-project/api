using BLL.Entities;

namespace BLL.Interfaces.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAsync();
        Task CreateAsync(Article article);
    }
}
