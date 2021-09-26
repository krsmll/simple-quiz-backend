using System;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class SelectedOption : DomainEntityId
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid OptionId { get; set; }
        public Option? Option { get; set; }
    }
}