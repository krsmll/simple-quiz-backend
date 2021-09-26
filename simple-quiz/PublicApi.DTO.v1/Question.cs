using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class Question
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        public string Content { get; set; } = default!;
        
        public ICollection<Option>? Options { get; set; }
    }
}