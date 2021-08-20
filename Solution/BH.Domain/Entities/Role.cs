using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Role : IdentityRole
    {
        public virtual IList<User> Users { get; set; } = new List<User>();
    }
}
