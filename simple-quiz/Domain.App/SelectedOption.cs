using System;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class SelectedOption : DomainEntityId
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid OptionId { get; set; }
        public Option? Option { get; set; }
    }
}