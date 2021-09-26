using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public bool IsPoll { get; set; }
        
        public ICollection<Question>? Questions { get; set; }
    }
}