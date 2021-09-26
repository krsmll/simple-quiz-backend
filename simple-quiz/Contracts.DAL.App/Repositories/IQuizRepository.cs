using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IQuizRepository : IBaseRepository<Quiz>, IQuestionRepositoryCustom<Quiz>
    {
        
    }
    
    public interface IQuizRepositoryCustom<TEntity>
    {
        
    }
}