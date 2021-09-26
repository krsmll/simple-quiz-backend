using System;
using System.Collections.Generic;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Option : DomainEntityId
    {
        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }
        
        public string Content { get; set; } = default!;
        public bool IsCorrect { get; set; }
        
        public ICollection<SelectedOption>? SelectedOptions { get; set; }
    }
}