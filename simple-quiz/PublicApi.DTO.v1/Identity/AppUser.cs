using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PublicApi.DTO.v1.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public ICollection<SelectedOption>? SelectedOptions { get; set; }
    }
}