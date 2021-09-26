using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IOptionRepository : IBaseRepository<Option>, IOptionRepositoryCustom<Option>
    {
        
    }
    
    public interface IOptionRepositoryCustom<TEntity>
    {
        
    }
}