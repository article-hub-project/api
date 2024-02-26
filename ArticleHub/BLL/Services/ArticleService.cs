using BLL.Entities;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;

namespace BLL.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IAuthService _authService;

        public ArticleService(IArticleRepository articleRepository, IAuthService authService)
        {
            _articleRepository = articleRepository;
            _authService = authService;
        }

        public async Task<IEnumerable<Article>> GetAsync()
        {
            return await _articleRepository.GetAsync();
        }

        public async Task<IEnumerable<Article>> GetTopAsync(int top)
        {
            return await _articleRepository.GetTopAsync(top);
        }

        public async Task<Article?> CreateAsync(Article article)
        {
            var currentUserId = _authService.GetCurrentUserId();
            if(currentUserId == null)
                return null;

            article.AuthorId = currentUserId;
            article.PublishedDate = DateTime.UtcNow;

            await _articleRepository.CreateAsync(article);

            return string.IsNullOrEmpty(article.Id) ? null : article;
        }
    }
}
