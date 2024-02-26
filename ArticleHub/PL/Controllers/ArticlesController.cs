using AutoMapper;
using BLL.Entities;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using PL.ViewModels.Article;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IArticleService _articleService;
        private CacheService _cacheService;

        public ArticlesController(IMapper mapper, IArticleService articleService, CacheService cacheService)
        {
            _mapper = mapper;
            _articleService = articleService;
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> Get()
        {
            var cacheKey = Constants.GetArticles;
            var articles = _cacheService.Get<IEnumerable<Article>>(cacheKey);

            if (articles == null)
            {
                articles = await _articleService.GetAsync();
                _cacheService.Set(cacheKey, articles, TimeSpan.FromMinutes(5));
            }

            return Ok(articles);
        }

        [HttpGet("top")]
        public async Task<ActionResult<IEnumerable<Article>>> GetTop(int top)
        {
            var cacheKey = $"{Constants.GetTopArticles}/{top}";
            var articles = _cacheService.Get<IEnumerable<Article>>(cacheKey);

            if (articles == null)
            {
                articles = await _articleService.GetTopAsync(top);
                _cacheService.Set(cacheKey, articles, TimeSpan.FromMinutes(5));
            }

            return Ok(articles);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] CreateArticleViewModel model)
        {
            var mappedModel = _mapper.Map<Article>(model);

            var createdArticle = await _articleService.CreateAsync(mappedModel);
            if (createdArticle == null)
                return BadRequest();

            return Ok(createdArticle.Id);
        }
    }
}
