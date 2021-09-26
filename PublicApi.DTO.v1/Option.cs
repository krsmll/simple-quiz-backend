using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class Option
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }
        
        public string Content { get; set; } = default!;
        public bool IsCorrect { get; set; }
        
        public ICollection<SelectedOption>? SelectedOptions { get; set; }
    }
}