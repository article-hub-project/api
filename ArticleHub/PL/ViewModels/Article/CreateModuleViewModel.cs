using BLL.Enums;

namespace PL.ViewModels.Article
{
    public record CreateModuleViewModel(
        int Order,
        ModuleType Type,
        string Content
        );
}
