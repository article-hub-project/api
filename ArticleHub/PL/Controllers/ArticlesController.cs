using AutoMapper;
using BLL.Entities;
using BLL.Interfaces.Services;
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

        public ArticlesController(IMapper mapper, IArticleService articleService)
        {
            _mapper = mapper;
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> Get()
        {
            return Ok(await _articleService.GetAsync());
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
