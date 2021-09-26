using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IOptionService Options { get; }
        IQuestionService Questions { get; }
        IQuizService Quizzes { get; }
        ISelectedOptionService SelectedOptions { get; }
    }
}