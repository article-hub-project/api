namespace PL.ViewModels.Article
{
    public record CreateArticleViewModel(
        string Title,
        ICollection<CreateModuleViewModel> Modules
        );
}
