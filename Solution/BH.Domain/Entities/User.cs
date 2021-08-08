using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }


        public Profile Profile { get; set; }

        public virtual IList<Role> Roles { get; set; } = new List<Role>();
    }
}
