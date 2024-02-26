using BLL.Entities;

namespace BLL.Interfaces.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAsync();
        Task<IEnumerable<Article>> GetTopAsync(int top);
        Task<Article?> CreateAsync(Article article);
    }
}
