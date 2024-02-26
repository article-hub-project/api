using BLL.Entities;

namespace BLL.Interfaces.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAsync();
        Task<Article?> CreateAsync(Article article);
    }
}
