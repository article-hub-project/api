using AutoMapper;
using BLL.Entities;
using PL.ViewModels.Article;
using PL.ViewModels.Auth;

namespace PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();

            CreateMap<CreateArticleViewModel, Article>();
            CreateMap<CreateModuleViewModel, Module>();
        }
    }
}
