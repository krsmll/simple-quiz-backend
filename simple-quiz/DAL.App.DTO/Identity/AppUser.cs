using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public ICollection<SelectedOption>? SelectedOptions { get; set; }
    }
}