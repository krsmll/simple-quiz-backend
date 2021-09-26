using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BLL.App.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public ICollection<SelectedOption>? SelectedOptions { get; set; }
    }
}