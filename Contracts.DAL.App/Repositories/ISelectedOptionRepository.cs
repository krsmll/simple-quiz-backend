using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ISelectedOptionRepository : IBaseRepository<SelectedOption>, ISelectedOptionRepositoryCustom<SelectedOption>
    {
        
    }
    
    public interface ISelectedOptionRepositoryCustom<TEntity>
    {
        
    }
}