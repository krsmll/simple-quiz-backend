using System;

namespace Contracts.Domain.Base
{
    public interface IDomainEntity : IDomainEntity<Guid>
    {
        
    }
    
    public interface IDomainEntity<TKey>
    {
        
    }
}