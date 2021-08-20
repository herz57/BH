using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public Profile Profile { get; set; }

        public virtual IList<Role> Roles { get; set; } = new List<Role>();
    }
}
