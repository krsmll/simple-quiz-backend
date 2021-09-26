using System.Collections.Generic;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Quiz : DomainEntityId
    {
        public string Title { get; set; } = default!;
        public bool IsPoll { get; set; }
        
        public ICollection<Question>? Questions { get; set; }
    }
}