using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
           IOptionRepository Options { get; }
           IQuestionRepository Questions { get; }
           IQuizRepository Quizzes { get; }
           ISelectedOptionRepository SelectedOptions { get; }
    }
}