using System.Collections.Generic;

namespace DTO.App
{
    public class JwtResponse
    {
        public string Token { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public ICollection<string> Roles { get; set; } = default!;
    }
}