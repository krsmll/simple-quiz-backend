using System;
using BLL.App.DTO.Identity;
using Domain.Base;

namespace BLL.App.DTO
{
    public class SelectedOption : DomainEntityId
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid OptionId { get; set; }
        public Option? Option { get; set; }
    }
}