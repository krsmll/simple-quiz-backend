using System;

namespace PublicApi.DTO.v1
{
    public class SelectedOption
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        
        public Guid OptionId { get; set; }
        public Option? Option { get; set; }
    }
}