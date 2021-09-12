using Microsoft.AspNetCore.Identity;

namespace BH.Domain.Entities
{
    public class User : IdentityUser
    {
        public Profile Profile { get; set; }
    }
}
