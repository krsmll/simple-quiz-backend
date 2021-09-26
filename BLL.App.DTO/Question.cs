using System;
using System.Collections.Generic;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Question : DomainEntityId
    {
        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        public string Content { get; set; } = default!;
        
        public ICollection<Option>? Options { get; set; }
    }
}